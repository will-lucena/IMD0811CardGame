using UnityEngine;
using Enums;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private PlayerScript[] players;
    [SerializeField] private Sprite[] playerSprites;

    public static event System.Action battleLog;
    public static event System.Action cancelBattleLog;
    public static event System.Action<GameObject> moveToHand;
    public static event System.Action<GameObject, HeroCard> moveToDead;

    private HeroCard tempCard;
    private Coroutine cancelCoroutine;
    private PlayerScript currentActivePlayer;

    // Use this for initialization
    void Start()
    {
        Clickable.selected += checkSelection;
        Clickable.showTargets += attackingCard;

        players[0].deckToHand += instantiateCard;
        players[1].deckToHand += instantiateCard;
        players[0].endMyTurn += changeTurn;
        players[1].endMyTurn += changeTurn;

        HeroCard.notifyMyValor += updateScore;

        players[0].loadDeck(Persistance.player1Deck, playerSprites[0]);
        players[1].loadDeck(Persistance.player2Deck, playerSprites[1]);

        startGame();
    }

    public void instantiateCard(HeroData cardInfos, string cardTag, Transform parent, DeadZone deadZone)
    {
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<HeroCard>().data = cardInfos;
        card.GetComponent<HeroCard>().setDeadZone(deadZone);
        card.transform.SetParent(parent);
        card.transform.localScale = Vector3.one;
        card.tag = cardTag;
        Debug.Log(card.GetComponent<HeroCard>().data.name);
        if (moveToHand != null)
        {
            moveToHand(card);
        }
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
        tempCard = card.GetComponent<HeroCard>();
        tempCard.subscribeToBattle();
        cancelCoroutine = StartCoroutine(waitingToCancel());
    }

    private void battle(GameObject obj)
    {
        HeroCard card = obj.GetComponent<HeroCard>();
        card.subscribeToBattle();

        if (card.data.type == Type.HERO)
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
        tempCard.updateState(State.SLEEPING);
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

        for (int i = 0; i < 4; i++)
        {
            players[0].pickCard();
            players[1].pickCard();
        }

        players[playerIndex].waitingTurn();
        players[1 - playerIndex].startTurn();
    }

    private void changeTurn(GameObject player)
    {
        if (verifyEndGame())
        {
            endGame();
        }
        else if (players[0].gameObject == player)
        {
            currentActivePlayer = players[1];
            players[0].waitingTurn();
            players[1].startTurn();
        }
        else
        {
            currentActivePlayer = players[0];
            players[1].waitingTurn();
            players[0].startTurn();
        }
    }

    private void updateScore(int valor)
    {
        currentActivePlayer.updateScore(valor);
    }

    private bool verifyEndGame()
    {
        return players[0].getAvailableCards() == 0 && players[1].getAvailableCards() == 0;
    }

    private void endGame()
    {
        int score1 = players[0].getScore();
        int score2 = players[1].getScore();
        if (score1 > score2)
        {
            Persistance.winner = playerSprites[2];
            Persistance.message = "Win";
        }
        else if (score2 > score1)
        {
            Persistance.winner = playerSprites[3];
            Persistance.message = "Win";
        }
        else
        {
            Persistance.winner = playerSprites[4];
            Persistance.message = "Draw";
        }

        LoadScene.loadScene("Final");
    }
}
