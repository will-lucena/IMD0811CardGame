using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistance : MonoBehaviour
{
    public static Persistance INSTANCE;

    public static Sprite winner = null;
    public static string message = "";
    public static int currentDeck = 0;
    public static List<CardData> player1Deck = new List<CardData>();
    public static List<CardData> player2Deck = new List<CardData>();

    void Awake()
    {
        if (INSTANCE == null)
        {
            DontDestroyOnLoad(gameObject);
            INSTANCE = this;
        }

        if (INSTANCE != this)
        {
            Destroy(gameObject);
        }
    }
}
