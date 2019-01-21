using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstaMarket : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) { 
			OpenMainPage ();
		}
	}

	public void OpenMainPage(){
		AstaPageManager.instance.AstaMainPage ();
	}
}
