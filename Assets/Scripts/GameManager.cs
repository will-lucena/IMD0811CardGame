using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event System.Action cancelActions;


    public Card[] CARDS;
    [SerializeField] private GameObject cardPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //find a non hardcoded way to do it
        if (Input.GetMouseButton(1) && cancelActions != null)
        {
            cancelActions();
        }
    }

    public void pushCard(Transform parent)
    {
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<CardScript>().cardInfos = CARDS[Random.Range(0, CARDS.Length)];
        card.transform.SetParent(parent);
    }
}
