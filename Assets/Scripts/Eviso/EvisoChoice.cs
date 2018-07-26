using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvisoChoice : MonoBehaviour {

	public GameObject buttomBolletta;
	public GameObject buttomAutolettura;
	public GameObject buttomNestore;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
		EvisoPageManager.instance.EvisoNestoreClick();
	}

	public void BackClick ()
	{
		//		EvisoPageManager.instance.mailClient = usernameString;
		//		EvisoPageManager.instance.passClient = passwordString;
		//		EvisoPageManager.instance.CheckPassMailLogInConnection ();
		EvisoPageManager.instance.BackClick("EvisoMain");
	}
}
