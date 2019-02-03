using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq.Expressions;

public class AstaPageManager : MonoBehaviour
{
    public string[] items;
    public bool checkGenerateCharacters;
    public static AstaPageManager instance;
    public string dateNormalFormat = "yyyy/MM/dd-HH:mm:ss";
    public string dateMyFormat = "yyyyMMddHHmmss";
    public List<Character> listOfCharacters;
    public int numCharactersDB;

    public enum Type
    {
        Uman,
        Beasts,
        Ghost,
        Zombie,
        Vampire
    }

    public enum Rule
    {
        Thief,
        // ladro
        Knight,
        // cavaliere
        Magician,
        Strategist,
        Druid
    }

    public struct Character
    {
        public string name;
        public int id;
        public string type;
        public string rule;
        public int vel;
        public int att;
        public int res;
        public int life;
        public int def;
        public int flag;
        public int head;
        public int body;
        public int extraBody;
        public string dateStart;
        public string dateEnd;
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
        Debug.Log("data: " + DateTime.Now.ToString(dateMyFormat));
        string myRandomName = GenerateRandomName();
        Debug.Log("nome: " + myRandomName);
//        System.DateTime.Now.ToString();
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            string myRandomName = GenerateRandomName();
            Debug.Log("nome: " + myRandomName);
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
        StartCoroutine("GetCharacters");
    }
        
    // Creata per nome più parlante,uguale a quella sopra
    IEnumerator GetCharacters()
    {
        WWW itemsData = new WWW("http://togeathosting.altervista.org/QueryAllCharacters.php");
        yield return itemsData;
        string itemsDataString = itemsData.text;
        Debug.Log(itemsDataString);
        string[] itemsDataVector = itemsDataString.Split(';');
        numCharactersDB = itemsDataVector.Length; // num characters DB
        for (int i = 0; i < itemsDataVector.Length - 1; i++)
        {
            items = itemsDataVector[i].Split('|');
            // prendo i dati in modo corretto ma pensare come fare check, una è una coroutine e non è sincronizzata
            checkGenerateCharacters = false;
//            Debug.Log("1 lunghezza: " + items.Length);
            // scandisco tutti i nomi delle mail e delle pass e controllo se almeno una fa check
            for (int j = 0; j < items.Length; j++)
            {
                string[] dataGet = items[j].Split(':');
                Debug.Log("Dato: " + dataGet[1].ToString());
            }
        }
    }

    public void GenerateListCharacters()
    {
		
    }

    public Character GenerateRandomCharacter()
    {
        Character randomCharacter = new Character();
        randomCharacter.name = GenerateRandomName();
        randomCharacter.id = 0;
        randomCharacter.type = "";
        randomCharacter.rule = "";
        randomCharacter.vel = UnityEngine.Random.Range(1, 100);
        randomCharacter.att = UnityEngine.Random.Range(1, 100);
        randomCharacter.res = UnityEngine.Random.Range(1, 100);
        randomCharacter.life = UnityEngine.Random.Range(1, 100);
        randomCharacter.flag = 1;
        randomCharacter.head = UnityEngine.Random.Range(1, 5);
        randomCharacter.body = UnityEngine.Random.Range(1, 5);
        randomCharacter.extraBody = UnityEngine.Random.Range(1, 5);
        randomCharacter.dateStart = DateTime.Now.ToString(dateMyFormat);
        // TEST AGGIUNTA DATA
        string dataString = DateTime.Now.ToString(dateMyFormat);
        long dataEndToPrint = long.Parse(dataString);
        dataEndToPrint += 240000;
        randomCharacter.dateEnd = dataEndToPrint.ToString(); // aggiungere 24 ore ma convertire in numero prima
        return randomCharacter;
    }

    public string GenerateRandomName()
    {
        int minCharAmount = 2;
        int maxCharAmount = 6;
        const string glyphs = "abcdefghijklmnopqrstuvwxyz";
        const string firstChar = "BCDFGHJKLMNPQRSTVWXZ";
        string myString = "";
        bool check;
        int charAmount = UnityEngine.Random.Range(minCharAmount, maxCharAmount); //set those to the minimum and maximum length of your string
        // inizia a generare
        for (int i = 0; i < charAmount; i++)
        {
            if (i == 0) // il primo valore deve essere maiuscolo
            {
                myString += firstChar[UnityEngine.Random.Range(0, firstChar.Length)]; 
            }
            else
            {
                check = true;
                while (check) // controllo s genera corretto se no faccio rigenerare
                {
                    string saveStartString = myString; // salvo la stringa iniziale
                    if (glyphs[UnityEngine.Random.Range(0, glyphs.Length)] != myString[i - 1]) // se lettera PRIMA UGUALE a quella generata
                    {
                        myString += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
                        check = false;
                    }
                    if (i > 2)
                    {
                        check = true;
                        if (glyphs[UnityEngine.Random.Range(0, glyphs.Length)] != myString[i - 2]) // se 2 LETTERE PRIMA UGUALE 
                        {
                            myString += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
                            check = false;
                        }
                    }
                    bool flag1 = true;
                    bool flag2 = true;
                    if (i == 1) // controllo se nella osizione 2 dEVE esserci almeno 1 VOCALE
                    {
                        check = true;
                        int countVocal = 0;
                        for (int j = 0; j < myString.Length; j++)
                        {
                            if (myString[j] == 'a' || myString[j] == 'e' || myString[j] == 'i' || myString[j] == 'o' || myString[j] == 'u')
                            {
                                countVocal++;
                            }
                        }
                        if (countVocal != 0)
                        {
                            check = false;
                        }
                        else
                        {
                            flag1 = false;
                        }
                    }
                    if (i == 5) // nella posizione 5 almeno 3 VOCALI
                    {
                        check = true;
                        int countVocal = 0;
                        for (int j = 0; j < myString.Length; j++)
                        {
                            if (myString[j] == 'a' || myString[j] == 'e' || myString[j] == 'i' || myString[j] == 'o' || myString[j] == 'u')
                            {
                                countVocal++;
                            }
                        }
                        if (countVocal != 3)
                        {
                            check = false;
                        }
                        else
                        {
                            flag2 = false;  
                        }
                    }
                    if (flag1 == false || flag2 == false)
                    {
                        myString = saveStartString;
                    }
                }
            }
        }
        return myString;
    }

    public List<Character> GenerateListOfCharacters(string[] items)
    {
        return null;
    }
}
