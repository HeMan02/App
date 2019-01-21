using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstaMainPage : MonoBehaviour {

	public static AstaMainPage instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenLoginPage(){
		Debug.Log("Entrato ");
		AstaPageManager.instance.CustomClick ("EvisoChoice");
	}

	public void OpenMyPlayersPage(){
		Debug.Log("Entrato My Players");
		AstaPageManager.instance.AstaLoginMyPlayers ();
	}

	public void OpenMarketPage(){
		Debug.Log("Entrato Market");
		AstaPageManager.instance.AstaLoginMarket ();
	}
}
