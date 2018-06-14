using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public static event System.Action<CardData> addCard;

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.CompareTag(tag))
        {
            d.parentToReturnTo = transform;
        }

        if (addCard != null)
        {
            addCard(eventData.pointerDrag.GetComponent<Content>().data);
        }
    }
}
