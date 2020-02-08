using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AstaDungeonCharacterSelectedObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject target;
    public int myId;
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
    // Start is called before the first frame update
    void Start()
    {
        // listDungeonCharacters = AstaPageManager.Instance.listUserCharacters;
        // name.text = "" + listDungeonCharacters[myId].name;
        actualFinalTransformReturnPosition = scrollParent.transform;
        returnBoxCollider.enabled = false;
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
        if (!check)
        {
            target.transform.SetParent(gridParent.transform, false);
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

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!chekDragAndDrop) // Ritorno alla partenza
        {
            target.transform.SetParent(actualFinalTransformReturnPosition, false);
            if (!disactiveBoxCollider)
            {
                target.transform.localPosition = new Vector3(0, 0, 0);
                targetRectTRansform.anchoredPosition = new Vector2(targetRectTRansform.anchoredPosition.x, 0);
            }
            target = null;
        }
        else // trovato posto dove inserire
        {
            actualFinalTransformReturnPosition = finalTargetTransform;
            target.transform.SetParent(actualFinalTransformReturnPosition, false);
            if (!disactiveBoxCollider)
            {
                target.transform.localPosition = new Vector3(0, 0, 0);
                targetRectTRansform.anchoredPosition = new Vector2(targetRectTRansform.anchoredPosition.x, 0);
            }
            target = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (string.Compare(col.gameObject.name, "CharacterContainerToDungeon") == 0)
        {
            Debug.Log("0");
            disactiveBoxCollider = true;
            finalTargetTransform = scrollParent.transform;
        }
        else
        {
            if (string.Compare(col.gameObject.name, "CharacterEnterToDungeon") == 0)
            {
                Debug.Log("1 " + col.gameObject.transform.childCount);
                if (col.gameObject.transform.childCount == 0)
                {
                    Debug.Log("3 ");
                    disactiveBoxCollider = false;
                    finalTargetTransform = col.gameObject.transform;
                }
            }
            else
            {
                if (!col.gameObject.name.Contains("CharacterDungeonSelect"))
                {
                    Debug.Log("4 ");
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
