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
    public float lifeValue;
    public bool occupedValue;
    public int xpValue;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("life: " + (lifeValue/100) + " Id: " + id);
        life.fillAmount = (lifeValue/100);
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
