using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstaDungeon : MonoBehaviour
{
    public GameObject scrollContentPAretDungeon;
    // Start is called before the first frame update
    void Start()
    {
        GenerateDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnClickButton(){
        AstaPageManager.Instance.AstaLoginUserListCharacters();
    }

     public void GenerateDungeon()
    {
        List<AstaPageManager.Dungeon> listDungeon = AstaPageManager.Instance.listDungeon;
        Debug.Log("Count" + listDungeon.Count);
        for (int i = 0; i < listDungeon.Count; i++)
        {
             Debug.Log("Entrato");
             GameObject dungeonPrefab = Resources.Load("Missions") as GameObject;
             GameObject dungeon = Instantiate(dungeonPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
             dungeon.transform.SetParent(scrollContentPAretDungeon.transform, false);
             dungeon.name = "DUNGEON." + i.ToString(); 
             dungeon.GetComponent<AstaDungeonObj>().myId =  i;
            //  dungeon.GetComponent<AstaDungeonObj>().time.text =  listDungeon[i].time;
            //  dungeon.GetComponent<AstaDungeonObj>().description = listDungeon[i].description;
            //  dungeon.GetComponent<AstaDungeonObj>().type = listDungeon[i].type;
            //  dungeon.GetComponent<AstaDungeonObj>().coinsText = listDungeon[i].cashWin;
        }
    }
}
