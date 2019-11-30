using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaMarketCharacter : MonoBehaviour
{
    public int myId;
    public Text name;
    // Start is called before the first frame update
    void Start()
    {
        name.text = "" + System.DateTime.Now.ToString ("dd/MM/yyyy"); // visualizzo la data odierna come controlo
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
