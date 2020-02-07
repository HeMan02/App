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
    bool chekDragAndDrop = false;
    public Transform finalTargetTransform;
    public RectTransform targetRectTRansform;
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
        if(!check){
        target.transform.SetParent(gridParent.transform, false);
        check = true;
        }
        // ==== INPUT DA TOUCH
        transform.position = Input.mousePosition;        
    }

     public void OnEndDrag (PointerEventData eventData)
    {
        if(!chekDragAndDrop){
        target.transform.SetParent(scrollParent.transform, false);
        target = null;
         }else{
            target.transform.SetParent(finalTargetTransform, false);
            target.transform.localPosition = new Vector3(0,0,0);
             target.transform.position = new Vector3(target.transform.position.x,0,target.transform.position.z);
             targetRectTRansform.anchoredPosition = new Vector2(targetRectTRansform.anchoredPosition.x,0);
        target = null;
         }
        // transform.position = startPosition;
        
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        chekDragAndDrop = true;
        finalTargetTransform = col.gameObject.transform;
        Debug.Log(col.gameObject.name);
    }

     void OnTriggerExit2D(Collider2D col)
    {
        chekDragAndDrop = false;
        Debug.Log("USCITO");
    }
}
