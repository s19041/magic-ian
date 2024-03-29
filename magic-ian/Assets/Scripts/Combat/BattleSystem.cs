using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{ START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour// WIELKA KLASA KT�RA ��CZY WSZYSTKO W CA�EJ WALCE. Prosz� nie krytykowa� jako�ci kodu tutaj. Tylko geniusz mo�e zapanowa� nad chaosem
{
    public BattleState state;
    //public CardDisplay cardDisplay;

    private GameObject playerPrefab;
    [SerializeField] private List<GameObject> enemyPrefabs;

    public Transform playerBattleStation;
    public List<Transform> enemyBattleStationList;

    public BattleHUD playerHUD;
    public List<BattleHUD> enemyHUDList;

    public CardDisplay combatCardDisplay;

    public TextMeshProUGUI dialogueText;
    public Text itemButtonText1;
    public Text itemButtonText2;
    private Unit playerUnit;

    [SerializeField]
    private List<Unit> enemyUnitList;

    private Deck deck;
    private Card currentCard;

    private AbstractAbilitySet abilitySet;

    private int turn;
    private ItemPowers itemPowers;
    private CombatRoom currentRoom;

    // Start is called before the first frame update
    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame

    private IEnumerator SetupBattle()
    {
        Debug.Log(0);
        currentRoom = DungeonManager.Instance.GetCurrentCombatRoom();

        LoadPlayer();

        LoadEnemies();

        dialogueText.text = currentRoom.encounterName + " encountered";//fajnie by by�a jak�� klase machna� na cz�� ui BattleSystem.
                                                                       //A mo�e nie... Kto wie jak powinno si� to zrobi� tak �eby by�o dobrze
                                                                       //Moim zdaniem to nie ma tak, �e dobrze albo �e nie dobrze.
                                                                       //Gdybym mia� powiedzie�, co ceni� w �yciu najbardziej, powiedzia�bym, �e ludzi.
                                                                       //Ekhm� Ludzi, kt�rzy podali mi pomocn� d�o�, kiedy sobie nie radzi�em, kiedy by�em sam.
                                                                       //I co ciekawe, to w�a�nie przypadkowe spotkania wp�ywaj� na nasze �ycie.
                                                                       //Chodzi o to, �e kiedy wyznaje si� pewne warto�ci, nawet pozornie uniwersalne, bywa, �e nie znajduje si� zrozumienia,
                                                                       //kt�re by tak rzec, kt�re pomaga si� nam rozwija�. Ja mia�em szcz�cie, by tak rzec, poniewa� je znalaz�em.
                                                                       //I dzi�kuj� �yciu. Dzi�kuj� mu, �ycie to �piew, �ycie to taniec, �ycie to mi�o��. Wielu ludzi pyta mnie o to samo,
                                                                       //ale jak ty to robisz? Sk�d czerpiesz t� rado��? A ja odpowiadam, �e to proste, to umi�owanie �ycia,
                                                                       //to w�a�nie ono sprawia, �e dzisiaj na przyk�ad buduj� maszyny, a jutro� kto wie, dlaczego by nie,
                                                                       //oddam si� pracy spo�ecznej i b�d� ot, cho�by sadzi� znaczy� marchew.

        deck = Deck.Instance;
        deck.cardDisplay = combatCardDisplay;
        combatCardDisplay.UpdateDisplay();

        abilitySet = deck.abilitySet;
        itemPowers = deck.itemPowers;

        deck.Shuffle();
        deck.SetDeckForCombat(combatCardDisplay);
        SetItemButtonsText(deck.item1, deck.item2);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Your turn";
        currentCard = deck.GetTopCard();
        //cardDisplay.card = currentCard;
        //cardDisplay.UpdateDisplay();
        turn++;
    }

    private IEnumerator PlayerPlayCard()
    {
        // tu powinna by� ca�a logika z dzia�aniem kart.
        //nie wiem jak rozwi�za� spraw� kart specjalnych. Pewnie sprawdzanie ich ifami i tutaj dzia�anie wpisywa� ale to troch� s�abe(ale �atwe)

        //currentCard = deck.GetTopCard();
        bool check = currentCard.hasAbility;

        while (check)//logika dla kart specjalnych. Wp�ywaj� one na nast�pne karty a nawet czasami na nich dzia�aj� wi�c trzeba sprawdzi� czy ich nie pozamienia�y
        {
            check = abilitySet.PlayAbility(currentCard);
            Debug.Log("Ability casted");
            deck.SetTopCard();
            currentCard = deck.GetTopCard();
        }
        playerUnit.Heal(currentCard.heal);
        playerUnit.ArmorUp(currentCard.armor);

        DealDamage();//pawe�

        if (currentCard == deck.topCard)//w przypadku podmiany current card, nie przestawi karty w decku
            deck.CardPlayed();

        Debug.Log("Card played");

        UpdateHuds();
        Debug.Log("Hud updated");
        bool areDead = true;

        foreach (Unit enemyUnit in enemyUnitList)
        {
            if (enemyUnit.hp > 0)
                areDead = false;
        }

        Debug.Log("Hp checked");

        if (areDead)
        {
            state = BattleState.WON;
            yield return new WaitForSeconds(2f);
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnFirstItemActivationButton()
    {
        ActivateItem(deck.item1);
    }

    public void OnSecondItemActivationButton()
    {
        ActivateItem(deck.item2);
    }

    public void ActivateItem(Item item)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        bool noActionPointsLeft = itemPowers.ActivateItemPower(item);
        currentCard = deck.GetTopCard();

        if (item.itemName == ItemName.CAPE)
        {
            currentCard = itemPowers.capeCard;
            StartCoroutine(PlayerPlayCard());
        }

        if (noActionPointsLeft)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnPlayCardButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerPlayCard());
    }

    private IEnumerator EnemyTurn()
    {
        itemPowers.shuffleCount = 0;
        Debug.Log("Enemy turn");
        dialogueText.text = currentRoom.encounterName + " move";
        bool isDead = false;
        yield return new WaitForSeconds(1f);

        for (int i = enemyUnitList.Count - 1; i >= 0; i--)
        {
            if (enemyUnitList[i].hp > 0)
            {
                dialogueText.text = enemyUnitList[i].unitName + " move";
                yield return new WaitForSeconds(0.5f);
                isDead = playerUnit.TakeDamage(enemyUnitList[i].combatAi.doSomething(playerUnit, enemyUnitList, turn, i, enemyUnitList[i].hp, dialogueText));
                yield return new WaitForSeconds(1f);
            }
        }

        UpdateHuds();

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won";
            MainCharacter.Instance.ResetStats();
            PlayerManager.Instance.AddGold(currentRoom.goldReward);
            DungeonManager.Instance.EnableNextSceneButton();
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost";
        }
        deck.Reset();
    }

    public void DealDamage()
    {
        if (currentCard.aoe)
        {
            foreach (Unit enemyUnit in enemyUnitList)
            {
                enemyUnit.TakeDamage(currentCard.damage);//aoe wiadomo
                enemyUnit.AddStunStacks(currentCard.stunStacks);
            }
            currentCard.aoe = false;
        }
        else
        {
            for (int i = enemyUnitList.Count - 1; i >= 0; i--)
            {
                if (enemyUnitList[i].hp > 0)
                {
                    enemyUnitList[i].TakeDamage(currentCard.damage);//klepanie frontalnego przeciwnika
                    enemyUnitList[i].AddStunStacks(currentCard.stunStacks);
                    break;
                }
            }
        }
    }

    private void UpdateHuds()
    {
        playerHUD.SetStats(playerUnit.hp, playerUnit.maxHp, playerUnit.armor);
        for (int i = 0; i < enemyUnitList.Count; i++)
        {
            enemyHUDList[2 - i].SetStats(enemyUnitList[i].hp, enemyUnitList[i].maxHp, enemyUnitList[i].armor);
        }
    }

    public void ShowShuffledCardsInDeck()
    {
        DeckDisplay.Instance.ShowDeckShuffled();
    }

    public void ShowGraveyard()
    {
        DeckDisplay.Instance.ShowGraveyard();
    }

    public void LoadPlayer()
    {
        GameObject playerGO = MainCharacter.Instance.gameObject;
        playerGO.transform.position = playerBattleStation.position;
        playerUnit = playerGO.GetComponent<Unit>();
        playerHUD.SetHud(playerUnit);
    }

    public void LoadEnemies()
    {
        enemyPrefabs = currentRoom.GetOpponents();
        List<GameObject> enemyGOs = new List<GameObject>();
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            enemyGOs.Add(Instantiate(enemyPrefabs[i], enemyBattleStationList[2 - i]));
        }
        enemyUnitList = new List<Unit>();
        foreach (GameObject enemyGO in enemyGOs)
        {
            enemyUnitList.Add(enemyGO.GetComponent<Unit>());
        }

        for (int i = 0; i < enemyUnitList.Count; i++)
        {
            enemyBattleStationList[2 - i].GetComponentInChildren<BattleHUD>().SetHud(enemyUnitList[i]);//kolejno�� jest od prawej do lewej
        }
        for (int i = enemyUnitList.Count; i < 3; i++)//dezaktywacja niepotrzebnych hud
        {
            enemyBattleStationList[2 - i].gameObject.SetActive(false);
        }
    }

    public void SetItemButtonsText(Item item1, Item item2)
    {
        itemButtonText1.text = item1.buttonText;
        itemButtonText2.text = item2.buttonText;
    }
}