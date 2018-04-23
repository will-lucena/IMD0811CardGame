using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
    public static event System.Action<CardAbstract, Transform, DeadZone> deckToHand;
    public event System.Action endMyTurn;

    [SerializeField] private Image profile;
    [SerializeField] private Text availableCards;
    [SerializeField] private Text currentScore;
    [SerializeField] private PlayerAbstract infos;
    [SerializeField] private Transform handTransform;
    [SerializeField] private DeadZone deadZone;

    private List<CardAbstract> deck;
    private List<GameObject> hand;
    private List<GameObject> battleField;

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
            deckToHand(c, handTransform, deadZone);
            availableCards.text = deck.Count.ToString();
        }
        else
        {
            //disable pick button
        }
    }

    private void addToHand(GameObject card)
    {
        hand.Add(card);
        card.GetComponent<Clickable>().enabled = false;
    }

    private void addToDead(GameObject obj, CardScript card)
    {
        hand.Remove(obj);
        card.dead();
    }

    private void addToBattleField(GameObject card)
    {
        hand.Remove(card);
        battleField.Add(card);
        card.GetComponent<Draggable>().enabled = false;
        card.GetComponent<Clickable>().enabled = true;
        card.GetComponent<CardScript>().subscribeToClickable();
    }

    public void finishTurn()
    {
        if (endMyTurn != null)
        {
            endMyTurn();
        }
    }
}
