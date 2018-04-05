using UnityEngine;
using Enums;

public abstract class Card : ScriptableObject
{
    [SerializeField] protected new string name;
    [SerializeField] protected string description;
    [SerializeField] protected Sprite image;
    [SerializeField] protected int atk;
    [SerializeField] protected int def;
    [SerializeField] protected int health;
    protected Type type;

    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }

    public Sprite getImage()
    {
        return image;
    }

    public Type getType()
    {
        return type;
    }

    public int getAtk()
    {
        return atk;
    }

    public int getDef()
    {
        return def;
    }

    public int getHp()
    {
        return health;
    }
}
