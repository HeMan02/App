using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaDungeonCharacterSelectedObj : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject target;
    public int myId;
    public Text name;
    public List<AstaPageManager.Character> listDungeonCharacters;
    Vector3 startPosition;
    public GameObject gridParent;
    public GameObject scrollParent;
    bool check;
    // Start is called before the first frame update
    void Start()
    {
        // listDungeonCharacters = AstaPageManager.Instance.listUserCharacters;
        // name.text = "" + listDungeonCharacters[myId].name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void OnBeginDrag (PointerEventData eventData)
    {
        Debug.Log("0");
        check = false;
        target = gameObject;
        startPosition = transform.position;
    }

    
    public void OnDrag (PointerEventData eventData)
    {
        
        Debug.Log("1");
        if(!check){
        target.transform.SetParent(gridParent.transform, false);
        check = true;
        }
        transform.position = Input.mousePosition;
    }

     public void OnEndDrag (PointerEventData eventData)
    {
        Debug.Log("2");
        target.transform.SetParent(scrollParent.transform, false);
        target = null;
        // transform.position = startPosition;
        
    }
}
