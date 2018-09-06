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
	public string CreateUserUrl = "http://togeathosting.altervista.org/Insert.php";
	public string AddReadingsUrl = "http://togeathosting.altervista.org/InsertAutoEnergy.php";
	// per ora non li uso, se servissero sono qua
	public string podClient;
	public string f1Client;
	public string f2Client;
	public string f3Client;


	void Start(){
		// Tengo i nlocale solo la versione proprietaria del codice, sul server ovviamente ci saranno tutte le copie
		if (isLocalPlayer) { // se sono in locale mi setto l'istanza e me l osalvo come indistruttibile in caso cambi scena
			instance = this;
			DontDestroyOnLoad (this);
		} else if (!isServer) { // Se non sono server e non sono il propietario l odistruggo
			Debug.LogError ("INTRUSO!!!! E LO DISTRUGGO");
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

	// in teoria non dovrebbe servire assegnare altre variabile ma passargli già le variabili da inserire perchè controllo effettuato in locale
	[Command]
	public void CmdAddAutoReadings(string pod, string f1, string f2, string f3,string data){
		Debug.LogError ("POD: " + pod + " F1: " + f1 + " F2: " + f2 + "F3" + f3 );
//		podClient = pod;
//		f1Client = f1;
//		f2Client = f2;
//		f3Client = f3;
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
	// TENERE TUTTO IL CODICE DENTRO L'OGGETTO CHE CHIAMA I COMMAND SE NO SI PERDE I RIFERIMENTI!!!!!
	// ================================================== CONNESSIONE PHP ==================================================
	// controlla se password presente sul DB e ritorna il check 
	IEnumerator CheckPassMailLogIn ()
	{
		WWW itemsData = new WWW ("http://togeathosting.altervista.org/Query.php");
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
				Debug.Log("MailClient " +  mailClient + mailClient.Length + " == " + mail + mail.Length);
				if (string.Compare (mailClient, mail) == 0) {
					mailCheck = true;
				}
			}
			string[] passTotal = mailAndPass[1].Split (':');
			string pass = null;
			if (passTotal [1] != null) {
				pass = passTotal [1];
				Debug.Log("PassClient " +  passClient + " == " + pass);
				if (string.Compare (passClient, pass) == 0) {
					passcheck = true;
				}
			}
		}
		if (mailCheck && passcheck) {
			// mi vado a prendere il riferimentop o vedo come dagli l'input per settare a ok e andare avanti se no no
			TargetChekValue (connectionToClient, true);
		} else {
			//			Debug.Log ("PASS SBAGLIATA " + connectionToClient.isConnected);
			TargetChekValue (connectionToClient, false);
		}
	}

	// se utente già presente non ti registra di nuovo, se  no ti aggiunge
	IEnumerator CheckPassMailRegister ()
	{
		WWW itemsData = new WWW ("http://togeathosting.altervista.org/Query.php");
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
		
	// ritorna dati dal PHP
	string GetDataValue (string data, string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		//        value = value.Remove(value.IndexOf("|"));
		return value;
	}
}
