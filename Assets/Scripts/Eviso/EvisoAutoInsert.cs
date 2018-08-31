using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EvisoAutoInsert : MonoBehaviour {
	public Text date;
	public InputField f1Input;
	public InputField f2Input;
	public InputField f3Input;
	string supportInputF1;
	// Use this for initialization
	void Start () {
		date.text = "" + System.DateTime.Now.ToString ("dd/MM/yyyy");
		f1Input.characterLimit = 7;
		f2Input.characterLimit = 7;
		f3Input.characterLimit = 7;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("F1: " + f1Input.text + " F2: " + f2Input.text + " F3: " + f3Input.text);
		if(f1Input.text.Length > 6){
//			CheckStringNumber(f1Input.text);
		}
	}

	// da fare check!!! non funziona
//	public void CheckStringNumber(string stringToCheck){
//		for (int i = 0; i < stringToCheck.Length; i++) {
//			char value = stringToCheck [i];
//			int number = (int)stringToCheck [i];
//			Debug.Log ("NUM: " + number);
//		}
//	}
}
