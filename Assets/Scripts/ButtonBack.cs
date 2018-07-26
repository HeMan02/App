using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBack : MonoBehaviour
{
    public void Back()
    {
//        if (PageSwapper.Page > 0)
//            PageSwapper.SetPage(PageSwapper.Page - 1);
//        else
        BackToMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackToMenu();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
