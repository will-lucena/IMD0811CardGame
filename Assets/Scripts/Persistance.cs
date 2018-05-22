using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistance : MonoBehaviour
{
    public static Persistance INSTANCE;

    public static Sprite winner = null;
    public static string message = "";

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
