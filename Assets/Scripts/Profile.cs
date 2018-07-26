using System.Collections.Generic;
using UnityEngine;
using System;

[SerializableAttribute]
public class Profile
{
    public string name;
    public Sprite profilePic;
    public long age;
    public string description;
    public bool match;

    public List<ChatHistory> chat = new List<ChatHistory>();

    public Profile(DatingScriptable info, bool match)
    {
        this.name = info.name;
        this.profilePic = info.profilePic;
        this.age = info.age;
        this.description = info.description;
        this.match = match;
    }

    public Profile(string name, Sprite photo, long age, string description, bool match)
    {
        this.name = name;
        this.profilePic = photo;
        this.age = age;
        this.description = description;
        this.match = match;
    }

    public Profile(string name, string photoPath)
    {
        this.name = name;
        this.profilePic = Resources.Load<Sprite>(photoPath);
        this.age = 21;
        this.description = "Description";
        this.match = true;
    }
}

public class ChatHistory
{
    public string message;
    public bool owner;

    public ChatHistory(string message, bool owner)
    {
        this.message = message;
        this.owner = owner;
    }
}
