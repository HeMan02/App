using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class NewsScriptable : ScriptableObject
{
    public string title;
    public Sprite photo;
    public Category category;
    public string hyperlink;

    public enum Category
    {
        Scienza,
Cultura,
Politica,
Sport,
Gossip,
Attualità}

    ;
}