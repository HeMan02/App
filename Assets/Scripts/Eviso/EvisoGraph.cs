using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvisoGraph : MonoBehaviour {

	public Image fillamountBarF1;
	public Image fillamountBarF2;
	public Image fillamountBarF3;
	public Text textF1;
	public Text textF2;
	public Text textF3;
	float fillAmountF1;
	float fillAmountF2;
	float fillAmountF3;
	// Use this for initialization
	void Start () {
//		Debug.Log (EvisoPageManager.instance.numberButtonBills);
//		Debug.Log (EvisoPageManager.instance.graphList.Count);
		fillamountBarF1.fillAmount = 0;
		fillamountBarF2.fillAmount = 0;
		fillamountBarF3.fillAmount = 0;
		textF1.text = EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f1.ToString("F1") + " kW";
		textF2.text = EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f2.ToString("F1") + " kW";
		textF3.text = EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f3.ToString("F1") + " kW";
		GenerateValueChar ();
	}
	
	// Update is called once per frame
	void Update () {
		fillamountBarF1.fillAmount = Mathf.Lerp (fillamountBarF1.fillAmount, fillAmountF1, 1 * Time.deltaTime);
		fillamountBarF2.fillAmount = Mathf.Lerp (fillamountBarF2.fillAmount, fillAmountF2, 1 * Time.deltaTime);
		fillamountBarF3.fillAmount = Mathf.Lerp (fillamountBarF3.fillAmount, fillAmountF3, 1 * Time.deltaTime);
	}

	public void BackClick ()
	{
		//		EvisoPageManager.instance.mailClient = usernameString;
		//		EvisoPageManager.instance.passClient = passwordString;
		//		EvisoPageManager.instance.CheckPassMailLogInConnection ();
		EvisoPageManager.instance.BackClick("EvisoBollettaMain");
	}

	void GenerateValueChar(){
//		EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f1
		float totPow = EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f1 + EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f2 + EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f3;
		fillAmountF1 = EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f1 / totPow;
		fillAmountF2 = EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f2 / totPow;
		fillAmountF3 = EvisoPageManager.instance.graphList [EvisoPageManager.instance.numberButtonBills].f3 / totPow;
	}
}
