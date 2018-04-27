using UnityEngine;
using Enums;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private PlayerScript[] players;

    public static event System.Action battleLog;
    public static event System.Action cancelBattleLog;
    public static event System.Action<GameObject> moveToHand;
    public static event System.Action<GameObject, CardScript> moveToDead;

    private CardScript tempCard;
    private Coroutine cancelCoroutine;
    private GameObject activePlayer;

    // Use this for initialization
    void Start()
    {
        checkMultiDisplay();

        Clickable.selected += checkSelection;
        Clickable.showTargets += attackingCard;

        players[0].deckToHand += instantiateCard;
        players[1].deckToHand += instantiateCard;
        players[0].endMyTurn += changeTurn;
        players[1].endMyTurn += changeTurn;
        startGame();
    }

    private void checkMultiDisplay()
    {
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
        /*
        else
        {
            Camera c1 = GameObject.Find("Camera1").GetComponent<Camera>();
            Camera c2 = GameObject.Find("Camera2").GetComponent<Camera>();

            c1.targetDisplay = 0;
            c2.targetDisplay = 0;

            c1.rect = new Rect(0, 0, 1, 0.5f);
            c2.rect = new Rect(0, 0.5f, 1, 1);
        }
        /**/
    }

    public void instantiateCard(CardAbstract cardInfos, string cardTag, Transform parent, DeadZone deadZone)
    {
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<CardScript>().cardInfos = cardInfos;
        card.GetComponent<CardScript>().setDeadZone(deadZone);
        card.transform.SetParent(parent);
        card.transform.localScale = Vector3.one;
        card.transform.localPosition = new Vector3(0, 0, 0);
        card.tag = cardTag;
        card.layer = parent.gameObject.layer;
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
                moveToDead(obj, card);
            }
        }
        StopCoroutine(cancelCoroutine);
        tempCard.canAttack = false;
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

    private void startGame()
    {
        int playerIndex = Random.Range(0, 2);
        activePlayer = players[1 - playerIndex].gameObject;
        players[playerIndex].waitingTurn();
        players[1 - playerIndex].startTurn();
    }

    private void changeTurn(GameObject player)
    {
        if (players[0].gameObject == player)
        {
            players[0].waitingTurn();
            players[1].startTurn();
            activePlayer = players[1].gameObject;
            Camera.main.targetDisplay = 1;
        }
        else
        {
            players[1].waitingTurn();
            players[0].startTurn();
            activePlayer = players[0].gameObject;
            Camera.main.targetDisplay = 0;
        }
    }
}
