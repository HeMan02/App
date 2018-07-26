using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsSetter : MonoBehaviour {

    [SerializeField]
    Image photo;
    [SerializeField]
    Text title;
    [SerializeField]
    Text tag;
    string hyperlink;

    public void SetNews(Sprite photo,string title,string tag,string hyperlink)
    {
        this.photo.sprite = photo;
        this.title.text = title;
        this.tag.text = tag;
        this.hyperlink = hyperlink;
    }

    public void OpenHyperlink()
    {
        Application.OpenURL(hyperlink);
    }
}
