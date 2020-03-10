using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaListUserCharacters : MonoBehaviour
{
     public GameObject scrollContentPAret;
     public Text userCharactersNumber;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMyCharacters();
        userCharactersNumber.text = "" + AstaPageManager.Instance.listUserCharacters.Count + " Piggies ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUserCharacterPage(){
         string nameObj = EventSystem.current.currentSelectedGameObject.name;
         string[] splitName = nameObj.Split('.');
         AstaPageManager.Instance.currentUserId = int.Parse(splitName[1]);
        AstaPageManager.Instance.AstaLoginUserCharacter();
    }

    public void GenerateMyCharacters()
    {
        List<AstaPageManager.Character> listMyCharacters = AstaPageManager.Instance.listUserCharacters;
        for (int i = 0; i < listMyCharacters.Count; i++)
        {
            GameObject characterPrefab = Resources.Load("UserCharacter") as GameObject;
            GameObject character = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            character.transform.SetParent(scrollContentPAret.transform, false);
            character.name = "CHR." + i.ToString(); 
            character.GetComponent<AstaUserList>().name.text =  listMyCharacters[i].name;
            character.GetComponent<AstaUserList>().id =  i;
            // character.GetComponent<AstaUserList>().lifeValue = listMyCharacters[i].life;
            character.GetComponent<AstaUserList>().lifeValue = listMyCharacters[i].life;
            //Debug.Log("id: " + listMyCharacters[i].id + " idDungeon: " + listMyCharacters[i].idDungeon);
            Debug.Log("Controllo con value: " + listMyCharacters[i].idDungeon + " idCharacter: " + listMyCharacters[i].id);
            if (listMyCharacters[i].idDungeon != 0)
            {
                character.GetComponent<AstaUserList>().occupedValue = true;
            }
            else
            {
                character.GetComponent<AstaUserList>().occupedValue = false;
            }
            //bool boolValue = (Random.Range(0, 2) == 0);
            //character.GetComponent<AstaUserList>().occupedValue = boolValue;
            // DA AGGIUNGERE SU DB "life" e "occuped", questi dati li passero qua,presi da DB
            character.GetComponent<AstaUserList>().xpValue = listMyCharacters[i].xp;

        }
    }

    public void ReturnClickButton(){
        AstaPageManager.Instance.AstaMainPage();
    }

    public void EnterToDungeonListButton(){
        AstaPageManager.Instance.AstaDungeon();
    }
}
