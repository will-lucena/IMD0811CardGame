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
        offsetX = transform.localPosition.x - eventData.pointerCurrentRaycast.screenPosition.x;
        offsetY = transform.localPosition.y - eventData.pointerCurrentRaycast.screenPosition.y;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.screenPosition == Vector2.zero)
        {
            OnEndDrag(eventData);
        }

        Vector3 newPos = new Vector3(eventData.pointerCurrentRaycast.screenPosition.x + offsetX, eventData.pointerCurrentRaycast.screenPosition.y + offsetY, 0);
        transform.localPosition = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (oldParent != parentToReturnTo)
        {
            handToBattle(eventData.pointerDrag);
        }
        transform.localPosition = new Vector3(0, 90, 0);
    }

    public void selfRemove()
    {
        Destroy(this);
    }
}
