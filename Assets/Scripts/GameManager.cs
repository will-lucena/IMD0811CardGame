using UnityEngine;
using Enums;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private PlayerScript player1;
    [SerializeField] private PlayerScript player2;


    public static event System.Action battleLog;
    public static event System.Action cancelBattleLog;
    public static event System.Action<GameObject> moveToHand;
    public static event System.Action<GameObject, CardScript> moveToDead;

    private CardScript tempCard;
    private Coroutine cancelCoroutine;

    // Use this for initialization
    void Start()
    {
        Clickable.selected += checkSelection;
        Clickable.showTargets += attackingCard;
        PlayerScript.deckToHand += instantiateCard;
    }

    public void instantiateCard(CardAbstract cardInfos, Transform parent, DeadZone deadZone)
    {
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<CardScript>().cardInfos = cardInfos;
        card.GetComponent<CardScript>().setDeadZone(deadZone);
        card.transform.SetParent(parent);
        card.transform.localScale = Vector3.one;
        moveToHand(card);
    }

    private void checkSelection(GameObject obj)
    {
        battle(obj);

        if (cancelBattleLog != null)
        {
            cancelBattleLog();
        }
    }

    private void attackingCard(GameObject card)
    {
        tempCard = card.GetComponent<CardScript>();
        tempCard.subscribeToBattle();
        cancelCoroutine = StartCoroutine(waitingToCancel());
    }

    private void battle(GameObject obj)
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

            if (card.health <= 0)
            {
                //change it to move to correct deadzone
                moveToDead(obj, card);
            }
        }
        StopCoroutine(cancelCoroutine);
    }

    private IEnumerator waitingToCancel()
    {
        while (!Input.GetMouseButton(1))
        {
            yield return null;
        }

        Clickable.cardSelected = null;
        if (cancelBattleLog != null)
        {
            cancelBattleLog();
        }
    }

    private IEnumerator startLoop()
    {


        yield return null;
    }

}
