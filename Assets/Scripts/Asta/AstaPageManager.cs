using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AstaPageManager : MonoBehaviour {

	public static AstaPageManager instance;
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		// controllo sul tasto di ritorno back
		if (Input.GetKey (KeyCode.Escape)) { 
			var actualScene = SceneManager.GetActiveScene ();
			switch (actualScene.name) { // in base a ogni scenaq torno indietro a quella che mi serve
			case "AstaMain":
				Debug.Log ("Sono nel Main non posso tornarte indietro");
				break;
			case "AstaMarket":
				SceneManager.LoadScene("AstaMain");
				break;
			case "AstaMyPlayers":
				SceneManager.LoadScene("AstaMain");
				break;
			}
		}
	}


	public void CustomClick(string pageName)
	{
		SceneManager.LoadScene(pageName);
	}

	public void AstaLoginMarket()
	{
		SceneManager.LoadScene("AstaMarket");
	}

	public void AstaLoginMyPlayers()
	{
		SceneManager.LoadScene("AstaMyPlayers");
	}
}
