using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstaDungeonCharacterSelectedObj : MonoBehaviour
{
    public int myId;
    public Text name;
    public List<AstaPageManager.Character> listDungeonCharacters;
    // Start is called before the first frame update
    void Start()
    {
        listDungeonCharacters = AstaPageManager.Instance.listUserCharacters;
        name.text = "" + listDungeonCharacters[myId].name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
