using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Register : MonoBehaviour
{
	public static Register instance;
    public GameObject username;
    public GameObject mail;
    public GameObject password;
    public GameObject confPassword;
    public GameObject buttonRegisterConfirmed;
    public GameObject buttonFacebook;
	public Text infoText;

    TouchScreenKeyboard keyboard;
    string usernameString;
    string mailString;
    string passwordString;
    string confPAsswordString;

    InputField usernameInputField;
    InputField passwordInputField;
	InputField confPasswordInputField;

    bool emailValid;
    // Use this for initialization
    void Start()
    {
		instance = this;
        usernameInputField = username.transform.GetChild(0).GetComponent<InputField>();
        passwordInputField = password.transform.GetChild(0).GetComponent<InputField>();
		confPasswordInputField = confPassword.transform.GetChild(0).GetComponent<InputField>();
		infoText.text = "";
    }
	
    // Update is called once per frame
    void Update()
    {
        usernameString = usernameInputField.text;
        passwordString = passwordInputField.text;
		confPAsswordString = confPasswordInputField.text;
        Debug.Log("il testo nome contiene " + usernameString);
        Debug.Log("il testo password contiene " + passwordString);
    }

    public void RegisterClick()
    {
		PageManager.instance.mailClient = usernameString;
		PageManager.instance.passClient = passwordString;
		if(string.Compare (passwordString, confPAsswordString) != 0){
			PrintInfoText ("PASS diverse,reinserire corrette");
		}else{
		PageManager.instance.CheckPassMailRegisterConnection ();
		}
    }


    public void BackClick()
    {
        PageManager.instance.BackClick("TogEatMain");
    }

	public void PrintInfoText(string textToPrint){
		infoText.text = "";
		infoText.text = "" + textToPrint;
	}
}
