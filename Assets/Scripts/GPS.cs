using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{
    public Text text;

    void Start()
    {
        Input.location.Start();
        Refresh();  
    }

    public void Refresh()
    {
        StartCoroutine(_Refresh());
    }


    IEnumerator _Refresh()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("non abilitato GPS");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1f);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Time Out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("unable to determin device location");
            yield break;
        }
        text.text = " latitude " + Input.location.lastData.latitude + " longitude " + Input.location.lastData.longitude;
        string url = "https://www.google.it/maps?q=" + Input.location.lastData.latitude + "," + Input.location.lastData.longitude;
        Application.OpenURL(url);
        Debug.Log("open " + text.text);
    }



}
