using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatingGuest : MonoBehaviour
{
    public Image photo;
    public Text nameAndAge;
    public Text description;
    public DatingHandler handler;


    public void CreateGuest(DatingHandler handler, Sprite photo, string nameAndAge, string description)
    {
        this.handler = handler;
        this.photo.sprite = photo;
        this.nameAndAge.text = nameAndAge;
        this.description.text = description;
    }

    public void Vote(bool value)
    {
        PageSwapper.SetPage(PageSwapper.Page + 1,true);
        PlayerPrefs.SetInt("currentDatingPage", PageSwapper.Page);
        if (value && Random.Range(0f, 1f) > .5f)
        {
            Profile p = Data.SearchProfile(photo.sprite);
            if (p != null && !Data.chats.Contains(p))
            {
                Data.chats.Add(p);
            }
        }
    }
}
