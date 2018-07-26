using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvisoTmpBottonGrapsh : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BackClick ()
	{
		EvisoPageManager.instance.numberButtonBills = int.Parse(gameObject.name.Replace("B",""));
		EvisoPageManager.instance.BackClick("EvisoGraph");
	}
	
		public void OpenGraphlick ()
	{
		EvisoPageManager.instance.numberButtonBills = int.Parse(gameObject.name.Replace("B",""));
		EvisoPageManager.instance.EvisoOpenGraphClick();
	}
}
