using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatList : MonoBehaviour
{
    public GameObject contactBar;

    Transform listParent;

    void Awake()
    {
        listParent = transform.Find("Scroll View/Viewport/Content");
        if (Data.chats.Count == 0)
        {
            Data.chats.Add(new Profile("Dora", "Photos/Dora"));
            Data.chats.Add(new Profile("Tonia", "Photos/Tonia"));
        }
        UpdateList();
    }

    public void UpdateList()
    {
        for (int i = listParent.childCount - 1; i >= 0; i--)
            Destroy(listParent.GetChild(i).gameObject);

        for (int i = 0; i < Data.chats.Count; i++)
        {
            GameObject c = Instantiate(contactBar);
            c.transform.SetParent(listParent);
            c.GetComponentInChildren<ButtonContact>().Profile = Data.chats[i];
            c.transform.localScale = Vector3.one;
        }
    }
}
