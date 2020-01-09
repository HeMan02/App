using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AstaCharaacterMarketList : MonoBehaviour
{
    public int id;
    public Text name;
    public Image icon;
    public DateTime dataStopMarket;
    public Text timer;
    // Start is called before the first frame update
    void Start()
    {
        // dataStopMarket = listCharactersMarket[myId].dataStopMarket;
    }

    // Update is called once per frame
    void Update()
    {
       timer.text = "" + (dataStopMarket - DateTime.Now).ToString("h'h 'm'm 's's '");
    }
}
