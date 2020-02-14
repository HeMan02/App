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
    // Start is called before the first frame update
    void Start()
    {
        // Richiesta se è libero o meno
        int idCharacterInSlot = CheckCharacterInSlot();
        if (idCharacterInSlot != 0)
        {
            // capisco quale dei miei characters è all'interno e lo inserisco
            GameObject[] listCharacters = GameObject.FindGameObjectsWithTag("Character");
            // listCharacters[0].gameObject.transform.SetParent(null);
            listCharacters[0].transform.SetParent(slotCharacter);
            listCharacters[0].transform.localPosition = new Vector3(0, 0, 0);
            RectTransform m_RectTransform = listCharacters[0].GetComponent<RectTransform>();
            m_RectTransform.anchoredPosition = new Vector2(m_RectTransform.anchoredPosition.x, m_RectTransform.anchoredPosition.y * 2);
            Debug.Log("Count " + listCharacters[0].name);
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
        // GameObject[] listCharacters = GameObject.FindGameObjectsWithTag("Character");
        // // listCharacters[0].transform.SetParent(null);
        // listCharacters[0].transform.SetParent(slotCharacter);
    }

    public int CheckCharacterInSlot()
    {
        return 1;
    }
}