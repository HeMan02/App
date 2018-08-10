using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestConn : NetworkBehaviour {

	public static TestConn instance;
	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
		instance = this;
		if (isServer && instance == null) {
//			instance = this;
			SpawnObj ();
		}
	}

//	[Command]
	public void SpawnObj(){
		Debug.LogError ("SERVER SPAWN 1");
		NetworkServer.Spawn(Instantiate(Resources.Load("Network", typeof(GameObject))) as GameObject);
	}

	[Command]
	public void CmdCheckClient(string name,string password){
		//		if (!isLocalPlayer)
		//			return;
		Debug.LogError("-1");
		Debug.LogError ("client dice al server  il nome: " + name + " e la password " + password + " CONN: " + connectionToClient.isConnected);
		EvisoNetworkManager.instance.mailClient = name;
		EvisoNetworkManager.instance.passClient = password;
		EvisoNetworkManager.instance.CheckPassMailLogInConnection ();
		// check dei valori s epresenti su DB e dopo invio del messaggio al client
		//			TargetChekValue (connectionToClient,true);
		//			TargetChekValue (connectionToClient,false);
	}
}
