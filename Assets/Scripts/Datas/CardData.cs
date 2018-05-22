using UnityEngine;
using Enums;

public abstract class CardData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite image;
    public int atk;
    public int def;
    public Type type;
}
