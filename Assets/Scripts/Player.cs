using UnityEngine;
using UnityEngine.UI;

public abstract class Player : ScriptableObject 
{
    [SerializeField] protected Card[] cards;
    [SerializeField] protected Image profile;
    protected int deckPower;

    public Card[] getCards()
    {
        return cards;
    }

    public Image getProfileImage()
    {
        return profile;
    }

    public int maxPower()
    {
        return deckPower;
    }
}
