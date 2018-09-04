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
	public Image f1ButtonImage;
	public Image f2ButtonImage;
	public Image f3ButtonImage;
	public Color startColor;
	public Color stopColor;
	string supportInputF1;
	bool checkF1;
	bool checkF2;
	bool checkF3;
	bool checkToSend;
	// Use this for initialization
	void Start () {
		date.text = "" + System.DateTime.Now.ToString ("dd/MM/yyyy");
		f1Input.characterLimit = 7;
		f2Input.characterLimit = 7;
		f3Input.characterLimit = 7;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("F1: " + f1Input.text + " F2: " + f2Input.text + " F3: " + f3Input.text);
//		Debug.Log ("F1: " + f1Input.text.Length + " F2: " + f2Input.text.Length + " F3: " + f3Input.text.Length);
//		if(f1Input.text.Length == 0){
//			CheckStringNumber(f1Input.text);
//		}
	}

	// check per controllare se hanno inserito i valori, se non inseriti devo farli inserire a loro bloccando e colorando di rossi i pulsanti 
	public bool CheckNumberToSend(InputField f1InputCheck,InputField f2InputCheck,InputField f3Inputcheck){
		if (f1InputCheck.text.Length == 0) {
			checkF1 = true;
			f1ButtonImage.color = stopColor;
		} else {
			checkF1 = false;
			f1ButtonImage.color = startColor;
		}
		if (f2InputCheck.text.Length == 0) {
			checkF2 = true;
			f2ButtonImage.color = stopColor;
		} else {
			checkF2 = false;
			f2ButtonImage.color = startColor;
		}
		if (f3Inputcheck.text.Length == 0) {
			checkF3 = true;
			f3ButtonImage.color = stopColor;
		} else {
			checkF3 = false;
			f3ButtonImage.color = startColor;
		}
		if (checkF1 || checkF2 || checkF3) {
			return false;
		} else {
			return true;
		}
	}

	// quando si preme si controlla se tutti i campi inseriti e in caso no nti fa mandare il messaggio al server
	public void SendNumbers(){
		if (CheckNumberToSend (f1Input, f2Input, f3Input)) {
			Debug.Log ("MANDARE!!");
		} else {
			Debug.Log ("NO!!!");
		}
	}
}
