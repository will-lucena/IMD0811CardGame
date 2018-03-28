using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Card[] CARDS;
    [SerializeField] private GameObject cardPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pushCard(Transform parent)
    {
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<CardScript>().cardInfos = CARDS[Random.Range(0, CARDS.Length)];
        card.transform.SetParent(parent);

    }
}
