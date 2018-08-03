using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class EvisoNetworkManager : NetworkManager {

	public string[] items;
	public bool checkToEnter = false;
	public string itemsDataString;
	public string passClient = null;
	public string mailClient = null;
	public string passToConfirmClient = null;
	bool mailCheck = false;
	bool passcheck = false;
	public string CreateUserUrl = "http://togeathosting.altervista.org/Insert.php";
	public static EvisoNetworkManager instance;

	void Awake(){
	}
	// Use this for initialization
	void Start () {
		instance = this;
	}
	// Update is called once per frame
	void Update(){
		if (Input.GetKeyDown (KeyCode.S)) {
			StartServer ();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			StartClient ();
		}
	}
	// Quando si inizializza il SERVER!!!! 
	public override void OnStartServer(){
		//		server = this;
		Debug.LogError ("SERVER DA MANAGER");
	}
	// manda il messaggio una sola volta appena il client si connette al server 
	public override void OnClientConnect(NetworkConnection conn){
		base.OnClientConnect (conn);
		Debug.LogError ("client e mi sono connesso MANAGER");
	}


	// ================================================== MANAGER CONNESSIONE PHP ==================================================
	// per ora copiato da EvisoPageManager!! da arrangiare riferimenti password ecc

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
			Debug.Log ("PASS GIUSTA ");
			MainPage.instance.OpenLoginPage ();
		} else {
			Debug.Log ("PASS SBAGLIATA ");
			MainPage.instance.PrintInfoText ("PASS SBAGLIATA");
		}
	}


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

	//  INSERT
	public void CreateUser(string mail, string pass){
		WWWForm form = new WWWForm();
		form.AddField("mailclientPost",mail);
		form.AddField("passClientPost",pass);
		WWW www = new WWW (CreateUserUrl, form);
	}

	// ============================================= CHIAMATE CORIUTINE =============================================

	public void CheckPassMailLogInConnection(){
		StartCoroutine ("CheckPassMailLogIn");
	}

	public void CheckPassMailRegisterConnection(){
		StartCoroutine ("CheckPassMailRegister");
	}
}
