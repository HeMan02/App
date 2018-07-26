using UnityEngine;
using UnityEngine.UI;

public class ButtonContact : MonoBehaviour
{
    Image image;
    Text name;
    Profile profile;

    public Profile Profile
    {
        get { return profile; }
        set
        {
            profile = value;
            image.sprite = profile.profilePic;
            name.text = profile.name;
        }
    }

    void Awake()
    {
        image = transform.Find("PhotoMask/Photo").GetComponent<Image>();
        name = GetComponentInChildren<Text>();
    }

    public void OpenChat()
    {
        if (profile != null)
            ChatManager.OpenChat(profile);
    }
}
