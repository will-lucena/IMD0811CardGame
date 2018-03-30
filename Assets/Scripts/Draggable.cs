using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float offsetX;
    private float offsetY;
    GameObject placeholder;
    public Transform placeholderParent;
    public Transform parentToReturnTo;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentToReturnTo = transform.parent;
        //setupPlaceholder();
        transform.SetParent(parentToReturnTo.parent);
        offsetX = transform.position.x - eventData.position.x;
        offsetY = transform.position.y - eventData.position.y;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPos = new Vector2(eventData.position.x + offsetX, eventData.position.y + offsetY);
        transform.position = newPos;

        /*
        int tempSiblingIndex = placeholderParent.childCount;

        if (placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                tempSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < tempSiblingIndex)
                {
                    tempSiblingIndex--;
                }
                break;
            }
        }
        
        placeholder.transform.SetSiblingIndex(tempSiblingIndex);
        /**/
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        //transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        Destroy(placeholder);
    }

    private void setupPlaceholder()
    {
        placeholder = new GameObject();
        placeholder.transform.SetParent(transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        placeholderParent = parentToReturnTo;
    }
}
