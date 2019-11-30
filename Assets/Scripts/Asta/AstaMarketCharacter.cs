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
    // Start is called before the first frame update
    void Start()
    {
        myId = AstaPageManager.Instance.currentId;
        listCharactersMarket = AstaPageManager.Instance.listCharacters;
        name.text = listCharactersMarket[myId].name; // visualizzo la data odierna come controlo
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
