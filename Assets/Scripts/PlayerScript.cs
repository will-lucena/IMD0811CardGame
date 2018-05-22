using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public event System.Action<HeroData, string, Transform, DeadZone> deckToHand;
    public event System.Action<GameObject> endMyTurn;

    [SerializeField] private Image profile;
    [SerializeField] private Text availableCards;
    [SerializeField] private Text currentScore;
    [SerializeField] private PlayerAbstract infos;
    [SerializeField] private Transform handTransform;
    [SerializeField] private DeadZone deadZone;
    [SerializeField] private Button pick;
    [SerializeField] private DropZone battleZone;
    [SerializeField] private string tagToCard;
    [SerializeField] private GameObject pickButton;
    [SerializeField] private GameObject doneButton;

    private List<HeroData> deck;
    public List<GameObject> hand;
    public List<GameObject> battleField;

    private void Awake()
    {
        loadDeck();
        loadProfileInfos();
    }

    private void Start()
    {
        GameManager.moveToHand += addToHand;
        GameManager.moveToDead += addToDead;
        Draggable.handToBattle += addToBattleField;
    }

    private void loadDeck()
    {
        deck = new List<HeroData>();
        hand = new List<GameObject>();
        battleField = new List<GameObject>();
        foreach (HeroData c in infos.getCards())
        {
            deck.Add(c);
        }
    }

    private void loadProfileInfos()
    {
        profile.sprite = infos.getProfileImage();
        availableCards.text = deck.Count.ToString();
        currentScore.text = 0.ToString();
    }

    public void pickCard()
    {
        if (deck.Count > 0)
        {
            HeroData c = deck.ToArray()[Random.Range(0, deck.Count)];
            deck.Remove(c);
            deckToHand(c, tagToCard, handTransform, deadZone);
            availableCards.text = deck.Count.ToString();
        }
    }

    private void addToHand(GameObject card)
    {
        if (card.CompareTag(tag))
        {
            hand.Add(card);
            card.GetComponent<Clickable>().enabled = false;
        }
    }

    private void addToDead(GameObject obj, HeroCard card)
    {
        if (card.CompareTag(tag))
        {
            battleField.Remove(obj);
            card.dead();
        }
    }

    private void addToBattleField(GameObject card)
    {
        if (card.CompareTag(tag))
        {
            hand.Remove(card);
            battleField.Add(card);
            card.GetComponent<Draggable>().enabled = false;
            card.GetComponent<Clickable>().enabled = true;
            card.GetComponent<HeroCard>().subscribeToClickable();
        }
    }

    public void finishTurn()
    {
        if (deck.Count <= 0)
        {
            pickButton.SetActive(false);
            doneButton.SetActive(true);
        }

        foreach (GameObject card in battleField)
        {
            card.GetComponent<HeroCard>().turnCount++;
        }

        if (endMyTurn != null)
        {
            endMyTurn(gameObject);
        }
    }

    public void waitingTurn()
    {
        pick.interactable = false;
        battleZone.enabled = false;

        foreach (GameObject obj in battleField)
        {
            HeroCard card = obj.GetComponent<HeroCard>();
            card.attackTurn = false;
            card.updateState(Enums.State.SLEEPING);
        }
        updateHandView(true);
    }

    public void startTurn()
    {
        pick.interactable = true;
        battleZone.enabled = true;

        foreach (GameObject obj in battleField)
        {
            HeroCard card = obj.GetComponent<HeroCard>();
            if (card.turnCount > 0)
            {
                card.attackTurn = true;
                card.canAttack = true;
                card.updateState(Enums.State.ACTIVE);
            }
        }
        updateHandView(false);
    }

    private void updateHandView(bool isActive)
    {
        foreach (GameObject obj in hand)
        {
            HeroCard card = obj.GetComponent<HeroCard>();
            card.handView(isActive);
        }
    }

    public void updateScore(int score)
    {
        currentScore.text = (int.Parse(currentScore.text) + score).ToString();
    }

    public int getScore()
    {
        return int.Parse(currentScore.text);
    }

    public int getAvailableCards()
    {
        return int.Parse(availableCards.text);
    }

    public Sprite getImage()
    {
        return profile.sprite;
    }
}
