using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerAbstract : ScriptableObject 
{
    [SerializeField] protected CardAbstract[] cards;
    [SerializeField] protected Image profile;
    protected int deckPower;

    public CardAbstract[] getCards()
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
