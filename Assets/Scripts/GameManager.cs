using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event System.Action cancelActions;

    private const float IdealWidth = 719;
    private const float IdealHeight = 404;

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
        float xAdjust = Screen.width / IdealWidth;
        float yAdjust = Screen.height / IdealHeight;
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<CardScript>().cardInfos = CARDS[Random.Range(0, CARDS.Length)];
        card.transform.SetParent(parent);
        card.transform.localScale = new Vector3(xAdjust, yAdjust, 1f);
        //card.transform.localScale = Vector3.one;
    }
}
