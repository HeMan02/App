using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessageHandler : MonoBehaviour, ISelectHandler
{
    public GameObject msgUser;
    public GameObject msgOther;

    InputField inputField;
    TouchScreenKeyboard keyboard;
    Transform messageList;
    Profile profileCurrent;

    float botTimer = 0f;

    void Start()
    {
        inputField = GetComponent<InputField>();
        messageList = transform.parent.parent.Find("MessageList");
    }

    void Update()
    {
        if (botTimer < 0)
        {
            if (transform.parent.parent.Find("MessageList").childCount > 0 && transform.parent.parent.Find("MessageList").GetChild(0).name.Contains("User"))
            {
                string message = Random.Range(0f, 1f) > .7f ? "Mortacci tua!" : transform.parent.parent.Find("MessageList").GetChild(0).GetComponentInChildren<Text>().text;
                InstantiateMessage(message, false);
                profileCurrent.chat.Add(new ChatHistory(message, false));
                botTimer = Random.Range(3f, 10f);
            }
            else
            {
                botTimer = 5f;
            }
        }
        else
            botTimer -= Time.deltaTime;
    }

    public void SendMsg()
    {
        string message = inputField.text;
        if (string.IsNullOrEmpty(message))
            return;
        InstantiateMessage(message, true);
        profileCurrent.chat.Add(new ChatHistory(message, true));
        inputField.text = "";
    }

    public void SetMessages(Profile profile)
    {
        profileCurrent = profile;

        for (int i = messageList.childCount - 1; i >= 0; i--)
            Destroy(messageList.GetChild(i).gameObject);

        foreach (ChatHistory chatItem in profileCurrent.chat)
            InstantiateMessage(chatItem.message, chatItem.owner);
    }

    void InstantiateMessage(string message, bool mine)
    {
        GameObject msgObj = GameObject.Instantiate(mine ? msgUser : msgOther);
        msgObj.transform.SetParent(messageList);
        msgObj.transform.position = msgObj.transform.parent.position;
        msgObj.transform.localScale = Vector3.one;
        msgObj.transform.SetAsFirstSibling();
        msgObj.GetComponentInChildren<Text>().text = message;
    }

    #region ISelectHandler implementation

    public void OnSelect(BaseEventData eventData)
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        keyboard.active = true;
    }

    #endregion
}
