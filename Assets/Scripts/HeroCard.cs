using UnityEngine;
using UnityEngine.UI;
using Interfaces;
using Enums;
using System.Text;

public class HeroCard : MonoBehaviour, ITargetable, IClickableAction
{
    public static event System.Action<int> notifyMyValor;

    [HideInInspector] public HeroData data;
    [HideInInspector] public int power;
    [HideInInspector] public int armor;
    [HideInInspector] public int health;
    private DeadZone deadZone;
    public int turnCount;
    public bool attackTurn;
    public bool canAttack;
    private int valor;

    [SerializeField] private Text cardName;
    [SerializeField] private Text cardDescription;
    [SerializeField] private Image cardImage;
    [SerializeField] private Text cardAtk;
    [SerializeField] private Text cardDef;
    [SerializeField] private Text cardHealth;
    [SerializeField] private Image border;
    
    public void subscribeToClickable()
    {
        if (data.type == Type.HERO)
        {
            Clickable.showTargets += targetMyself;
        }
    }

    void Start()
    {
        power = data.atk;
        armor = data.def;
        updateDisplay();
        border.color = Color.white;
        turnCount = 0;
        valor = calculateCardValor();
    }

    private int calculateCardValor()
    {
        return data.atk + data.def + data.health;
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
        cardName.text = data.name;
        cardDescription.text = data.description;
        cardImage.sprite = data.image;
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
        sb.Append(data.name);

        return sb.ToString();
    }

    public void onClickAction()
    {
        if (Clickable.cardSelected == null && attackTurn && canAttack)
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
        if (!attackTurn)
        {
            notifyMyValor(valor);
        }
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

    public void handView(bool isActive)
    {
        foreach (Image img in GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("Back"))
            {
                img.enabled = isActive;
            }
        }
    }
}
