﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EvisoNetworkObj : NetworkBehaviour {

	// non sono convinto per ORA di utilizzare una classe statica come instance perchè non ne vedo l'utilizzo
	public string ip = "192.168.1.192";
	public int port = 25001;
	public static EvisoNetworkObj ServerInstance;
	public static EvisoNetworkObj OwnerInstance;
	public static EvisoNetworkObj instance;
	public NetworkIdentity nId; // utilizzato per capire l'unico oggetto in locale utilizzatore degli script

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


	// utilizzo l'instance per generare una sola copia del server e per controllare se sono l'owner andrò a vedere se esiste l'instance
	void Awake(){
		nId = gameObject.GetComponent<NetworkIdentity> (); // mi facci orestituire il networkidentity per capire l'owner
	}
		
	void Start(){
		// ogni oggetto nuovo creato si auto setta serverinstance = this e perdo il settaggio
		if (isLocalPlayer) {
			instance = this;
//			Debug.LogError ("PIPPOOOOO LOCALEEEEEEEE");	
		}
	}

	void Update(){ // Test per vedeer a chi manda i messaggi
	}

	[Command]
	public void CmdCheckClient(string name,string password){
//		if (!isLocalPlayer)
//			return;
		Debug.LogError("-1");
		Debug.LogError ("client dice al server  il nome: " + name + " e la password " + password + " CONN: " + connectionToClient.isConnected);
//		EvisoNetworkManager.instance.mailClient = name;
//		EvisoNetworkManager.instance.passClient = password;
//		EvisoNetworkManager.instance.CheckPassMailLogInConnection ();
		CheckPassMailLogInConnection();
		// check dei valori s epresenti su DB e dopo invio del messaggio al client
	}

	// SERVER -> CLIENT risponde se password è giusta o no inserita dal client
	[TargetRpc]
	public void TargetChekValue(NetworkConnection target,bool checkCLinet){
		Debug.LogError ("4 ");
		Debug.LogError ("sono i lserver e ti rispondo che ho ceccato con : "  + checkCLinet);
		if (checkCLinet) {
		EvisoMainPage.instance.OpenLoginPage ();
		} else {
			Debug.LogError("CLIENT SBAGLIATA!!!!!!!");
	   	EvisoMainPage.instance.PrintInfoText ("PASS SBAGLIATA");
			// sbagliata fare comparire a video che sbagliata
		}
		// se vero o false vado ad aprire la scena
	}

	[ClientRpc]
	public void RpcChekValue(bool checkCLinet){
		if (!isLocalPlayer)
			return;
		Debug.LogError ("4 ");
		Debug.LogError ("sono i lserver e ti rispondo che ho ceccato con : "  + checkCLinet);
		if (checkCLinet) {
			EvisoMainPage.instance.OpenLoginPage ();
		} else {
			Debug.LogError("CLIENT SBAGLIATA!!!!!!!");
			EvisoMainPage.instance.PrintInfoText ("PASS SBAGLIATA");
			// sbagliata fare comparire a video che sbagliata
		}
		// se vero o false vado ad aprire la scena
	}

	public void ResposeLoginToClient(bool checkValue){
		Debug.LogError ("2 " + connectionToClient.isConnected);
		if (checkValue) {
			TargetChekValue (connectionToClient, true);
		} else {
			Debug.LogError ("3 " + connectionToClient.isConnected);
			TargetChekValue (connectionToClient, false);
		}
	}

	// ============================================= CHIAMATE CORIUTINE =============================================

	// TENERE TUTTO IL CODICE DENTRO L'OGGETTO CHE CHIAMA I COMMAND SE NO SI PERDE I RIFERIMENTI!!!!!
	public void CheckPassMailLogInConnection(){
		Debug.LogError("0");
		StartCoroutine ("CheckPassMailLogIn");
	}

	public void CheckPassMailRegisterConnection(){
		StartCoroutine ("CheckPassMailRegister");
	}

	// ================================================== CONNESSIONE PHP ==================================================

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
			Debug.Log ("PASS GIUSTA ");
			if(EvisoNetworkObj.ServerInstance.isLocalPlayer)
				ResposeLoginToClient (true);
		} else {
			//			Debug.Log ("PASS SBAGLIATA " + connectionToClient.isConnected);
			Debug.LogError ("1");
			ResposeLoginToClient (false);
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

	IEnumerator CiccioConnect ()
	{
		WWW itemsData = new WWW ("http://togeathosting.altervista.org/Ciccio.php");
		yield return itemsData;
	}

	string GetDataValue (string data, string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		//        value = value.Remove(value.IndexOf("|"));
		return value;
	}
}
