using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "Hero", menuName = "Cards/Hero")]
public class CardHero : CardAbstract
{
    // Use this for initialization
    void Start()
    {
        type = Type.HERO;
    }
}
