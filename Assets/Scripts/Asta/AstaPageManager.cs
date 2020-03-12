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
    public bool checkGenerateCharacters;
    public static AstaPageManager Instance;
    public string dateNormalFormat = "yyyy/MM/dd-HH:mm:ss";
    public string dateMyFormat = "yyyyMMddHHmmss";
    public List<Character> listCharacters = new List<Character>(); // MARKET
    public List<Character> listUserCharacters = new List<Character>(); // MY CHARACTERS

    public Sprite[] iltemBody;
    public Sprite[] iltemHead;
    // =========== MARKET DATA CHARACTERS
    public int currentId = 0;
    public int numCharactersDB;
    public string[] itemsDataVector;

    public string[] items;
    // =========== USER DATA CHARACTERS
    public int currentUserId = 0;
    public int numUserCharactersDB;
    public string[] itemsUserDataVector;

    public string[] itemsUser;
    // ========== USER DATA
    public string idUser;
    public string totalCash;
    // ========== DUNGEON
    public List<Dungeon> listDungeon = new List<Dungeon>();
    public string[] itemsDungeonVector;
    public int numDungeon;
    public string[] itemsDungeon;

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

    public enum TypeDungeon
    {
        Fuoco,
        // ladro
        Stress,
        // cavaliere
        Acqua,
        Famiglia,
        Lavoro,
        Divertimento,
        Or
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
        public int price;
        public int head;
        public int body;
        public DateTime dataStopMarket;
        public int idDungeon;
    }

    public struct Dungeon
    {
        public int id;
        public int time;
        public int description;
        public TypeDungeon type;
        public int cashWin;
        public int level;
        public int name;
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
           //CheckRefreshCharacters();
        //   StartCoroutine(StartFromWait()); // Se scommentato è poi già presente in CheckRefreshCharacters()
    }

    IEnumerator StartFromWait()
    {
        yield return new WaitForSeconds(1);
        CheckCharactersConnection();
        yield return new WaitForSeconds(1);
        CheckUserCharactersConnection();
        yield return new WaitForSeconds(1);
        CheckDungeonConnection();
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
        if (listCharacters.Count > 0)
            SceneManager.LoadScene("AstaMarket");
    }

    public void AstaLoginUserListCharacters()
    {
        if (listUserCharacters.Count > 0)
            SceneManager.LoadScene("AstaUserListCharacters");
    }

    public void AstaLoginCharacterMarket()
    {
        SceneManager.LoadScene("AstaCharacter");
    }

    public void AstaLoginUserCharacter()
    {
        SceneManager.LoadScene("AstaUserCharacter");
    }

    public void AstaMainPage()
    {
        SceneManager.LoadScene("AstaMain");
    }

    public void AstaDungeon()
    {
        SceneManager.LoadScene("AstaDungeon");
    }

    // ================================ CONNESSIONE AL DB ===================================================

    public void CheckCharactersConnection()
    {
        // Debug.Log("Funzione start coroutine");
        StartCoroutine("GetCharacters");
    }

    public void CheckUserCharactersConnection()
    {
        // Debug.Log("Funzione start coroutine");
        StartCoroutine("GetUserCharacters");
    }

    public void CheckDungeonConnection()
    {
        // Debug.Log("Funzione start coroutine");
        StartCoroutine("GetDungeon");
    }

    public void CheckRefreshCharactersConnection()
    {
        // Debug.Log("Funzione start coroutine nome");
        StartCoroutine("CheckRefreshCharacters");
    }

    // Chiam i lDB e mi faccio restituire i dati dei characters
    IEnumerator GetCharacters()
    {
        WWWForm form = new WWWForm();
        form.AddField("idUser", 0);
        WWW itemsData = new WWW("http://astaapp.altervista.org/GetCharacters.php", form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        // Debug.Log(itemsDataString);
        itemsDataVector = itemsDataString.Split(';');
        numCharactersDB = itemsDataVector.Length - 1; // num characters DB
        GenerateListOfCharacters();
    }

    IEnumerator GetUserCharacters()
    {
        WWWForm form = new WWWForm();
        form.AddField("idUser", idUser);
        WWW itemsData = new WWW("http://astaapp.altervista.org/GetCharacters.php", form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        //Debug.Log(itemsDataString);
        itemsUserDataVector = itemsDataString.Split(';');
        numUserCharactersDB = itemsUserDataVector.Length - 1; // num characters DB
        GenerateListOfUserCharacters();
    }

    public void CheckRefreshCharacters()
    {
        WWWForm form = new WWWForm();
        string randomName = GenerateRandomName();
        form.AddField("name", randomName);
        form.AddField("idUser", idUser);
        WWW www = new WWW("http://astaapp.altervista.org/RefreshCharacters.php", form);
        Debug.Log("StartREfresh");
        StartCoroutine(StartFromWait());
    }

    IEnumerator GetDungeon()
    {
        WWWForm form = new WWWForm();
        form.AddField("idUser", idUser);
        WWW itemsData = new WWW("http://astaapp.altervista.org/GetDungeon.php", form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        // Debug.Log(itemsDataString);
        itemsDungeonVector = itemsDataString.Split(';');
        numDungeon = itemsDungeonVector.Length - 1; // num characters DB
        GenerateListOfDungeon();
    }

    public void GenerateListOfDungeon()
    {
        // Debug.Log("count " + itemsDataVector.Length);
        for (int i = 0; i < itemsDungeonVector.Length - 1; i++)
        {
            Dungeon newDungeon = new Dungeon();
            itemsDungeon = itemsDungeonVector[i].Split('|');
            for (int j = 0; j < itemsDungeon.Length; j++)
            {
                string[] dataGet = itemsDungeon[j].Split('@');
                if (j == 0)
                {
                    //  Debug.Log("0: " + dataGet[1].ToString());
                    newDungeon.id = int.Parse(dataGet[1].ToString());
                }
                if (j == 1)
                {
                    //   Debug.Log("1: " + dataGet[1].ToString());
                    newDungeon.time = int.Parse(dataGet[1].ToString());
                }
                if (j == 2)
                {
                    //   Debug.Log("2: " + dataGet[1].ToString());
                    newDungeon.description = int.Parse(dataGet[1].ToString());
                }
                if (j == 3)
                {
                    //   Debug.Log("3: " + dataGet[1].ToString());
                    newDungeon.type = (TypeDungeon)int.Parse(dataGet[1].ToString());
                }
                if (j == 4)
                {
                    //   Debug.Log("4: " + dataGet[1].ToString());
                    newDungeon.cashWin = int.Parse(dataGet[1].ToString());
                }
                if (j == 5)
                {
                    //   Debug.Log("5: " + dataGet[1].ToString());
                    newDungeon.level = int.Parse(dataGet[1].ToString());
                }
                if (j == 6)
                {
                    //   Debug.Log("5: " + dataGet[1].ToString());
                    newDungeon.name = int.Parse(dataGet[1].ToString());
                }
            }
            // newUserCharacter.life = 100;
            listDungeon.Add(newDungeon);
        }
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


    public void GenerateListOfCharacters()
    {
        for (int i = 0; i < itemsDataVector.Length - 1; i++)
        {
            Character newCharacter = new Character();
            items = itemsDataVector[i].Split('|');
            for (int j = 0; j < items.Length; j++)
            {
                string[] dataGet = items[j].Split('@');
                if (j == 0)
                {
                    //  Debug.Log("0: " + dataGet[1].ToString());
                    newCharacter.name = dataGet[1].ToString();
                }
                if (j == 1)
                {
                    //   Debug.Log("1: " + dataGet[1].ToString());
                    newCharacter.xp = int.Parse(dataGet[1].ToString());
                }
                if (j == 2)
                {
                    //   Debug.Log("2: " + dataGet[1].ToString());
                    newCharacter.bonus = (Bonus)int.Parse(dataGet[1].ToString());
                }
                if (j == 3)
                {
                    //   Debug.Log("3: " + dataGet[1].ToString());
                    newCharacter.malus = (Malus)int.Parse(dataGet[1].ToString());
                }
                if (j == 4)
                {
                    //   Debug.Log("4: " + dataGet[1].ToString());
                    newCharacter.randomSkill = (RandomSkill)int.Parse(dataGet[1].ToString());
                }
                if (j == 5)
                {
                    //   Debug.Log("5: " + dataGet[1].ToString());
                    newCharacter.type = (Type)int.Parse(dataGet[1].ToString());
                }
                if (j == 6)
                {
                    //   Debug.Log("6: " + dataGet[1].ToString());
                    newCharacter.head = int.Parse(dataGet[1].ToString());
                }
                if (j == 7)
                {
                    //Debug.Log("7: " + dataGet[1].ToString());
                    newCharacter.body = int.Parse(dataGet[1].ToString());
                }
                if (j == 8)
                {
                    //Debug.Log("7: " + dataGet[1].ToString());
                    DateTime dateDB = DateTime.Parse(dataGet[1].ToString());
                    // Debug.Log("DATA: " + dateDB + " DIFF: " + (DateTime.Now - dateDB).TotalHours + " +2 giorni: " + dateDB.AddDays(2));
                    string[] getDataStep = items[12].Split('@');
                    string dataStep = getDataStep[1].ToString();
                    int valueDataStep = int.Parse(dataStep);
                    newCharacter.dataStopMarket = dateDB.AddHours(valueDataStep);
                }
                if (j == 9)
                {
                    newCharacter.price = int.Parse(dataGet[1].ToString());
                }
                if (j == 10)
                {
                    newCharacter.life = int.Parse(dataGet[1].ToString());
                }
                if (j == 11)
                {
                    newCharacter.id = int.Parse(dataGet[1].ToString());
                }
            }
            // newCharacter.life = 100;
            listCharacters.Add(newCharacter);
        }
        // return listCharacters;
    }

    public void StartCoroutineRefreshCharacter()
    {
        StartCoroutine("RefreshListOfUserCharactersFromDB");
    }

    IEnumerator RefreshListOfUserCharactersFromDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("idUser", idUser);
        WWW itemsData = new WWW("http://astaapp.altervista.org/GetCharacters.php", form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        //Debug.Log(itemsDataString);
        itemsUserDataVector = null;
        itemsUserDataVector = itemsDataString.Split(';');
        numUserCharactersDB = itemsUserDataVector.Length - 1; // num characters DB
        RefreshListOfUserCharacters();
    }

    public void RefreshListOfUserCharacters()
    {
        //Debug.Log("count " + itemsDataVector.Length);
        for (int i = 0; i < itemsUserDataVector.Length - 1; i++)
        {
            Character userCharacterDB = new Character();
            itemsUser = itemsUserDataVector[i].Split('|');
            for (int j = 0; j < itemsUser.Length; j++)
            {
                
                string[] dataGet = itemsUser[j].Split('@');
                if (j == 0)
                {
                    //Debug.Log("0: " + dataGet[1].ToString());
                    userCharacterDB.name = dataGet[1].ToString();
                }
                if (j == 1)
                {
                    //Debug.Log("1: " + dataGet[1].ToString());
                    userCharacterDB.xp = int.Parse(dataGet[1].ToString());
                }
                if (j == 2)
                {
                    //Debug.Log("2: " + dataGet[1].ToString());
                    userCharacterDB.bonus = (Bonus)int.Parse(dataGet[1].ToString());
                }
                if (j == 3)
                {
                    //Debug.Log("3: " + dataGet[1].ToString());
                    userCharacterDB.malus = (Malus)int.Parse(dataGet[1].ToString());
                }
                if (j == 4)
                {
                    //Debug.Log("4: " + dataGet[1].ToString());
                    userCharacterDB.randomSkill = (RandomSkill)int.Parse(dataGet[1].ToString());
                }
                if (j == 5)
                {
                    //Debug.Log("5: " + dataGet[1].ToString());
                    userCharacterDB.type = (Type)int.Parse(dataGet[1].ToString());
                }
                if (j == 6)
                {
                    //Debug.Log("6: " + dataGet[1].ToString());
                    userCharacterDB.head = int.Parse(dataGet[1].ToString());
                }
                if (j == 7)
                {
                    //Debug.Log("7: " + dataGet[1].ToString());
                    userCharacterDB.body = int.Parse(dataGet[1].ToString());
                }
                if (j == 10)
                {
                    //Debug.Log("10: " + dataGet[1].ToString());
                    userCharacterDB.life = int.Parse(dataGet[1].ToString());
                }
                if (j == 11)
                {
                    //Debug.Log("11: " + dataGet[1].ToString());
                    userCharacterDB.id = int.Parse(dataGet[1].ToString());
                }
                if (j == 13)
                {
                    //Debug.Log("12: " + dataGet[1].ToString());
                    userCharacterDB.idDungeon = int.Parse(dataGet[1].ToString());
                } 
            }
            for (int x = 0; x < listUserCharacters.Count; x++)
            {
                //Debug.Log("confronto da: " + listUserCharacters[x].id + " con: " + userCharacterDB.id);
                if (listUserCharacters[x].id == userCharacterDB.id)
                {
                    //Debug.Log("sostituzione");
                    //listUserCharacters[x] = void;
                    listUserCharacters[x] = userCharacterDB;
                }
            }
        }
    }

    public void GenerateListOfUserCharacters()
    {
        //Debug.Log("count " + itemsDataVector.Length);
        for (int i = 0; i < itemsUserDataVector.Length - 1; i++)
        {
            Character newUserCharacter = new Character();
            itemsUser = itemsUserDataVector[i].Split('|');
            for (int j = 0; j < itemsUser.Length; j++)
            {
                string[] dataGet = itemsUser[j].Split('@');
                if (j == 0)
                {
                    //Debug.Log("0: " + dataGet[1].ToString());
                    newUserCharacter.name = dataGet[1].ToString();
                }
                if (j == 1)
                {
                    //Debug.Log("1: " + dataGet[1].ToString());
                    newUserCharacter.xp = int.Parse(dataGet[1].ToString());
                }
                if (j == 2)
                {
                    //Debug.Log("2: " + dataGet[1].ToString());
                    newUserCharacter.bonus = (Bonus)int.Parse(dataGet[1].ToString());
                }
                if (j == 3)
                {
                    //Debug.Log("3: " + dataGet[1].ToString());
                    newUserCharacter.malus = (Malus)int.Parse(dataGet[1].ToString());
                }
                if (j == 4)
                {
                    //Debug.Log("4: " + dataGet[1].ToString());
                    newUserCharacter.randomSkill = (RandomSkill)int.Parse(dataGet[1].ToString());
                }
                if (j == 5)
                {
                    //Debug.Log("5: " + dataGet[1].ToString());
                    newUserCharacter.type = (Type)int.Parse(dataGet[1].ToString());
                }
                if (j == 6)
                {
                    //Debug.Log("6: " + dataGet[1].ToString());
                    newUserCharacter.head = int.Parse(dataGet[1].ToString());
                }
                if (j == 7)
                {
                    //Debug.Log("7: " + dataGet[1].ToString());
                    newUserCharacter.body = int.Parse(dataGet[1].ToString());
                }
                if (j == 10)
                {
                    //Debug.Log("10: " + dataGet[1].ToString());
                    newUserCharacter.life = int.Parse(dataGet[1].ToString());
                }
                if (j == 11)
                {
                    //Debug.Log("11: " + dataGet[1].ToString());
                    newUserCharacter.id = int.Parse(dataGet[1].ToString());
                }
                if (j == 13)
                {
                    //Debug.Log("12: " + dataGet[1].ToString());
                    newUserCharacter.idDungeon = int.Parse(dataGet[1].ToString());
                }
            }
            // newUserCharacter.life = 100;
            if (newUserCharacter.life > 0)
                listUserCharacters.Add(newUserCharacter);
        }
        // return listUserCharacters;
    }

    /// <summary>
    /// Funzione per richiamare bonus o malus ai character finiti dungeon
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public void AddBonusCharacter(int id,string type,int value)
    {
        for (int i = 0; i < listUserCharacters.Count; i++)
        {
            if (listUserCharacters[i].id == id)
            {
                // BONUS
                switch (type)
                {
                    case "LoseLife": 
                        // Rimuovo vita
                        break;
                    case "AddLife":
                        // Rimuovo vita
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Aggiungo o rimuovo coins
    /// </summary>
    public void AddCoins(int value)
    {
        // iduser
        totalCash += value;
        StartCoroutine("SetCoins");
    }

    /// <summary>
    /// Aggiungo cash su DB
    /// </summary>
    /// <returns></returns>
    IEnumerator SetCoins()
    {
        WWWForm form = new WWWForm();
        form.AddField("idUser", idUser);
        form.AddField("totalCash", totalCash);
        //WWW itemsData = new WWW("http://astaapp.altervista.org/GetCharacters.php", form);
        //yield return itemsData;
        yield return null;
    }

    /// <summary>
    /// Elimino character con vita a 0
    /// </summary>
    /// <param name="idCharacterUser"></param>
    public void DeleteCharacterUser(int idCharacterUser)
    {
        for (int i = 0; i < listUserCharacters.Count; i++)
        {
            if (listUserCharacters[i].id == idCharacterUser)
            {
                listUserCharacters.Remove(listUserCharacters[i]);
            }
        }
    }
}
