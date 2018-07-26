using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageScaler : MonoBehaviour
{
    public static float pageWidth;
    public static float pageHeight;

    void Start()
    {
        RectTransform canvasRt = GameObject.Find("Canvas").GetComponent<RectTransform>();
        pageWidth = canvasRt.sizeDelta.x;
        pageHeight = canvasRt.sizeDelta.y;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform childRt = Transformf.GetRt(transform.GetChild(i));
            childRt.offsetMin = new Vector2(pageWidth * i, 0);
            childRt.offsetMax = new Vector2(pageWidth * i, 0);
        }
    }
}