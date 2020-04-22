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
	public GameObject serverObj;
	public NetworkConnection connectionToClient;

	void Awake(){
	}
	// Use this for initialization
	void Start () {
//		Debug.LogError (" HEADLESS: " + IsHeadless ());
		if (IsHeadless ()) {
			StartServer ();
		} else {
			StartClient ();
		}
	}



	// Update is called once per frame
	void Update(){
//		if (Input.GetKeyDown (KeyCode.S)) {
//			StartServer ();
////			instance = this;
//		}
//		if (Input.GetKeyDown (KeyCode.C)) {
//			StartClient ();
//		}
	}
	// Quando si inizializza il SERVER!!!! 
	public override void OnStartServer(){
		Debug.LogError ("SERVER DA MANAGER");
		instance = this; // DA VEDERE!!!
	}
	// manda il messaggio una sola volta appena il client si connette al server 
	public override void OnClientConnect(NetworkConnection conn){
		base.OnClientConnect (conn);
		Debug.LogError ("client e mi sono connesso MANAGER");
	}
	// se non forzata la disconnessione dal client non viene riconosciuto
	public override void OnClientDisconnect(NetworkConnection connection){
		Debug.LogError ("client DISCONNESSO!!!!");
	}

	bool IsHeadless(){
		return SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null;
	}
}
