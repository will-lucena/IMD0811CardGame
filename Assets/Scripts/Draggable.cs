using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static event System.Action<GameObject> handToBattle;

    private float offsetX;
    private float offsetY;
    public Transform parentToReturnTo;
    private Transform oldParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentToReturnTo = transform.parent;
        oldParent = transform.parent;
        transform.SetParent(parentToReturnTo.parent);
        offsetX = transform.position.x - eventData.position.x;
        offsetY = transform.position.y - eventData.position.y;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPos = new Vector2(eventData.position.x + offsetX, eventData.position.y + offsetY);
        transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (oldParent != parentToReturnTo)
        {
            handToBattle(eventData.pointerDrag);
        }
    }

    public void selfRemove()
    {
        Destroy(this);
    }
}
