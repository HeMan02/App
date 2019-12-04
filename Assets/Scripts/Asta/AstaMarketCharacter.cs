using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaMarketCharacter : MonoBehaviour
{
    public int myId;
    public List<AstaPageManager.Character> listCharactersMarket;
    public Text name;
    public Image body;
    public Image head;
    // Start is called before the first frame update
    void Start()
    {
        // Setto tutti i parametri del CHR
        myId = AstaPageManager.Instance.currentId;
        listCharactersMarket = AstaPageManager.Instance.listCharacters;
        name.text = listCharactersMarket[myId].name; // visualizzo la data odierna come controlo
         body.sprite = AstaPageManager.Instance.iltemBody[ listCharactersMarket[myId].body];
         head.sprite = AstaPageManager.Instance.iltemHead[ listCharactersMarket[myId].head];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnClickButton()
    {
        AstaPageManager.Instance.AstaLoginMarket();
    }
}
