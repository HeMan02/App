using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AstaPageManager : MonoBehaviour
{
    public string[] items;
    public bool checkGenerateCharacters;
    public static AstaPageManager instance;

    public struct Character
    {
        string name;
        int id;
        string type;
        string rule;
        int vel;
        int att;
        int res;
        int life;
        int def;
        int flag;
        int head;
        int body;
        int extraBody;
        string dateStart;
        string dateEnd;
    }

    void Awake()
    {
        int numGameobject = GameObject.FindGameObjectsWithTag("AstaPageManager").Length;
        if (numGameobject > 1)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
    }
	
    // Update is called once per frame
    void Update()
    {
        // controllo sul tasto di ritorno back
        if (Input.GetKey(KeyCode.Escape))
        { 
            var actualScene = SceneManager.GetActiveScene();
            switch (actualScene.name)
            { // in base a ogni scenaq torno indietro a quella che mi serve
                case "AstaMain":
                    Debug.Log("Sono nel Main non posso tornarte indietro");
                    break;
                case "AstaMarket":
                    SceneManager.LoadScene("AstaMain");
                    break;
                case "AstaMyPlayers":
                    SceneManager.LoadScene("AstaMain");
                    break;
            }
        }
    }


    public void CustomClick(string pageName)
    {
        SceneManager.LoadScene(pageName);
    }

    public void AstaLoginMarket()
    {
        CheckCharactersConnection();
        SceneManager.LoadScene("AstaMarket");
    }

    public void AstaLoginMyPlayers()
    {
        SceneManager.LoadScene("AstaMyPlayers");
    }

    public void AstaMainPage()
    {
        SceneManager.LoadScene("AstaMain");
    }

    // ================================ CONNESSIONE AL DB ===================================================

    public void CheckCharactersConnection()
    {
        Debug.Log("Funzione start coroutine");
        StartCoroutine("CheckCharacters");
    }

    IEnumerator CheckCharacters()
    {
        Debug.Log("Funzione dentro coroutine");
        WWW itemsData = new WWW("http://togeathosting.altervista.org/QueryAllCharacters.php");
        yield return itemsData;
        string itemsDataString = itemsData.text;
        items = itemsDataString.Split('|');
        // prendo i dati in modo corretto ma pensare come fare check, una è una coroutine e non è sincronizzata
        checkGenerateCharacters = false;
        Debug.Log("lunghezza: " + items.Length);
        // scandisco tutti i nomi delle mail e delle pass e controllo se almeno una fa check
        for (int i = 0; i < items.Length; i++)
        {
            string[] dataGet = items[i].Split(':');
            Debug.Log("Dato: " + dataGet[1].ToString());
//			for (int j = 0; j < dataGet.Length; j++) {
//				Debug.Log ("Dato: " + dataGet[j].ToString());
//			}
//			Debug.Log ("Dato: " + items[i].ToString());
        }
        //		Debug.Log("mailCheck " +  mailCheck + " passcheck " + passcheck);
//		if (mailCheck && passcheck) {
//			// mi vado a prendere il riferimentop o vedo come dagli l'input per settare a ok e andare avanti se no no
//			Debug.Log ("PASS GIUSTA ");
//			MainPage.instance.OpenLoginPage ();
//		} else {
//			Debug.Log ("PASS SBAGLIATA ");
//			MainPage.instance.PrintInfoText ("PASS SBAGLIATA");
//		}
    }

}
