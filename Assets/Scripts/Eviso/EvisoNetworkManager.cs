using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class EvisoNetworkManager : NetworkManager {

	void Awake(){
	}
	// Use this for initialization
	void Start () {

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
}
