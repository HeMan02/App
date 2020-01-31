using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AstaMarketCharacter : MonoBehaviour
{
    public int myId;
    public List<AstaPageManager.Character> listCharactersMarket;
    public Text name;
    public Image body;
    public Image head;
    public Text bonus;
    public Text malus;
    public Text randomSkill;
    public Text type;
    public Image life;
    public Text xp;
    public Text timer;
    public DateTime dataStopMarket;
    public Text price;
    public Text myCoins;
    public InputField priceToRelance;
    public GameObject buttonToRelance;
    public int idUser;
    public int idCharacterDb;
    int actualPrice;
    int inputValue;
    int priceValue;
    int myCoinsValue;
    int valueTotCashUser;
    // Start is called before the first frame update
    void Start()
    {
        // ============ GET DATA
        idUser = int.Parse(AstaPageManager.Instance.idUser);
        myId = AstaPageManager.Instance.currentId;
        listCharactersMarket = AstaPageManager.Instance.listCharacters;
        // ============ LIST DATA
        idCharacterDb = listCharactersMarket[myId].id;
        name.text = listCharactersMarket[myId].name; // visualizzo la data odierna come controlo
        bonus.text = "" + listCharactersMarket[myId].bonus;
        malus.text = "" + listCharactersMarket[myId].malus;
        randomSkill.text = "" + listCharactersMarket[myId].randomSkill;
        type.text = "" + listCharactersMarket[myId].type;
        head.sprite = AstaPageManager.Instance.iltemHead[listCharactersMarket[myId].head];
        body.sprite = AstaPageManager.Instance.iltemBody[listCharactersMarket[myId].body];
        life.fillAmount = listCharactersMarket[myId].life;
        xp.text = "Xp: " + listCharactersMarket[myId].xp + "/100";
        // =========== REFRESH VALUE MARKET
        dataStopMarket = listCharactersMarket[myId].dataStopMarket;
        price.text = "" + listCharactersMarket[myId].price;
        myCoins.text = "$" + AstaPageManager.Instance.totalCash;
        // ======== VALUE SUPPORT
        int priceValue = int.Parse(priceToRelance.text);
        int myCoinsValue =  int.Parse(AstaPageManager.Instance.totalCash);
        if (myCoinsValue > priceValue)
        {
            buttonToRelance.SetActive(true);
        }
        else
        {
            buttonToRelance.SetActive(false);
        }
    }

    public void RefreshValueMArket()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        //   Debug.Log("" + (dataStopMarket - DateTime.Now).TotalHours);
        //   timer.text = "" + (dataStopMarket - DateTime.Now).TotalHours;$result = mysqli_query($conn,$sql);
    }

    public void ReturnClickButton()
    {
        AstaPageManager.Instance.AstaLoginMarket();
    }

    public void SetRelance()
    {

        actualPrice = int.Parse(price.text);
        priceValue = int.Parse(priceToRelance.text);
        myCoinsValue = int.Parse(AstaPageManager.Instance.totalCash);
        // Debug.Log("priceValue: " + priceValue + " > myCoinsValue: " + myCoinsValue + " --- myCoinsValue: " + myCoinsValue + " < actualPrice: " + actualPrice);
        if (priceValue > myCoinsValue || myCoinsValue < actualPrice || priceValue <= actualPrice) 
        {
            priceToRelance.text = "0";
            return;
        }
        if (myCoinsValue > priceValue)
        {
            buttonToRelance.SetActive(true);
        }
        else
        {
            buttonToRelance.SetActive(false);
            priceToRelance.text = "";
        }
        valueTotCashUser = int.Parse(AstaPageManager.Instance.totalCash) - priceValue;
        AstaPageManager.Instance.totalCash = valueTotCashUser.ToString();
        // =============== TEST
        // price.text = "" + int.Parse(priceToRelance.text);
        // myCoins.text = "$" + AstaPageManager.Instance.totalCash;
        // =============== START COROUTINE QUI SOTTO
        StartCoroutine("SendValueRelance");
    }

    IEnumerator SendValueRelance()
    {
        // Debug.Log("id: " + idCharacterDb + " idUser: " + idUser + "valueRelance: " + priceValue + " valueTotCash: " + valueTotCashUser);
        // Creo il rilancio e aggiorno il prezzo del character, con una query ad "id"
        WWWForm form = new WWWForm();
        form.AddField("idUser", idUser);
        form.AddField("id", idCharacterDb);
        form.AddField("valueRelance", priceValue);
        form.AddField("valueTotCashUser", valueTotCashUser);
        // query per aggiornare i valori su DB!!
        WWW itemsData = new WWW("http://astaapp.altervista.org/SteValueAndRefresh.php", form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        // Debug.Log(itemsDataString);
        string[] itemsDataVector = itemsDataString.Split(';');
        RefreshCharacter(itemsDataVector);
    }

    public void RefreshCharacter(string[] itemsDataVector)
    {
        for (int i = 0; i < itemsDataVector.Length - 1; i++)
        {
            string[] items = itemsDataVector[i].Split('|');
            for (int j = 0; j < items.Length; j++)
            {
                string[] dataGet = items[j].Split('@');
                if (j == 9)
                {
                    int newPrice = int.Parse(dataGet[1].ToString());
                    AstaPageManager.Character newStructForPrice = AstaPageManager.Instance.listCharacters[myId];
                    newStructForPrice.price = newPrice;
                    AstaPageManager.Instance.listCharacters[myId] = newStructForPrice;
                }
                // ====== REFRESH VALUE MARKET
                price.text = "" + listCharactersMarket[myId].price;
                myCoins.text = "$" + AstaPageManager.Instance.totalCash;
            }
        }  
        // Riaggiorno i valori di ritorno della query!!
         price.text = "" +  AstaPageManager.Instance.listCharacters[myId].price;
    }
}
