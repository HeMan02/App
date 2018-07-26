using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DatingHandler : MonoBehaviour
{
    [SerializeField]
    GameObject prefabPage;

    // Use this for initialization
    void Awake()
    {
        if (Data.datingProfile == null)
        {
            DatingScriptable[] datingScriptable = Resources.LoadAll<DatingScriptable>("DatingScriptable");
            Data.datingProfile = new Profile[datingScriptable.Length];
            for (int i = 0; i < datingScriptable.Length; i++)
            {
                GameObject page = Instantiate(prefabPage, transform);
                page.GetComponent<DatingGuest>().CreateGuest(this, datingScriptable[i].profilePic, datingScriptable[i].name + ", " + datingScriptable[i].age, datingScriptable[i].description);
                Profile guest = new Profile(datingScriptable[i], false);
                Data.datingProfile[i] = guest;
            }
        }
        else
        {
            for (int i = 0; i < Data.datingProfile.Length; i++)
            {
                GameObject page = Instantiate(prefabPage, transform);
                page.GetComponent<DatingGuest>().CreateGuest(this, Data.datingProfile[i].profilePic, Data.datingProfile[i].name + ", " + Data.datingProfile[i].age, Data.datingProfile[i].description);
            }
        }
    }

    void Start()
    {
        PageSwapper.SetPage(PlayerPrefs.GetInt("currentDatingPage"));
    }
}
