using UnityEngine;

public abstract class PlayerAbstract : ScriptableObject 
{
    [SerializeField] protected CardAbstract[] cards;
    [SerializeField] protected Sprite profile;
    protected int deckPower;

    public CardAbstract[] getCards()
    {
        return cards;
    }

    public Sprite getProfileImage()
    {
        return profile;
    }

    public int maxPower()
    {
        return deckPower;
    }
}
