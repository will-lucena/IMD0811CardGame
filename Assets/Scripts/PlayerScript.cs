using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public event System.Action<CardAbstract, string, Transform, DeadZone> deckToHand;
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

    private List<CardAbstract> deck;
    public List<GameObject> hand;
    public List<GameObject> battleField;

    private void Awake()
    {
        loadDeck();
        //loadProfileInfos();
        availableCards.text = deck.Count.ToString();
    }

    private void Start()
    {
        GameManager.moveToHand += addToHand;
        GameManager.moveToDead += addToDead;
        Draggable.handToBattle += addToBattleField;
    }

    private void loadDeck()
    {
        deck = new List<CardAbstract>();
        hand = new List<GameObject>();
        battleField = new List<GameObject>();
        foreach (CardAbstract c in infos.getCards())
        {
            deck.Add(c);
        }
    }

    private void loadProfileInfos()
    {
        profile.sprite = infos.getProfileImage().sprite;
        availableCards.text = deck.Count.ToString();
        currentScore.text = 0.ToString();
    }

    public void pickCard()
    {
        if (deck.Count > 0)
        {
            CardAbstract c = deck.ToArray()[Random.Range(0, deck.Count)];
            deck.Remove(c);
            deckToHand(c, tagToCard, handTransform, deadZone);
            availableCards.text = deck.Count.ToString();
        }
        else
        {
            //disable pick button
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

    private void addToDead(GameObject obj, CardScript card)
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
            card.GetComponent<CardScript>().subscribeToClickable();
        }
    }

    public void finishTurn()
    {
        foreach (GameObject card in battleField)
        {
            card.GetComponent<CardScript>().turnCount++;
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

        foreach (GameObject card in battleField)
        {
            card.GetComponent<Clickable>().enabled = false;
            Debug.Log(card.name);
        }

    }

    public void startTurn()
    {
        pick.interactable = true;
        battleZone.enabled = true;

        foreach (GameObject card in battleField)
        {
            if (card.GetComponent<CardScript>().turnCount > 0)
            {
                card.GetComponent<Clickable>().enabled = true;
            }
        }
    }
}
