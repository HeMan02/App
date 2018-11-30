using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EvisoAutoGas : MonoBehaviour {

	public Text date;
	public InputField fTotInput;
	public Color startColor;
	public Color stopColor;
	public Text sendResult; 
	public Image fButtonImage;
	string supportInputF1;
	bool checkF;
	bool checkToSend;
	bool startDissolve;
	public float t;
	// Use this for initialization
	void Start () {
		date.text = "" + System.DateTime.Now.ToString ("dd/MM/yyyy"); // visualizzo la data odierna come controlo
		fTotInput.characterLimit = 7; // lunghezza massimo di inserimento valori energia
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
	public bool CheckNumberToSend(InputField fTotInput){
		// controllo su FTOT
		if (fTotInput.text.Length == 0) {
			checkF = true;
			fButtonImage.color = stopColor;
		} else {
			checkF = false;
			fButtonImage.color = startColor;
		}
		// se tutti e 3 sono abilitati allora posso inviare i dati
		if (checkF) {
			return false;
		} else {
			return true;
		}
	}

	// quando si preme si controlla se tutti i campi inseriti e in caso no nti fa mandare il messaggio al server
	public void SendNumbers(){
		if (CheckNumberToSend (fTotInput)) {
			// se supera il controllo invio i valori al DB e risetto i pulsanti come predefiniti, faccio anche partire la dissolvenza
//			EvisoNetworkObj.instance.CmdAddAutoReadings ("IT001E12345678",f1Input.text,f2Input.text,f3Input.text,date.text);// DA MODIFICARE per pushar esu tabella ma con una sola Ftot
			Debug.Log ("MANDARE!!");
			fTotInput.text = "";
			fButtonImage.color = startColor;
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
