using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EvisoMainPage : MonoBehaviour {

	public static EvisoMainPage instance;
	public GameObject username;
	public GameObject mail;
	public GameObject password;
	public GameObject buttonLogin;
	public GameObject buttonRegister;
	public Text infoText;


	//    TouchScreenKeyboard keyboard;
	string usernameString;
	string passwordString;

	InputField usernameInputField;
	InputField passwordInputField;

	// Use this for initialization
	void Start ()
	{
		instance = this;
		usernameInputField = username.transform.GetChild (0).GetComponent<InputField> ();
		passwordInputField = password.transform.GetChild (0).GetComponent<InputField> ();
		infoText.text = "";
	}

	// Update is called once per frame
	void Update ()
	{
		usernameString = usernameInputField.text;
		passwordString = passwordInputField.text;
		//		Debug.Log ("il testo nome contiene " + usernameString);
		//		Debug.Log ("il testo password contiene " + passwordString);
	}

	public void RegisterClick ()
	{
//		PageManager.instance.RegisterClick();
	}

	// quando si preme il lognin click in locale, solo su un oggetto viene eseguito
	public void LoginClick ()
	{
//		EvisoNetworkObj.instance.CmdCheckClient(usernameString,passwordString); // utilizzo per mandare numero random da CLIENT->SERVER e viceversa dopo in risposta
		EvisoNetworkObj.instance.CmdGetDataClient();
	}
		
	public void OpenLoginPage(){
		Debug.Log("Entrato");
		EvisoPageManager.instance.BackClick ("EvisoChoice");
	}

	public void PrintInfoText(string textToPrint){
		infoText.text = "";
		infoText.text = "" + textToPrint;
	}
}
