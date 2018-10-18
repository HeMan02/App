using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EvisoNetworkObj : NetworkBehaviour {

	// non sono convinto per ORA di utilizzare una classe statica come instance perchè non ne vedo l'utilizzo
	public string ip = "192.168.1.192";
	public int port = 25001;
	public static EvisoNetworkObj instance;

	// PRESI DA NETWORK MANAGER USATI PER PHP
	public string[] items;
	public bool checkToEnter = false;
	public string itemsDataString;
	public string passClient = null;
	public string mailClient = null;
	public string passToConfirmClient = null;
	bool mailCheck = false;
	bool passcheck = false;
	string CreateUserUrl = "http://localhost/PHPScripts/Insert.php";
	string AddReadingsUrl = "http://localhost/PHPScripts/InsertAutoEnergy.php";
	public List<DataClient> dataClientList;
	public DataClient dataClient;
	public string[] getItemVector;
	public string[] testString;

	public struct DataClient{
		public string nome;
		public string pod;
		public string nFtt;
		public string emissione;
		public string perRifIn;
		public string perRifFine;
		public string origineDati;
		public int f1;
		public int f2;
		public int f3;
	}


	void Start(){
		// Tengo i nlocale solo la versione proprietaria del codice, sul server ovviamente ci saranno tutte le copie
		if (isLocalPlayer) { // se sono in locale mi setto l'istanza e me l osalvo come indistruttibile in caso cambi scena
			instance = this;
			DontDestroyOnLoad (this);
		} else if (!isServer) { // Se non sono server e non sono il propietario l odistruggo
//			Debug.LogError ("INTRUSO!!!! E LO DISTRUGGO");
			Destroy (gameObject);
			return;
		}
	}

	[Command]
	public void CmdCheckClient(string name,string password){
		Debug.LogError ("client dice al server  il nome: " + name + " e la password " + password + " CONN: " + connectionToClient.isConnected);
		passClient = password;
		mailClient = name;
		StartCoroutine ("CheckPassMailLogIn");
		// check dei valori s epresenti su DB e dopo invio del messaggio al client
	}

	[Command]
	public void CmdGetDataClient(){
		StartCoroutine("GetDataClientFromDb");
	}

	// in teoria non dovrebbe servire assegnare altre variabile ma passargli già le variabili da inserire perchè controllo effettuato in locale
	[Command]
	public void CmdAddAutoReadings(string pod, string f1, string f2, string f3,string data){
//		Debug.LogError ("POD: " + pod + " F1: " + f1 + " F2: " + f2 + "F3" + f3 );
		AddReadings (pod,f1,f2,f3,data);
	}

	// SERVER -> CLIENT risponde se password è giusta o no inserita dal client
	[TargetRpc]
	public void TargetChekValue(NetworkConnection target,bool checkCLinet){
		if (checkCLinet) {
		EvisoMainPage.instance.OpenLoginPage ();
		} else {
			Debug.LogError("CLIENT SBAGLIATA!!!!!!!");
	   	EvisoMainPage.instance.PrintInfoText ("PASS SBAGLIATA");
			// sbagliata fare comparire a video che sbagliata
		}
		// se vero o false vado ad aprire la scena
	}

	// utilizzo per mandare da SERVER -> CLIENT i dati dell'utente
	[TargetRpc]
	public void TargetSetDataClient(NetworkConnection target,DataClient dataList){
		// controllo se i dati ricevuti sono già presenti nella lista
		//  se non presenti creo la lista contenente tutti i pod e letture per il cliente in locale
		if (dataClientList == null) {
			dataClientList = new List<DataClient>();
			dataClientList.Add (dataList);
		} else {
			dataClientList.Add (dataList);
		}
		Debug.LogError ("Data Lenght " + dataClientList.Count);
		for (int i = 0; i < dataClientList.Count; i++) {
			Debug.LogError("N: " + dataClientList[i].nome + " P: " + dataClientList[i].pod + " F: " + dataClientList[i].nFtt  + " E: " + dataClientList[i].emissione  + " RI: " + dataClientList[i].perRifIn + " RF: " + dataClientList[i].perRifFine + " O: " + dataClientList[i].origineDati + " F1: " + dataClientList[i].f1 + " F2: " + dataClientList[i].f2 + " F3: " + dataClientList[i].f3);
		}
	}
	// TENERE TUTTO IL CODICE DENTRO L'OGGETTO CHE CHIAMA I COMMAND SE NO SI PERDE I RIFERIMENTI!!!!!
	// ================================================== CONNESSIONE PHP ==================================================
	// controlla se password presente sul DB e ritorna il check 
	IEnumerator CheckPassMailLogIn ()
	{
		WWW itemsData = new WWW ("http://localhost/PHPScripts/Query.php");
		yield return itemsData;
		string itemsDataString = itemsData.text;
		items = itemsDataString.Split (';');
		// prendo i dati in modo corretto ma pensare come fare check, una è una coroutine e non è sincronizzata
		mailCheck = false;
		passcheck = false;
		// scandisco tutti i nomi delle mail e delle pass e controllo se almeno una fa check
		for (int i = 0; i < items.Length -1; i++ ){
			string[] mailAndPass = items[i].Split('|');
			string[] mailTotal = mailAndPass[0].Split (':');
			string mail = null;
			if (mailTotal[1] != null) {
				mail = mailTotal [1];
//				Debug.Log("MailClient " +  mailClient + mailClient.Length + " == " + mail + mail.Length);
				if (string.Compare (mailClient, mail) == 0) {
					mailCheck = true;
				}
			}
			string[] passTotal = mailAndPass[1].Split (':');
			string pass = null;
			if (passTotal [1] != null) {
				pass = passTotal [1];
//				Debug.Log("PassClient " +  passClient + " == " + pass);
				if (string.Compare (passClient, pass) == 0) {
					passcheck = true;
				}
			}
		}
		if (mailCheck && passcheck) {
			// mi vado a prendere il riferimentop o vedo come dagli l'input per settare a ok e andare avanti se no no
			StartCoroutine(GetAndSendDataToClient(mailClient));
		} else {
			//			Debug.Log ("PASS SBAGLIATA " + connectionToClient.isConnected);
			TargetChekValue (connectionToClient, false);
		}
	}

	// se utente già presente non ti registra di nuovo, se  no ti aggiunge
	IEnumerator CheckPassMailRegister ()
	{
		WWW itemsData = new WWW ("http://localhost/PHPScripts/Query.php");
		yield return itemsData;
		string itemsDataString = itemsData.text;
		items = itemsDataString.Split (';');
		// prendo i dati in modo corretto ma pensare come fare check, una è una coroutine e non è sincronizzata
		mailCheck = false;
		passcheck = false;
		// scandisco tutti i nomi delle mail e delle pass e controllo se almeno una fa check
		for (int i = 0; i < items.Length -1; i++ ){
			string[] mailAndPass = items[i].Split('|');
			string[] mailTotal = mailAndPass[0].Split (':');
			string mail = null;
			if (mailTotal[1] != null) {
				mail = mailTotal [1];
				//				Debug.Log("MailClient " +  mailClient + mailClient.Length + " == " + mail + mail.Length);
				if (string.Compare (mailClient, mail) == 0) {
					mailCheck = true;
				}
			}
			string[] passTotal = mailAndPass[1].Split (':');
			string pass = null;
			if (passTotal [1] != null) {
				pass = passTotal [1];
				//				Debug.Log("PassClient " +  passClient + " == " + pass);
				if (string.Compare (passClient, pass) == 0) {
					passcheck = true;
				}
			}
		}
		//		Debug.Log("mailCheck " +  mailCheck + " passcheck " + passcheck);
		if (mailCheck && passcheck) {
			// mi vado a prendere il riferimentop o vedo come dagli l'input per settare a ok e andare avanti se no no
			Debug.Log ("PASS già presente, non ti puoi registrare ");
			Register.instance.PrintInfoText ("PASS già presente, non ti puoi registrare ");
		} else {
			Debug.Log ("PASS non presente ti registro ");
			Register.instance.PrintInfoText ("PASS non presente ti registro ");
			CreateUser (mailClient, passClient); // AGGIUNTO 
			// CHIAMARE l'insert nel DB
		}
	}

	//  INSERT su DB
	public void CreateUser(string mail, string pass){
		WWWForm form = new WWWForm();
		form.AddField("mailclientPost",mail);
		form.AddField("passClientPost",pass);
		WWW www = new WWW (CreateUserUrl, form);
	}

	public void AddReadings(string podClient, string f1Client, string f2Client, string f3Client,string dataClient){
		WWWForm form = new WWWForm();
		form.AddField("podClient",podClient);
		form.AddField("f1Client",f1Client);
		form.AddField("f2Client",f2Client);
		form.AddField("f3Client",f3Client);
		form.AddField("dataClient",dataClient);
		WWW www = new WWW (AddReadingsUrl, form);
	}
	// TEST		------------------------------------------------------
//	IEnumerator GetDataclientFromDb ()
//	{
////		WWWForm form = new WWWForm();
////		form.AddField("Mail",);
//		WWW itemsData = new WWW ("http://togeathosting.altervista.org/QueryGetDataRiepBollette.php");
//		yield return itemsData;
//		string itemsDataString = itemsData.text;
//		items = itemsDataString.Split (';');
//		// prendo i dati in modo corretto ma pensare come fare check, una è una coroutine e non è sincronizzata
//		// scandisco tutti i nomi delle mail e delle pass e controllo se almeno una fa check
//		for (int i = 0; i < items.Length -1; i++ ){
//			string[] mailAndPass = items[i].Split('|');
//			Debug.LogError("i: " + i + " item: " + items[i].ToString());
////			string[] mailTotal = mailAndPass[0].Split (':');
////			string[] passTotal = mailAndPass[1].Split (':');
//		}
//	}

	// utilizzare per checcare e poi andare a settare i valori al client
	IEnumerator GetAndSendDataToClient(string mail){
		WWWForm form = new WWWForm();
		form.AddField("mailclientPost",mail);
		WWW itemsData = new WWW ("http://localhost/PHPScripts/QueryGetDataRiepBollette.php");
		yield return itemsData;
		string itemsDataString = itemsData.text;
		items = itemsDataString.Split (';');
		// prendo i dati in modo corretto ma pensare come fare check, una è una coroutine e non è sincronizzata
		// scandisco tutti i nomi delle mail e delle pass e controllo se almeno una fa check
		for (int i = 0; i < items.Length -1; i++ ){
			getItemVector = items[i].Split('|');
			dataClient = new DataClient();
			for (int j= 0; j < getItemVector.Length; j++) {
				testString = getItemVector[j].Split (':');
				switch (j) {
				case 0:
					dataClient.nome = testString [1];
					break;
				case 1:
					dataClient.pod = testString [1];
					break;
				case 2:
					dataClient.nFtt = testString [1];
					break;
				case 3:
					dataClient.emissione = testString [1];
					break;
				case 4:
					dataClient.perRifIn = testString [1];
					break;
				case 5:
					dataClient.perRifFine = testString [1];
					break;
				case 6:
					dataClient.origineDati = testString [1];
					break;
				case 7:
					dataClient.f1 = Random.Range(0,300);
					break;
				case 8:
					dataClient.f2 = Random.Range(0,300);
					break;
				case 9:
					dataClient.f3 = Random.Range(0,300);
					break;
				}
			}
			TargetSetDataClient (connectionToClient,dataClient);
		}
		TargetChekValue (connectionToClient, true);
	}

	// ritorna dati dal PHP
	string GetDataValue (string data, string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		//        value = value.Remove(value.IndexOf("|"));
		return value;
	}
}
