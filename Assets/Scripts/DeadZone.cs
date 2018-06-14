using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class DeadZone : MonoBehaviour, IClickableAction
{
    private List<GameObject> cards;
    [SerializeField] private Sprite deadIcon;

	// Use this for initialization
	void Start ()
    {
        cards = new List<GameObject>();
	}
	
	public void addToStack(GameObject card)
    {
        card.GetComponent<CanvasGroup>().blocksRaycasts = false;
        card.transform.SetParent(transform);
        card.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        card.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        card.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        card.GetComponent<HeroCard>().state.sprite = deadIcon;
        card.GetComponent<HeroCard>().updateState(Enums.State.DEAD);
        cards.Add(card);
    }

    private void showAll()
    {
        foreach (GameObject c in cards)
        {
            Debug.Log(c.GetComponent<HeroCard>().show());
        }
    }

    public void onLeftClickAction()
    {
        showAll();
    }

    public void onRightClickAction(UnityEngine.EventSystems.PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
