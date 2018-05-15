using UnityEngine;

public abstract class PlayerAbstract : ScriptableObject 
{
    [SerializeField] protected HeroData[] cards;
    [SerializeField] protected Sprite profile;
    protected int deckPower;

    public HeroData[] getCards()
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

    protected void calculateDeckPower()
    {
        deckPower = 0;
        foreach (HeroData card in cards)
        {
            int cardPower = 0;
            cardPower += card.atk;
            cardPower += card.health;
            cardPower += card.def;

            deckPower += cardPower;
        }
    }
}
