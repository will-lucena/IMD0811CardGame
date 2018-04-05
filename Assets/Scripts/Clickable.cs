using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    public static event System.Action<GameObject> showTargets;
    public static event System.Action<GameObject> selected;

    public static GameObject cardSelected;

    private Card card;

    private void Start()
    {
        cardSelected = null;
        card = GetComponent<CardScript>().cardInfos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardSelected == null)
        {
            cardSelected = gameObject;
            if (showTargets != null)
            {
                showTargets(gameObject);
            }
        }

        else if (cardSelected != null && cardSelected != gameObject && selected != null)
        {
            selected(gameObject);
            cardSelected = null;
        }
    }
}
