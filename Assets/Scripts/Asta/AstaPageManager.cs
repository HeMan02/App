using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq.Expressions;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaPageManager : MonoBehaviour
{
    // ========== CHARACTERS
    public string[] items;
    public bool checkGenerateCharacters;
    public static AstaPageManager Instance;
    public string dateNormalFormat = "yyyy/MM/dd-HH:mm:ss";
    public string dateMyFormat = "yyyyMMddHHmmss";
    public List<Character> listCharacters = new List<Character>();
    public int numCharactersDB;
    public int currentId = 0;

    public Sprite[] iltemBody;
    public Sprite[] iltemHead;
    public string[] itemsDataVector;

    public enum Type
    {
        Ingegnere,
        Bestia,
        Fantasma,
        Zombie,
        Vampiro,
        Architetto,
        Muratore,
        Fabbro,
        Cavaliere,
        Postino
    }

    public enum Bonus
    {
        Coraggioso,
        // ladro
        Resistente,
        // cavaliere
        Intelligente,
        Veloce,
        Leader,
        Combattimento,
        Sincero,
        Curativo,
    }

    public enum Malus
    {
        Analfabeta,
        // ladro
        Allergico,
        // cavaliere
        Disonesto,
        Pauroso,
        Logorroico,
        Ladro,
        Disattento,
        Brutto,
        Puzza
    }

    public enum RandomSkill
    {
        Artista,
        // ladro
        Musicista,
        // cavaliere
        Borseggiatore,
        Nascondersi,
        Vegano,
        Vola,
        Danza
        
    }

    public struct Character
    {
        public string name;
        public int id;
        public Type type;
        public Bonus bonus;
        public Malus malus;
        public RandomSkill randomSkill;
        public int xp;
        public int life;
        public int head;
        public int body;
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
        Instance = this; 
        // ======= CHARACTERS
        // CheckRefreshCharactersConnection();
        CheckRefreshCharacters();
        StartCoroutine(StartFromWait());
    }

    IEnumerator StartFromWait(){
yield return new WaitForSeconds(3);
CheckCharactersConnection();
    }

    // Update is called once per frame
    void Update()
    {
        // ========= NAVIGATION 
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
                case "AstaCharacter":
                    SceneManager.LoadScene("AstaMarket");
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
        if(listCharacters.Count>0)
        SceneManager.LoadScene("AstaMarket");
    }

    public void AstaLoginMyPlayers()
    {
        SceneManager.LoadScene("AstaMyPlayers");
    }

    public void AstaLoginCharacterMarket()
    {
        SceneManager.LoadScene("AstaCharacter");
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

        public void CheckRefreshCharactersConnection()
    {
        Debug.Log("Funzione start coroutine nome");
        StartCoroutine("CheckRefreshCharacters");
    }

    // Chiam i lDB e mi faccio restituire i dati dei characters
    IEnumerator GetCharacters()
    {
        WWW itemsData = new WWW("http://astaapp.altervista.org/GetCharacters.php");
        yield return itemsData;
        string itemsDataString = itemsData.text;
        Debug.Log(itemsDataString);
        itemsDataVector = itemsDataString.Split(';');
        numCharactersDB = itemsDataVector.Length - 1; // num characters DB
        GenerateListOfCharacters();
    }

    public void CheckRefreshCharacters(){
        WWWForm form = new WWWForm();
        string randomName = GenerateRandomName();   
	    form.AddField("name",randomName);
		WWW www = new WWW ("http://astaapp.altervista.org/RefreshCharacters.php", form);
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


    public List<Character> GenerateListOfCharacters()
    {
        Debug.Log("count " + itemsDataVector.Length);
        for (int i = 0; i < itemsDataVector.Length - 1; i++)
        {
            Character newCharacter = new Character();
            items = itemsDataVector[i].Split('|');
            for (int j = 0; j < items.Length; j++)
            {
                string[] dataGet = items[j].Split('@');
                if (j == 0)
                {
                    Debug.Log("0: " + dataGet[1].ToString());
                    newCharacter.name = dataGet[1].ToString();
                }
                if (j == 1)
                {
                     Debug.Log("1: " + dataGet[1].ToString());
                    newCharacter.xp = int.Parse(dataGet[1].ToString());
                }
                if (j == 2)
                {
                     Debug.Log("2: " + dataGet[1].ToString());
                    newCharacter.bonus = (Bonus)int.Parse(dataGet[1].ToString());
                }
                if (j == 3)
                {
                     Debug.Log("3: " + dataGet[1].ToString());
                    newCharacter.malus = (Malus)int.Parse(dataGet[1].ToString());
                }
                if (j == 4)
                {
                     Debug.Log("4: " + dataGet[1].ToString());
                    newCharacter.randomSkill = (RandomSkill)int.Parse(dataGet[1].ToString());
                }
                if (j == 5)
                {
                     Debug.Log("5: " + dataGet[1].ToString());
                    newCharacter.type = (Type)int.Parse(dataGet[1].ToString());
                }
                if (j == 6)
                {
                     Debug.Log("6: " + dataGet[1].ToString());
                    newCharacter.head  = int.Parse(dataGet[1].ToString()); 
                }
                 if (j == 7)
                {
                     Debug.Log("7: " + dataGet[1].ToString());
                    newCharacter.body = int.Parse(dataGet[1].ToString());
                }
            }
            newCharacter.life = 100;
            listCharacters.Add(newCharacter);
        }
        return listCharacters;
    }
}
