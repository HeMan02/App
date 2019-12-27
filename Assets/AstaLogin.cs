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
    public string[] items;
    // Start is called before the first frame update
    void Start()
    {
        usernameInputField = username.transform.GetChild(0).GetComponent<InputField>();
        passwordInputField = password.transform.GetChild(0).GetComponent<InputField>();
        infoText.text = "";
        gameobjectExit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ========= INPUT
        usernameString = usernameInputField.text;
        passwordString = passwordInputField.text;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameobjectExit.SetActive(!gameobjectExit.activeSelf);
        }
        // Debug.Log ("il testo nome contiene " + usernameString);
        // Debug.Log ("il testo password contiene " + passwordString);
    }
    // ================================================== CONNESSIONE PHP ==================================================
    // controlla se password presente sul DB e ritorna il check 
    IEnumerator CheckPassMailLogIn()
    {
        // chiamata della query per check di user e password
        Debug.Log("0: " + usernameString + " psw: " + passwordString);
        WWWForm form = new WWWForm();
        form.AddField("name", usernameString);
        form.AddField("pwd", passwordString);
        WWW itemsData = new WWW("http://astaapp.altervista.org/CheckLogin.php", form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        items = itemsDataString.Split(';');
        // Controllo se la query mi ritorna valori e me li vado a settare
        if (items.Length == 2)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                string[] userAndPass = items[i].Split('|');

                for (int j = 0; j < userAndPass.Length; j++)
                {
                    string[] userTotal = userAndPass[j].Split('@');
                    if (j == 0)
                    {
                        AstaPageManager.Instance.userId = userTotal[1];
                    }
                    if (j == 3)
                    {
                        AstaPageManager.Instance.totalCash = userTotal[1];
                    }
                }
            }
			AstaPageManager.Instance.AstaMainPage();
        }
        else
        {
			// NON HO TROVATO NESSUNO 

        }
    }

    public void StartCheckLogin()
    {
        Debug.Log("Funzione start coroutine");
        StartCoroutine("CheckPassMailLogIn");
    }
}
