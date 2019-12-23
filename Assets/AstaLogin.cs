using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq.Expressions;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AstaLogin : MonoBehaviour
{
    public GameObject username;
	public GameObject mail;
	public GameObject password;
	public GameObject buttonLogin;
	public GameObject buttonRegister;
    public Text infoText;
	public GameObject gameobjectExit;
    string usernameString;
	string passwordString;

	InputField usernameInputField;
	InputField passwordInputField;
    // Start is called before the first frame update
    void Start()
    {
        usernameInputField = username.transform.GetChild (0).GetComponent<InputField> ();
		passwordInputField = password.transform.GetChild (0).GetComponent<InputField> ();
		infoText.text = "";
		gameobjectExit.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        // ========= INPUT
        usernameString = usernameInputField.text;
		passwordString = passwordInputField.text;
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			gameobjectExit.SetActive (!gameobjectExit.activeSelf);
		}
				Debug.Log ("il testo nome contiene " + usernameString);
				Debug.Log ("il testo password contiene " + passwordString);
    }
}
