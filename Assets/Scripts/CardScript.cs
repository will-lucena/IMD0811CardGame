using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Interfaces;

public class CardScript : MonoBehaviour, ITargetable
{
    public Card cardInfos;

    [SerializeField] private Text cardName;
    [SerializeField] private Text cardDescription;
    [SerializeField] private Image cardImage;
    [SerializeField] private Text cardAtk;
    [SerializeField] private Text cardDef;
    [SerializeField] private Text cardHealth;
    [SerializeField] private Image border;

    public void subscribeToClickable()
    {
        if (cardInfos.type == Enums.Type.HERO)
        {
            Clickable.showTargets += targetMyself;
        }
    }

    void Start()
    {
        cardName.text = cardInfos.name;
        cardDescription.text = cardInfos.description;
        cardImage.sprite = cardInfos.image;
        cardAtk.text = cardInfos.atk.ToString();
        cardDef.text = cardInfos.def.ToString();
        cardHealth.text = cardInfos.health.ToString();

        subscribeToClickable();
    }

    private void targetMyself(Transform parent)
    {
        if (parent != transform.parent)
        {
            border.color = Color.green;
        }
    }
}
