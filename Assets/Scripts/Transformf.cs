using UnityEngine;

public static class Transformf
{
    public static RectTransform GetRt(Transform transform)
    {
        RectTransform rt = transform.GetComponent<RectTransform>();
        if (!rt)
            Debug.LogError("RectTransform not found on '" + rt.name + "'.");
        return transform.GetComponent<RectTransform>();
    }
}
