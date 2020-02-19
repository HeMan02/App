using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AstaDungeonCharacterSelectedObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject target;
    public int myId;
    // Aggiunto per posizione nella lista, ma scorrendo la lista e confrontando l'id preso da DB dovrebbe essere tutto ok
    // public int myIdPositionOfList;
    public Text name;
    public List<AstaPageManager.Character> listDungeonCharacters;
    Vector3 startPosition;
    public GameObject gridParent;
    public GameObject scrollParent;
    bool check;
    public bool chekDragAndDrop = false;
    public Transform finalTargetTransform;
    public Transform actualFinalTransformReturnPosition;
    public RectTransform targetRectTRansform;
    public BoxCollider2D returnBoxCollider;
    public bool disactiveBoxCollider = false;
    public bool isOccuped = false;
    // Start is called before the first frame update

    void Awake()
    {
        scrollParent = GameObject.Find("ScrollContentCharacter");
        gridParent = GameObject.Find("Grid");
        returnBoxCollider = GameObject.Find("CharacterContainerToDungeon").GetComponent<BoxCollider2D>();
        actualFinalTransformReturnPosition = scrollParent.transform;
        returnBoxCollider.enabled = false;
    }

    void Start()
    {
        listDungeonCharacters = AstaPageManager.Instance.listUserCharacters;
        for (int i = 0; i < listDungeonCharacters.Count; i++)
        {
            if (listDungeonCharacters[i].id == myId)
            {
                name.text = "" + listDungeonCharacters[i].name;
            }
        }
        Debug.Log("IdCharacter: " + myId);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        check = false;
        target = gameObject;
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isOccuped)
        {
            if (!check)
            {
                target.transform.SetParent(gridParent.transform);
                check = true;
            }
            // ==== INPUT DA TOUCH
            transform.position = Input.mousePosition;
            if (disactiveBoxCollider)
            {
                returnBoxCollider.enabled = false;
            }
            else
            {
                returnBoxCollider.enabled = true;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isOccuped)
        {
            targetRectTRansform.anchorMax = new Vector2(0, 0);
            targetRectTRansform.anchorMin = new Vector2(0, 0);
            if (!chekDragAndDrop) // Ritorno alla partenza
            {
                // Pezzo ocmmentato ma non detto che sia corretto
                target.transform.SetParent(actualFinalTransformReturnPosition);
                // if (!disactiveBoxCollider)
                // {
                target.transform.localPosition = new Vector3(0, 0, 0);
                targetRectTRansform.anchoredPosition = new Vector2(targetRectTRansform.anchoredPosition.x, targetRectTRansform.sizeDelta.y);
                // }
                target = null;
            }
            else // trovato posto dove inserire
            {
                Debug.Log("1");
                actualFinalTransformReturnPosition = finalTargetTransform;
                target.transform.SetParent(actualFinalTransformReturnPosition);
                if (!disactiveBoxCollider)
                {
                    target.transform.localPosition = new Vector3(0, 0, 0);
                    targetRectTRansform.anchoredPosition = new Vector2(targetRectTRansform.anchoredPosition.x, targetRectTRansform.sizeDelta.y);
                }
                target = null;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (string.Compare(col.gameObject.name, "CharacterContainerToDungeon") == 0)
        {
            disactiveBoxCollider = true;
            finalTargetTransform = scrollParent.transform;
        }
        else
        {
            if (string.Compare(col.gameObject.name, "CharacterEnterToDungeon") == 0)
            {
                if (col.gameObject.transform.childCount == 0)
                {
                    disactiveBoxCollider = false;
                    finalTargetTransform = col.gameObject.transform;
                }
            }
            else
            {
                if (!col.gameObject.name.Contains("CHR"))
                {
                    disactiveBoxCollider = false;
                    finalTargetTransform = col.gameObject.transform;
                }
            }
        }
        chekDragAndDrop = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (string.Compare(col.gameObject.name, "CharacterContainerToDungeon") == 1)
        {
            chekDragAndDrop = false;
        }
    }
}