using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	private float offsetX;
	private float offsetY;
	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("begun");
		offsetX = transform.position.x - eventData.position.x;
		offsetY = transform.position.y - eventData.position.y;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 newPos = new Vector2(eventData.position.x + offsetX, eventData.position.y + offsetY);
		this.transform.position = newPos;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("ended");
	}
}
