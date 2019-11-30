﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstaMarket : MonoBehaviour
{
    public bool checkGenerate = true;
    public GameObject scrollContentPAret;
    // Use this for initialization
    void Start()
    {

    }
	
    // Update is called once per frame
    void Update()
    {
        if (AstaPageManager.Instance.numCharactersDB != 0 && checkGenerate)
        {
            checkGenerate = false;
            GenerateMarketCharacters();
        }

        if (Input.GetKey(KeyCode.Escape))
        { 
            OpenMainPage();
        }
    }

    public void OpenMainPage()
    {
        AstaPageManager.Instance.AstaMainPage();
    }

    public void OpenCharacterPage(){
      AstaPageManager.Instance.AstaLoginCharacterMarket();
    }

    public void GenerateMarketCharacters()
    {
        List<AstaPageManager.Character> listCharactersMarket = AstaPageManager.Instance.GenerateListOfCharacters();
        for (int i = 0; i < listCharactersMarket.Count; i++)
        {
            GameObject characterPrefab = Resources.Load("Character") as GameObject;
            GameObject character = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            character.transform.SetParent(scrollContentPAret.transform, false);
            character.name = "CHR" + i.ToString(); 
        }
    }
}
