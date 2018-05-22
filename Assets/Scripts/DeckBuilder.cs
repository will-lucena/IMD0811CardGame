using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour
{
    public Text cardsLeft;
    public Button buildButton;
    private List<CardData> cards;
    public int deckMaxSize;

	// Use this for initialization
	void Start ()
    {
        cards = new List<CardData>();
        DropZone.addCard += addCard;
        cardsLeft.text = (deckMaxSize - cards.Count) + " slots available";
        buildButton.interactable = false;
        GetComponent<DropZone>().enabled = true;
    }
	
    private void addCard(CardData card)
    {
        if (cards.Count < deckMaxSize)
        {
            cards.Add(card);
        }
        if (cards.Count >= deckMaxSize)
        {
            GetComponent<DropZone>().enabled = false;
            buildButton.interactable = true;
        }
        cardsLeft.text = (deckMaxSize - cards.Count) + " slots available";
    }

    public void buildDeck()
    {
        if (Persistance.currentDeck == 0)
        {
            Persistance.player1Deck = cards;
            Persistance.currentDeck++;
            LoadScene.loadScene("DeckBuilder");
        }
        else
        {
            Persistance.player2Deck = cards;
            Persistance.currentDeck = 0;
            LoadScene.loadScene("Game");
        }
    }

    private void OnDisable()
    {
        DropZone.addCard -= addCard;
    }

    private void OnDestroy()
    {
        DropZone.addCard -= addCard;
    }
}
