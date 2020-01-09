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
    // Start is called before the first frame update
    void Start()
    {
        // Setto tutti i parametri del CHR
        myId = AstaPageManager.Instance.currentId;
        listCharactersMarket = AstaPageManager.Instance.listCharacters;
        name.text = listCharactersMarket[myId].name; // visualizzo la data odierna come controlo
        bonus.text = "" + listCharactersMarket[myId].bonus;
        malus.text = "" + listCharactersMarket[myId].malus;
        randomSkill.text = "" + listCharactersMarket[myId].randomSkill;
        type.text = "" + listCharactersMarket[myId].type;
        head.sprite = AstaPageManager.Instance.iltemHead[ listCharactersMarket[myId].head];
        body.sprite = AstaPageManager.Instance.iltemBody[ listCharactersMarket[myId].body];
        life.fillAmount = listCharactersMarket[myId].life;
        xp.text = "Xp: " + listCharactersMarket[myId].xp + "/100";
        dataStopMarket = listCharactersMarket[myId].dataStopMarket;
    }

    // Update is called once per frame
    void Update()
    {
      Debug.Log("" + (dataStopMarket - DateTime.Now).TotalHours);
      timer.text = "" + (dataStopMarket - DateTime.Now).TotalHours;
    }

    public void ReturnClickButton()
    {
        AstaPageManager.Instance.AstaLoginMarket();
    }
}
