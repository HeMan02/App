using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoHandler : MonoBehaviour
{
    [SerializeField]
    GameObject prefabPhoto;
    [SerializeField]
    Transform photosSection;
    PhotoScriptable[] photosScriptables;

    float sizeY = 1100;
    float startY = -0;
    float spacingY = 10;

    // Use this for initialization
    void Awake()
    {
        photosScriptables = Resources.LoadAll<PhotoScriptable>("PhotoScriptable");
        photosSection = transform.Find("Page0/PostSection");
    }

    void Start()
    {
        Shuffle();

        float contentSize = startY;

        for (int i = 0; i < photosScriptables.Length; i++)
        {
            GameObject go = Instantiate(prefabPhoto);
            go.transform.parent = photosSection;
            go.GetComponent<PhotoSetter>().SetPhoto(photosScriptables[i].name, photosScriptables[i].profilePic, photosScriptables[i].pic[0], photosScriptables[i].like,photosScriptables[i].comment);
            go.transform.localScale = Vector3.one;
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, contentSize);
            rt.sizeDelta = new Vector2(0, sizeY);
            contentSize = startY + ((spacingY + sizeY) * -(i + 1));
        }

        photosSection.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -contentSize);
    }

    public void Shuffle()
    {
        PhotoScriptable tempGO;
        for (int i = 0; i < photosScriptables.Length; i++)
        {
            int rnd = Random.Range(0, photosScriptables.Length);
            tempGO = photosScriptables[rnd];
            photosScriptables[rnd] = photosScriptables[i];
            photosScriptables[i] = tempGO;
        }
    }
}

