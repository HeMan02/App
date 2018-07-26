using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class DatingScriptable : ScriptableObject
{
    public string name;
    public Sprite profilePic;
    public Sprite contactPic;
    public long age;
    public string description;
}
