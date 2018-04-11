using UnityEngine;
using UnityEngine.EventSystems;
using Interfaces;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    public static event System.Action<GameObject> showTargets;
    public static event System.Action<GameObject> selected;

    public static GameObject cardSelected;

    private void Start()
    {
        cardSelected = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<IClickableAction>().onClickAction();
    }

    public static void checkShowTargets(GameObject obj)
    {
        if (showTargets != null)
        {
            showTargets(obj);
        }
    }

    public static void checkSelected(GameObject obj)
    {
        if (selected != null)
        {
            selected(obj);
        }
    }

    public void selfRemove()
    {
        Destroy(this);
    }
}
