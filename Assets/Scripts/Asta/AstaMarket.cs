using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaMarket : MonoBehaviour
{
    // public bool checkGenerate = true;
    public GameObject scrollContentPAret;
    // Use this for initialization
    void Start()
    {
            GenerateMarketCharacters();
    }
	
    // Update is called once per frame
    void Update()
    {
        // if (AstaPageManager.Instance.numCharactersDB != 0 && checkGenerate)
        // {
        //     Debug.Log("Entrato");
        //     checkGenerate = false;
        //     GenerateMarketCharacters();
        // }

        // if (Input.GetKey(KeyCode.Escape))
        // { 
        //     OpenMainPage();
        // }
    }

    // public void OpenMainPage()
    // {
    //     AstaPageManager.Instance.AstaMainPage();
    // }

    public void OpenCharacterPage(){
         string nameObj = EventSystem.current.currentSelectedGameObject.name;
         string[] splitName = nameObj.Split('.');
         AstaPageManager.Instance.currentId = int.Parse(splitName[1]);
        AstaPageManager.Instance.AstaLoginCharacterMarket();
    }

    public void GenerateMarketCharacters()
    {
        List<AstaPageManager.Character> listCharactersMarket = AstaPageManager.Instance.listCharacters;
        // Debug.Log("COUNTTT: " + listCharactersMarket.Count);
        for (int i = 0; i < listCharactersMarket.Count; i++)
        {
            GameObject characterPrefab = Resources.Load("Character") as GameObject;
            GameObject character = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            character.transform.SetParent(scrollContentPAret.transform, false);
            character.name = "CHR." + i.ToString(); 
            character.GetComponent<AstaCharaacterMarketList>().name.text =  listCharactersMarket[i].name;
            character.GetComponent<AstaCharaacterMarketList>().id =  i;
            character.GetComponent<AstaCharaacterMarketList>().dataStopMarket = listCharactersMarket[i].dataStopMarket;
        }
    }

    public void ReturnClickButton(){
        AstaPageManager.Instance.AstaMainPage();
    }
}
