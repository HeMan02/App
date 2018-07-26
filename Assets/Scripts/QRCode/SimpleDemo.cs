using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wizcorp.Utils.Logger;

public class SimpleDemo : MonoBehaviour
{

    private IScanner barcodeScanner;
    public RawImage Image;
    public AudioSource audio;
    public Image frameImage;
    public GameObject backButton;

    // Disable Screen Rotation on that screen
    void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        backButton = GameObject.Find("BackButton");
    }

    void Start()
    {
        #if !UNITY_EDITOR
        backButton.SetActive(false);
        #endif
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            throw new Exception("This Webcam library can't work without the webcam authorization");
        }
        // Create a basic scanner
        barcodeScanner = new Scanner();
        barcodeScanner.Camera.Play();

        // Display the camera texture through a RawImage
        barcodeScanner.OnReady += (sender, arg) =>
        {
            // Set Orientation & Texture
            Image.transform.localEulerAngles = barcodeScanner.Camera.GetEulerAngles();
            Image.transform.localScale = barcodeScanner.Camera.GetScale();
            Image.texture = barcodeScanner.Camera.Texture;

            // Keep Image Aspect Ratio
            var rect = Image.GetComponent<RectTransform>();
//            rect.sizeDelta = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        };
        StartCoroutine("StartScanEnum");
    }

    IEnumerator StartScanEnum()
    {
        yield return new WaitForSeconds(2f);
        StartScan();
    }

    /// <summary>
    /// The Update method from unity need to be propagated to the scanner
    /// </summary>
    void Update()
    {
        if (barcodeScanner == null)
        {
            return;
        }
        barcodeScanner.Update();
    }

    #region UI Buttons

    public void StartScan()
    {
        if (barcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Start");
            return;
        }

        // Start Scanning
        barcodeScanner.Scan((barCodeType, barCodeValue) =>
            {
                barcodeScanner.Stop();
                Debug.Log("Found: " + barCodeType + " / " + barCodeValue);
                frameImage.color = Color.green;
                Application.OpenURL(barCodeValue);
                // Feedback
                audio.Play();
                #if UNITY_ANDROID || UNITY_IOS
                Handheld.Vibrate();
                #endif
            });
    }

    public void ClickStop()
    {
        if (barcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Stop");
            return;
        }

        // Stop Scanning
        barcodeScanner.Stop();
    }

    public void ClickBack()
    {
        // Try to stop the camera before loading another scene
        StartCoroutine(StopCamera(() =>
                {
                    SceneManager.LoadScene("MainMenu");
                }));
    }

    /// <summary>
    /// This coroutine is used because of a bug with unity (http://forum.unity3d.com/threads/closing-scene-with-active-webcamtexture-crashes-on-android-solved.363566/)
    /// Trying to stop the camera in OnDestroy provoke random crash on Android
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator StopCamera(Action callback)
    {
        // Stop Scanning
        Image = null;
        barcodeScanner.Destroy();
        barcodeScanner = null;

        // Wait a bit
        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }

    #endregion
}
