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
    public bool setTransform;
    // Start is called before the first frame update
    void Start()
    {
        // Richiesta se è libero o meno
        listDungeon = AstaPageManager.Instance.listDungeon;
        for (int i = 0; i < listDungeon.Count; i++)
        {
            if (listDungeon[i].id == myId)
            {
                name.text = "" + listDungeon[i].name;
            }
        }
        Debug.Log("IdDungeon: " + myId);
        StartCoroutine("GetCharacterOnDungeon");
        // 
        //  name.text =  "" + listDungeon[myId].name;
        //  time.text =  "" + listDungeon[myId].time;
        //  description.text =  "" + listDungeon[myId].description;
        //  type.text =   "" +listDungeon[myId].type;
        //  coinsText.text =  "" + listDungeon[myId].cashWin;
    }

    IEnumerator GetCharacterOnDungeon()
    {
        WWWForm form = new WWWForm();
        form.AddField("idDungeon", myId);
        WWW itemsData = new WWW("http://astaapp.altervista.org/GetCharacterOnDungeon.php", form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        string[] itemsCharacterOnDungeonArray = itemsDataString.Split(';');
        string[] itemSplit = itemsCharacterOnDungeonArray[0].ToString().Split('@');
        int characterId = int.Parse(itemSplit[1]); // num characters DB
        SetCharacterOnSlot(characterId);
    }

    public void SetCharacterOnSlot(int characterIdDB)
    {
        GameObject[] listCharacters = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < listCharacters.Length; i++)
        {
            string[] nameSplit = listCharacters[i].name.Split('.');
            int idCharacterObj = int.Parse(nameSplit[1]);
            if (idCharacterObj == characterIdDB)
            {

                listCharacters[i].gameObject.transform.SetParent(null);
                listCharacters[i].transform.SetParent(slotCharacter);
                listCharacters[i].transform.localPosition = new Vector3(0, 0, 0);
                RectTransform m_RectTransform = listCharacters[i].GetComponent<RectTransform>();
                m_RectTransform.anchoredPosition = new Vector2(m_RectTransform.anchoredPosition.x, m_RectTransform.sizeDelta.y);
                m_RectTransform.anchorMax = new Vector2(0, 0);
                m_RectTransform.anchorMin = new Vector2(0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update() { }

    public int CheckCharacterInSlot()
    {
        return 1;
    }
}