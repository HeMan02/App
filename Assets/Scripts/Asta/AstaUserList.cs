using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AstaUserList : MonoBehaviour
{
    public int id;
    public Text name;
    public Image icon;
    public Image life;
    public Image occuped;
    public Image xp;
    public int lifeValue;
    public bool occupedValue;
    public int xpValue;

    // Start is called before the first frame update
    void Start()
    {
        life.fillAmount = lifeValue;
        if(occupedValue){
            occuped.color = Color.red;
        }else{
            occuped.color = Color.green;
        }
        xp.fillAmount = xpValue; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
