using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
    [SerializeField] private Image profile;
    [SerializeField] private Text availableCards;
    [SerializeField] private Text currentScore;
    [SerializeField] private Player infos;

    private List<Card> deck;
    private List<Card> hand;
    private List<Card> battleField;
    private List<Card> dead;

    private void Start()
    {
        loadDeck();
        loadProfileInfos();
        
        currentScore.text = 0.ToString();
        availableCards.text = deck.Count.ToString();
    }

    private void loadDeck()
    {
        foreach (Card c in infos.getCards())
        {
            deck.Add(c);
        }
    }

    private void loadProfileInfos()
    {
        profile.sprite = infos.getProfileImage().sprite;
    }

    public Card pickCard()
    {
        Card c = deck.ToArray()[Random.Range(0, deck.Count)];

        deck.Remove(c);
        hand.Add(c);

        return c;
    }
}
