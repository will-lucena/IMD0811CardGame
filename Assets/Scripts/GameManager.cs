using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class GameManager : MonoBehaviour
{
    public Card[] CARDS;
    [SerializeField] private GameObject cardPrefab;

    public static event System.Action battleLog;
    public static event System.Action cancelBattleLog;


    private CardScript tempCard;

    // Use this for initialization
    void Start()
    {
        Clickable.selected += checkSelection;
        Clickable.showTargets += attackingCard;
    }

    // Update is called once per frame
    void Update()
    {
        //find a non hardcoded way to do it
        if (Input.GetMouseButton(1))
        {
            Clickable.cardSelected = null;
            if (cancelBattleLog != null)
            {
                cancelBattleLog();
            }
        }
    }

    public void pushCard(Transform parent)
    {
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<CardScript>().cardInfos = CARDS[Random.Range(0, CARDS.Length)];
        card.transform.SetParent(parent);
        card.transform.localScale = Vector3.one;
    }

    private void checkSelection(GameObject obj)
    {
        CardScript card = obj.GetComponent<CardScript>();
        card.subscribeToBattle();

        if (card.cardInfos.getType() == Type.HERO)
        {
            int damage = tempCard.power - card.armor;

            if (damage > 0)
            {
                card.armor = 0;
                card.health -= damage;
            }
            else if (damage == 0)
            {
                card.armor = 0;
            }
            else
            {
                card.armor -= tempCard.power;
            }
            if (battleLog != null)
            {
                battleLog();
            }
        }

        if (cancelBattleLog != null)
        {
            cancelBattleLog();
        }
    }

    private void attackingCard(GameObject card)
    {
        tempCard = card.GetComponent<CardScript>();
        tempCard.subscribeToBattle();
    }
}
