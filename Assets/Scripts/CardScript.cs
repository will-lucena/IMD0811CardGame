using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Card cardInfos;

    [SerializeField] private Text cardName;
    [SerializeField] private Text cardDescription;
    [SerializeField] private Image cardImage;
    [SerializeField] private Text cardAtk;
    [SerializeField] private Text cardDef;
    [SerializeField] private Text cardHealth;

    void Start()
    {
        cardName.text = cardInfos.name;
        cardDescription.text = cardInfos.description;
        cardImage.sprite = cardInfos.image;
        cardAtk.text = cardInfos.atk.ToString();
        cardDef.text = cardInfos.def.ToString();
        cardHealth.text = cardInfos.health.ToString();
    }
}
