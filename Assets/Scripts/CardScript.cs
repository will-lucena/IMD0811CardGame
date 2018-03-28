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

    private bool aimed;

    public void subscribeToClickable()
    {
        if (cardInfos.type == Enums.Type.HERO)
        {
            Clickable.showTargets += targetMyself;
            //unhardcoded this
            GameManager.cancelActions += cancelAtk;
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
        aimed = false;
    }

    private void Update()
    {
        //implement it with animations and remove from update
        if (aimed)
        {
            border.color = Color.green;
        }
        else
        {
            border.color = Color.white;
        }
    }

    private void targetMyself(Transform parent)
    {
        //fix bug
        if (!transform.parent.CompareTag("Hand") && parent != transform.parent)
        {
            //change state to Aimed
            aimed = true;
        }
    }

    private void cancelAtk()
    {
        aimed = false;
    }

}
