using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    [SerializeField] private int atk;
    [SerializeField] private int def;
    [SerializeField] private int health;
    [SerializeField] private Type type;


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
