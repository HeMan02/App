﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstaDungeonObj : MonoBehaviour
{
    public int myId;
    public Text name;
    public Text type;
    public Text time;
    public Text description;
    public Text coinsText;
    public Sprite imageDungeon;
    public List<AstaPageManager.Dungeon> listDungeon;
    public Transform slotCharacter;
    public RectTransform targetRectTRansform;
    public bool checkOccuped = false;
    public GameObject buttonConfirmDungeon;
    public Image backgroundImage;
    public DateTime endOfOccupedDateCharacterInSLot;
    public bool startTimer;
    public int dateTimeDateGetFromDBDungeon;

    public bool CheckOccuped
    {
        get
        {
            return checkOccuped;
        }
        set
        {
            checkOccuped = value;
            if (checkOccuped)
            {
                buttonConfirmDungeon.SetActive(false);
                backgroundImage.color = new Color32(191, 191, 191, 255);
                // ==== AGGIUNGERE CODICE TIMER DA DB O MENO
                startTimer = true;
            }
            else
            {
                buttonConfirmDungeon.SetActive(true);
                backgroundImage.color = new Color32(255, 255, 255, 255);
                startTimer = false;
            }
        }
    }

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // Richiesta se è libero o meno
        listDungeon = AstaPageManager.Instance.listDungeon;
        for (int i = 0; i < listDungeon.Count; i++)
        {
            if (listDungeon[i].id == myId)
            {
                name.text = "" + listDungeon[i].name;
                this.description.text = "" + AstaPageManager.Instance.placeDungeonArray[(int)listDungeon[i].place] + AstaPageManager.Instance.descDungeonArray[(int)listDungeon[i].description] + AstaPageManager.Instance.goalDungeonArray[(int)listDungeon[i].goal];
                this.coinsText.text = "WIN: " + listDungeon[i].cashWin;
                this.type.text = "" + listDungeon[i].type;
                this.time.text = "Time: " + listDungeon[i].time + "H";
                this.name.text =  listDungeon[i].name.ToString();
                // =============== DATA PRESA DA DB
                string dateGetFromDB = listDungeon[i].dataEnd.ToString();
                dateTimeDateGetFromDBDungeon = listDungeon[i].time;
                DateTime now = DateTime.Now;
                //TimeSpan diff = dateTimeDateGetFromDBDungeon - now;
                //double hours = diff.TotalHours;
                //Debug.Log("DataEnd: " + dateTimeDateGetFromDBDungeon + " DifFromNow: " + hours);
            }
        }
        StartCoroutine("GetCharacterOnDungeon");
        AstaPageManager.Instance.StartCoroutineRefreshCharacter();
    }

    // Update is called once per frame
    void Update() {
        if (startTimer)
        {
            DateTime now = DateTime.Now;
            TimeSpan diff =  endOfOccupedDateCharacterInSLot - now;
            double hours = diff.TotalHours;
            this.time.text = "Time: " + Math.Round( hours,2)  + "H";
        }
    }

    IEnumerator GetCharacterOnDungeon()
    {
        string myUserId = AstaPageManager.Instance.idUser;
        WWWForm form = new WWWForm();
        form.AddField("idDungeon", myId);
        form.AddField("idUser", myUserId);
        WWW itemsData = new WWW("http://astaapp.altervista.org/GetCharacterOnDungeon.php", form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        Debug.Log("0 ReturnDB: " + itemsDataString);
        try
        {
            string[] itemsCharacterOnDungeonArray = itemsDataString.Split(';');
            string[] itemSplit = itemsCharacterOnDungeonArray[0].ToString().Split('@');
            //Debug.Log("Item0: " + itemSplit[0] + " item1: " + itemSplit[1] + " item2: " + itemSplit[2] + " item2: " + itemSplit[3] );
            if (string.Compare(itemSplit[0], "FREE") == 0) // Controllo se mi ritorna un valore già terminato su DB o no
            {
                int characterId = int.Parse(itemSplit[1]);
                // Lasciarlo libero e assegnargli bonus o malus finito dungeon
                GameObject[] listCharacters = GameObject.FindGameObjectsWithTag("Character");
                for (int i = 0; i < listCharacters.Length; i++)
                {
                    string[] nameSplit = listCharacters[i].name.Split('.');
                    int idCharacterObj = int.Parse(nameSplit[1]);
                    if (idCharacterObj == characterId)
                    {
                        listCharacters[i].GetComponent<AstaDungeonCharacterSelectedObj>().LifeValue -= int.Parse(itemSplit[3]); // caso con tempo finito e gli assegno malus o bonus
                        // Aggiungere denaro
                        //sbloccare character e aggiornare vita su DB

                        if (listCharacters[i].GetComponent<AstaDungeonCharacterSelectedObj>().LifeValue <= 0)
                        {
                            AstaPageManager.Instance.DeleteCharacterUser(listCharacters[i].GetComponent<AstaDungeonCharacterSelectedObj>().myId);
                            Destroy(listCharacters[i]);
                            // Qua eliminare da DB
                        }
                    }
                }
            }
            else // caso ancora occupato
            {
                Debug.Log("DEVO stampare la data");
                int characterId = int.Parse(itemSplit[1]); // num characters DB             
                SetCharacterOnSlot(characterId);
                List<AstaPageManager.Character> listUserCHR = AstaPageManager.Instance.listUserCharacters;
                
                for (int i = 0; i < listUserCHR.Count; i++)
                {
                    Debug.Log("confronto idList: " + listUserCHR[i].id + " conIDDB: " + characterId);
                    if (listUserCHR[i].id == characterId)
                    {
                        Debug.Log("data: " + listUserCHR[i].dateEndTime);
                        endOfOccupedDateCharacterInSLot = listUserCHR[i].dateEndTime;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Nessun id da assegnare al Dungeon");
        }
    }

    public void SetCharacterOnSlot(int characterIdDB)
    {
        GameObject[] listCharacters = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < listCharacters.Length; i++)
        {
            string[] nameSplit = listCharacters[i].name.Split('.');
            int idCharacterObj = int.Parse(nameSplit[1]);
            if (idCharacterObj == characterIdDB)
            {
                listCharacters[i].GetComponent<AstaDungeonCharacterSelectedObj>().isOccuped = true;
                listCharacters[i].gameObject.transform.SetParent(null);
                listCharacters[i].transform.SetParent(slotCharacter);
                listCharacters[i].transform.localPosition = new Vector3(0, 0, 0);
                RectTransform m_RectTransform = listCharacters[i].GetComponent<RectTransform>();
                m_RectTransform.anchoredPosition = new Vector2(m_RectTransform.anchoredPosition.x, m_RectTransform.sizeDelta.y);
                m_RectTransform.anchorMax = new Vector2(0, 0);
                m_RectTransform.anchorMin = new Vector2(0, 0);
            }
            this.CheckOccuped = true;
        }
    }



    public int CheckCharacterInSlot()
    {
        return 1;
    }

    public void SetCharacterOnDungeonSelected()
    {
        if (slotCharacter.transform.childCount == 0)
        {
            Debug.Log("SLOT VUOTO");
            return;
        }
        else
        {
            GameObject ogjInSLot = slotCharacter.transform.GetChild(0).gameObject;
            int idCHR = ogjInSLot.GetComponent<AstaDungeonCharacterSelectedObj>().myId;
            Debug.Log("Index CHR: " + idCHR);
            // PRENDO l'ID DEL CHARACTER ASSEGNATO!!!!
            ogjInSLot.GetComponent<AstaDungeonCharacterSelectedObj>().isOccuped = true;
            ogjInSLot.gameObject.transform.SetParent(null);
            ogjInSLot.transform.SetParent(slotCharacter);
            ogjInSLot.transform.localPosition = new Vector3(0, 0, 0);
            RectTransform m_RectTransform = ogjInSLot.GetComponent<RectTransform>();
            m_RectTransform.anchoredPosition = new Vector2(m_RectTransform.anchoredPosition.x, m_RectTransform.sizeDelta.y);
            m_RectTransform.anchorMax = new Vector2(0, 0);
            m_RectTransform.anchorMin = new Vector2(0, 0);
            this.CheckOccuped = true;
            StartCoroutine("SendValueToSet");
        }

    }

    IEnumerator SendValueToSet()
    {
        // Id Dungeon
        // Id Character 
        int idCHR = slotCharacter.transform.GetChild(0).GetComponent<AstaDungeonCharacterSelectedObj>().myId;
        int idDungeon = myId;
        Debug.Log("idDungeon: " + idDungeon + " idCharacter " + idCHR);
        WWWForm form = new WWWForm();
        form.AddField("idDungeon", idDungeon);
        form.AddField("idCharacter", idCHR);
        WWW itemsData = new WWW("http://astaapp.altervista.org/SetCharacterOnSLotDungeon.php", form);
        yield return itemsData;
        yield return new WaitForSeconds(1);
        AstaPageManager.Instance.StartCoroutineRefreshCharacter();
    }
}