using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class PhotoScriptable : ScriptableObject
{
    public string name;
    public Sprite profilePic;
    public Sprite[] pic;
    public long like;
    [TextArea]
    public string comment;
}
