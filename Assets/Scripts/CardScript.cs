using UnityEngine;
using UnityEngine.UI;
using Interfaces;
using Enums;

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

    public int power;
    public int armor;
    public int health;

    public void subscribeToClickable()
    {
        if (cardInfos.getType() == Type.HERO)
        {
            Clickable.showTargets += targetMyself;
        }
    }

    void Start()
    {
        GameManager.cancelBattleLog += outOfBattle;
        power = cardInfos.getAtk();
        armor = cardInfos.getDef();
        health = cardInfos.getHp();
        updateDisplay();
        subscribeToClickable();
        border.color = Color.white;
    }

    private void targetMyself(GameObject card)
    {
        if (!inHand() && !isMyCard(card.transform.parent))
        {
            border.color = Color.green;
        }
    }

    private bool inHand()
    {
        return transform.parent.CompareTag("Hand");
    }

    private bool isMyCard(Transform cardParent)
    {
        return transform.parent.CompareTag(cardParent.tag);
    }

    public void subscribeToBattle()
    {
        GameManager.battleLog += updateDisplay;
        Debug.Log(cardName.text + " entrou em batalha");
    }

    private void outOfBattle()
    {
        GameManager.battleLog -= updateDisplay;
        Debug.Log(cardName.text + " saiu da batalha");
        border.color = Color.white;
    }

    private void updateDisplay()
    {
        cardName.text = cardInfos.name;
        cardDescription.text = cardInfos.getDescription();
        cardImage.sprite = cardInfos.getImage();
        cardAtk.text = power.ToString();
        cardDef.text = armor.ToString();
        cardHealth.text = health.ToString();

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
