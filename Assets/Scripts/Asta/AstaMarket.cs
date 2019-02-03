using System.Collections;
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
        if (AstaPageManager.instance.numCharactersDB != 0 && checkGenerate)
        {
            checkGenerate = false;
            for (int i = 0; i < 5; i++)
            {
                GameObject characterPrefab = Resources.Load("Character") as GameObject;
                GameObject character = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                character.transform.SetParent(scrollContentPAret.transform, false);
            }

        }

        if (Input.GetKey(KeyCode.Escape))
        { 
            OpenMainPage();
        }
    }

    public void OpenMainPage()
    {
        AstaPageManager.instance.AstaMainPage();
    }
}
