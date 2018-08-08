using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EvisoNetworkObj : NetworkBehaviour {

	// non sono convinto per ORA di utilizzare una classe statica come instance perchè non ne vedo l'utilizzo
	public string ip = "192.168.1.192";
	public int port = 25001;
	public static EvisoNetworkObj ServerInstance;
	public static EvisoNetworkObj OwnerInstance;
	public NetworkIdentity nId; // utilizzato per capire l'unico oggetto in locale utilizzatore degli script

	// utilizzo l'instance per generare una sola copia del server e per controllare se sono l'owner andrò a vedere se esiste l'instance
	void Awake(){
		nId = gameObject.GetComponent<NetworkIdentity> (); // mi facci orestituire il networkidentity per capire l'owner
//		if (isServer) {
//			Debug.LogError ("SERVERRRRRRRRRRRRRRRRRRRR");
//			ServerInstance = this;
//		} else {
//			Debug.LogError ("OWNEEERRRRRRRRRRR");
//			OwnerInstance = this;
//		}
//		if (nId.isServer) {
//			Debug.LogError ("SERVERRRRRRRRRRRRRRRRRRRR   IDDDDDD");
//			ServerInstance = this;
//		} else {
//			Debug.LogError ("OWNEEERRRRRRRRRRR   IDDDDD");
//			OwnerInstance = this;
//		}
	}

	// Chiamato dall'oggetto sul server quando viene inzializzato 
	public override void OnStartServer(){
		if (nId.isLocalPlayer) {
			Debug.LogError ("SERVER da networkOBJ!!");
		}
	}
	// chiamato da tutti gli obj quando inizializzato
	public override void OnStartClient(){
		if (nId.isLocalPlayer) {
			Debug.LogError ("Client da networkOBJ!!");
		}
	}

	void Start(){
		if (EvisoNetworkManager.instance && nId.serverOnly) { // =============== CONTROLL OSPORCO PERO MI IDENTIVICA L'UNICA VERSIONE SU SERVER !!! DA MIGLIORARE! ===============
			Debug.LogError ("Server OBJ");
		} else if(nId.isLocalPlayer){
			Debug.LogError ("client OBJ");
		}
	}

	void Update(){ // Test per vedeer a chi manda i messaggi
		if (Input.GetKeyDown (KeyCode.T) && isServer) {
			RpcServerToClient (); 
		}
	}

	// Messaggio dal Server ai clients , problema che ricevono multi messaggi!!
	[ClientRpc]
	void RpcServerToClient(){
		if(isLocalPlayer)
			Debug.LogError ("Server scrive al Client");
	}

	// Messaggio dal Client al server
	[Command]
	void CmdClientToServer(){
		Debug.LogError ("Client scrive al Server");
	}

	// utilizzata per rispondere come test con numero random
	[Command]
	void CmdPrintNum(int num){
		Debug.LogError ("client dice al server  il num: " + num);
		TargetResposeToClient (connectionToClient,num);
	}

	[TargetRpc]
	public void TargetResposeToClient(NetworkConnection target,int num){
		// rispondo al client per il numero random, utilizzato come prova
		Debug.LogError ("sono i lserver e ti rispondo,ho ricevuto il tuo num : " + num);
	}

	[Command]
	public void CmdCheckClient(string name,string password){
		Debug.LogError ("client dice al server  il nome: " + name + " e la password " + password);
		EvisoNetworkManager.instance.mailClient = name;
		EvisoNetworkManager.instance.passClient = password;
		EvisoNetworkManager.instance.CheckPassMailLogInConnection ();
		// check dei valori s epresenti su DB e dopo invio del messaggio al client
//			TargetChekValue (connectionToClient,true);
//			TargetChekValue (connectionToClient,false);
	}

	[TargetRpc]
	public void TargetChekValue(NetworkConnection target,bool checkCLinet){
		Debug.LogError ("sono i lserver e ti rispondo che ho ceccato con : "  + checkCLinet);
		// se vero o false vado ad aprire la scena
//		EvisoPageManager.instance.EvisoChoiceClick();
	}

	public void ResposeLoginToClient(bool checkValue){
		if (checkValue) {
			TargetChekValue (connectionToClient, true);
		} else {
			TargetChekValue (connectionToClient, false);
		}
	}
}
