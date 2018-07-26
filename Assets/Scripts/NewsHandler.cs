using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsHandler : MonoBehaviour
{
    [SerializeField]
    GameObject prefabNews;
    [SerializeField]
    Transform newsSection;
    NewsScriptable[] newsScriptables;

    float sizeY = 200;
    float startY = -110;
    float spacingY = 10;

    // Use this for initialization
    void Awake()
    {
        newsScriptables = Resources.LoadAll<NewsScriptable>("NewsScriptable");
        newsSection = transform.Find("Page0/NewsSection");
    }

    void Start()
    {
        Shuffle();

        float contentSize = startY;

        for (int i = 0; i < newsScriptables.Length; i++)
        {
            GameObject go = Instantiate(prefabNews);
            go.transform.parent = newsSection;
            go.GetComponent<NewsSetter>().SetNews(newsScriptables[i].photo, newsScriptables[i].title, newsScriptables[i].category.ToString(), newsScriptables[i].hyperlink);
            go.transform.localScale = Vector3.one;
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, contentSize);
            rt.sizeDelta = new Vector2(0, sizeY);
            contentSize = startY + ((spacingY + sizeY) * -(i + 1));
        }

        newsSection.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -contentSize);
    }

    public void Shuffle()
    {
        NewsScriptable tempGO;
        for (int i = 0; i < newsScriptables.Length; i++)
        {
            int rnd = Random.Range(0, newsScriptables.Length);
            tempGO = newsScriptables[rnd];
            newsScriptables[rnd] = newsScriptables[i];
            newsScriptables[i] = tempGO;
        }
    }
}
