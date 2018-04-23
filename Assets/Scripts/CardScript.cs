using UnityEngine;
using UnityEngine.UI;
using Interfaces;
using Enums;
using System.Text;

public class CardScript : MonoBehaviour, ITargetable, IClickableAction
{
    [HideInInspector] public CardAbstract cardInfos;
    [HideInInspector] public int power;
    [HideInInspector] public int armor;
    [HideInInspector] public int health;
    private DeadZone deadZone;
    public int turnCount;

    [SerializeField] private Text cardName;
    [SerializeField] private Text cardDescription;
    [SerializeField] private Image cardImage;
    [SerializeField] private Text cardAtk;
    [SerializeField] private Text cardDef;
    [SerializeField] private Text cardHealth;
    [SerializeField] private Image border;
    
    public void subscribeToClickable()
    {
        if (cardInfos.getType() == Type.HERO)
        {
            Clickable.showTargets += targetMyself;
        }
    }

    void Start()
    {
        power = cardInfos.getAtk();
        armor = cardInfos.getDef();
        health = cardInfos.getHp();
        updateDisplay();
        border.color = Color.white;
        turnCount = 0;
    }

    private void targetMyself(GameObject card)
    {
        if (card != gameObject)
        {
            border.color = Color.green;
            GameManager.cancelBattleLog += outOfBattle;
        }
    }

    public void subscribeToBattle()
    {
        GameManager.battleLog += updateDisplay;
    }

    private void outOfBattle()
    {
        GameManager.battleLog -= updateDisplay;
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
    }

    private void lastDisplayUpdate()
    {
        border.color = Color.grey;
        cardAtk.text = power.ToString();
        cardDef.text = 0.ToString();
        cardHealth.text = 0.ToString();
        GameManager.battleLog -= updateDisplay;
    }

    public string show()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Card: ");
        sb.Append(cardInfos.name);

        return sb.ToString();
    }

    public void onClickAction()
    {
        if (Clickable.cardSelected == null)
        {
            Clickable.cardSelected = gameObject;
            Clickable.checkShowTargets(gameObject);
        }
        else if (Clickable.cardSelected != null && Clickable.cardSelected != gameObject)
        {
            Clickable.checkSelected(gameObject);
            Clickable.cardSelected = null;
        }
    }

    public void subscribeToCancelLog()
    {
        GameManager.cancelBattleLog += outOfBattle;
    }

    public void selfRemove()
    {
        Destroy(this);
    }

    public void dead()
    {
        lastDisplayUpdate();
        deadZone.addToStack(gameObject);
        GetComponent<Clickable>().enabled = false;
        Clickable.showTargets -= targetMyself;
    }

    private void OnDisable()
    {
        GameManager.battleLog -= updateDisplay;
        Clickable.showTargets -= targetMyself;
    }

    public void setDeadZone(DeadZone zone)
    {
        deadZone = zone;
    }
}
