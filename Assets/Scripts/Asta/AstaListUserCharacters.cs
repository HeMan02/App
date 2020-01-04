﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaListUserCharacters : MonoBehaviour
{
     public GameObject scrollContentPAret;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMyCharacters();
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
        }
    }

    public void ReturnClickButton(){
        AstaPageManager.Instance.AstaMainPage();
    }
}
