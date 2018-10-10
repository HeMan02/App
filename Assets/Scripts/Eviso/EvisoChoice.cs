using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvisoChoice : MonoBehaviour {

	public GameObject buttomBolletta;
	public GameObject buttomAutolettura;
	public GameObject buttomNestore;
	string url = "http://10.8.0.10:3011/";
	public GameObject gameobjectExit;
	// Use this for initialization
	void Start () {
		gameobjectExit.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			gameobjectExit.SetActive (!gameobjectExit.activeSelf);
		}
	}

	public void BollettaClick ()
	{
		//		EvisoPageManager.instance.mailClient = usernameString;
		//		EvisoPageManager.instance.passClient = passwordString;
		//		EvisoPageManager.instance.CheckPassMailLogInConnection ();
		EvisoPageManager.instance.numberButtonBills = 2;
		EvisoPageManager.instance.EvisoBollettaClick();
	}

	public void AutoletturaClick ()
	{
		//		EvisoPageManager.instance.mailClient = usernameString;
		//		EvisoPageManager.instance.passClient = passwordString;
		//		EvisoPageManager.instance.CheckPassMailLogInConnection ();
		EvisoPageManager.instance.EvisoAutoletturaClick();
	}

	public void NestoreClick ()
	{
		//		EvisoPageManager.instance.mailClient = usernameString;
		//		EvisoPageManager.instance.passClient = passwordString;
		//		EvisoPageManager.instance.CheckPassMailLogInConnection ();
//		EvisoPageManager.instance.EvisoNestoreClick();
//		Application.OpenURL(url);
	}

	public void BackClick ()
	{
		//		EvisoPageManager.instance.mailClient = usernameString;
		//		EvisoPageManager.instance.passClient = passwordString;
		//		EvisoPageManager.instance.CheckPassMailLogInConnection ();
		EvisoPageManager.instance.BackClick("EvisoMain");
	}

	public void ExitApllication(){
		Application.Quit ();
	}

	public void ReturnToAppication(){
		gameobjectExit.SetActive (false);
	}
}
