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
    public Text bonus;
    public Text malus;
    public Text randomSkill;
    public Text type;
    public Text life;
    public Text xp;
    // Start is called before the first frame update
    void Start()
    {
        // Setto tutti i parametri del CHR
        myId = AstaPageManager.Instance.currentId;
        listCharactersMarket = AstaPageManager.Instance.listCharacters;
        name.text = listCharactersMarket[myId].name; // visualizzo la data odierna come controlo
        body.sprite = AstaPageManager.Instance.iltemBody[ listCharactersMarket[myId].body];
        head.sprite = AstaPageManager.Instance.iltemHead[ listCharactersMarket[myId].head];
        bonus.text = "" + listCharactersMarket[myId].bonus;
        malus.text = "" + listCharactersMarket[myId].malus;
        randomSkill.text = "" + listCharactersMarket[myId].randomSkill;
        type.text = "" + listCharactersMarket[myId].type;
        life.text = "" + listCharactersMarket[myId].life;
        xp.text = "" + listCharactersMarket[myId].xp;
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
