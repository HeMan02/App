using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class ChatManager
{
    public static void OpenChat(Profile profile)
    {
        if (SceneManager.GetActiveScene().name != "Chat")
            SceneManager.LoadScene("Chat");

        Text name = GameObject.Find("Canvas/Grid/Page1/TopBar/Name").GetComponent<Text>();
        Image image = GameObject.Find("Canvas/Grid/Page1/TopBar/PhotoMask/Photo").GetComponent<Image>();

        name.text = profile.name;
        image.sprite = profile.profilePic;

        MessageHandler messageHandler = GameObject.Find("Canvas/Grid/Page1/InputBar/InputField").GetComponent<MessageHandler>();
        Debug.Log("message");
        messageHandler.SetMessages(profile);

        PageSwapper.SetPage(1);
    }
}
