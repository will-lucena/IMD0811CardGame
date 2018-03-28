using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    public static event System.Action<Transform> showTargets;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (showTargets != null)
        {
            showTargets(transform.parent);
        }
    }
}
