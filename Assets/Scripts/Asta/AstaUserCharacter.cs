using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaUserCharacter : MonoBehaviour
{
    public int myId;
    public List<AstaPageManager.Character> userCharacter;
    public Text name;
    public Image body;
    public Image head;
    public Text bonus;
    public Text malus;
    public Text randomSkill;
    public Text type;
    public Image life;
    public Text xp;
    // Start is called before the first frame update
    void Start()
    {
        // Setto tutti i parametri del CHR
        myId = AstaPageManager.Instance.currentUserId;
        userCharacter = AstaPageManager.Instance.listUserCharacters;
        name.text = userCharacter[myId].name; // visualizzo la data odierna come controlo
        bonus.text = "" + userCharacter[myId].bonus;
        malus.text = "" + userCharacter[myId].malus;
        randomSkill.text = "" + userCharacter[myId].randomSkill;
        type.text = "" + userCharacter[myId].type;
        head.sprite = AstaPageManager.Instance.iltemHead[ userCharacter[myId].head];
        body.sprite = AstaPageManager.Instance.iltemBody[ userCharacter[myId].body];
        life.fillAmount = (float)((float)userCharacter[myId].life/100);
        xp.text = "Xp: " + userCharacter[myId].xp + "/100";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnClickButton()
    {
        AstaPageManager.Instance.AstaLoginUserListCharacters();
    }
}
