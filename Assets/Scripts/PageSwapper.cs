using UnityEngine;

public class PageSwapper : MonoBehaviour
{
    public static PageSwapper singleton;

    /// <summary>
    /// Gets the current page.
    /// </summary>
    /// <value>The current page.</value>
    public static int Page { get { return singleton.page; } }

    int page;

    void Awake()
    {
        singleton = this;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(-page * Screen.width + (Screen.width / 2f), transform.position.y, 0), 0.2f);
    }

    public static void SetPage(int page,bool loopPages=false)
    {
        if (loopPages && page == singleton.transform.childCount)
            singleton.page = 0;
        else
            singleton.page = Mathf.Clamp(page, 0, singleton.transform.childCount - 1);
    }
}
