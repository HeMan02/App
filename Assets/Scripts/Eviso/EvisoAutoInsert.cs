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
	public Text sendResult; 
	string supportInputF1;
	bool checkF1;
	bool checkF2;
	bool checkF3;
	bool checkToSend;
	bool startDissolve;
	public float t;
	// Use this for initialization
	void Start () {
		date.text = "" + System.DateTime.Now.ToString ("dd/MM/yyyy"); // visualizzo la data odierna come controlo
		f1Input.characterLimit = 7; // lunghezza massimo di inserimento valori energia
		f2Input.characterLimit = 7;
		f3Input.characterLimit = 7;
		sendResult.color = new Vector4 (0, 0, 0, 0); // setto invisibile la scritta dell'OK dei dati i nviati al DB
		sendResult.text = "SEND OK";
	}

	void FixedUpdate(){
		// facci opartire la dissolvenza della scritta dell'ok dell'invio dei dati al DB
		if (startDissolve) {
			DissolveText (sendResult);
		}
	}
	// check per controllare se hanno inserito i valori, se non inseriti devo farli inserire a loro bloccando e colorando di rossi i pulsanti 
	public bool CheckNumberToSend(InputField f1InputCheck,InputField f2InputCheck,InputField f3Inputcheck){
		// controllo sui 3 inserimento in F1,F2,F3
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
		// se tutti e 3 sono abilitati allora posso inviare i dati
		if (checkF1 || checkF2 || checkF3) {
			return false;
		} else {
			return true;
		}
	}

	// quando si preme si controlla se tutti i campi inseriti e in caso no nti fa mandare il messaggio al server
	public void SendNumbers(){
		if (CheckNumberToSend (f1Input, f2Input, f3Input)) {
			// se supera il controllo invio i valori al DB e risetto i pulsanti come predefiniti, faccio anche partire la dissolvenza
			Debug.Log ("MANDARE!!");
			f1Input.text = "";
			f1ButtonImage.color = startColor;
			f2Input.text = "";
			f2ButtonImage.color = startColor;
			f3Input.text = "";
			f3ButtonImage.color = startColor;
			t = 0;
			sendResult.color = new Vector4 (0, 0, 0, 1);
			startDissolve = true;
		} else {
			Debug.Log ("NO!!!");
		}
	}

	// ndissolve l'ok dell'invio dei dati
	public void DissolveText(Text sendResultToDissolve){
		t += 0.2f * Time.deltaTime;
		sendResultToDissolve.color = new Vector4 (0, 0, 0, Mathf.Lerp(1,0,t));
	}
}
