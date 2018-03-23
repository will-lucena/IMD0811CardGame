﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	private float offsetX;
	private float offsetY;

	public Transform parentToReturnTo = null;

	public void OnBeginDrag(PointerEventData eventData)
	{
		parentToReturnTo = transform.parent;
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
	}
}
