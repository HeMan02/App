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
}
