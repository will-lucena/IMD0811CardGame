using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite image;
    public int atk;
    public int def;
    public int health;
    public Type type;
}
