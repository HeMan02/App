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
    // Start is called before the first frame update
    void Start()
    {
        // Richiesta se è libero o meno
        int idCharacterInSlot = CheckCharacterInSlot();
        if (idCharacterInSlot != 0)
        {
            // capisco quale dei miei characters è all'interno e lo inserisco
            GameObject[] listCharacters = GameObject.FindGameObjectsWithTag("Character");
            // List<GameObject> list = new List<GameObject>();
            // GameObject temp = list.Find("CHR");
            Debug.Log("Count " + listCharacters.Length);
        }
        else
        {
            // Caso di slot vuoto
        }
        // listDungeon = AstaPageManager.Instance.listDungeon;
        //  name.text =  "" + listDungeon[myId].name;
        //  time.text =  "" + listDungeon[myId].time;
        //  description.text =  "" + listDungeon[myId].description;
        //  type.text =   "" +listDungeon[myId].type;
        //  coinsText.text =  "" + listDungeon[myId].cashWin;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int CheckCharacterInSlot()
    {
        return 1;
    }
}