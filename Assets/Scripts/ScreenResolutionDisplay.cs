using UnityEngine;

public class ScreenResolutionDisplay : MonoBehaviour
{
    void Awake()
    {
        Debug.Log(Screen.currentResolution.width + " x " + Screen.currentResolution.height);
    }
}
