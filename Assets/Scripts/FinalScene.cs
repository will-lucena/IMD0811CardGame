using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour
{
    public Image image;
    public Text message;

	// Use this for initialization
	void Start ()
    {
        image.sprite = Persistance.winner;
        message.text = Persistance.message;

        if (image.sprite == null)
        {
            image.color = new Color(0, 0, 0, 0);
        }
        else
        {
            image.color = new Color(255, 255, 255, 1);
        }
	}
}
