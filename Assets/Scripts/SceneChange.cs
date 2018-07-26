using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
        else
            Debug.LogError("ERROR: A scene to load is not set!");
    }

    void Start()
    {
        if (string.IsNullOrEmpty(sceneName))
            GetComponent<Button>().interactable = false;
    }
}
