using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };
public class BattleSystem : MonoBehaviour// WIELKA KLASA KTÓRA £¥CZY WSZYSTKO W CA£EJ WALCE. Proszê nie krytykowaæ jakoœci kodu tutaj. Tylko geniusz mo¿e zapanowaæ nad chaosem
{
    public BattleState state;
    //public CardDisplay cardDisplay;

    private GameObject playerPrefab;
    [SerializeField] List<GameObject> enemyPrefabs;

    public Transform playerBattleStation;
    public List<Transform> enemyBattleStationList;

    public BattleHUD playerHUD;
    public List<BattleHUD> enemyHUDList;

    public CardDisplay combatCardDisplay;

    public TextMeshProUGUI dialogueText;
    Unit playerUnit;
    [SerializeField]
    List<Unit> enemyUnitList;

    private Deck deck;
    Card currentCard;


    AbstractAbilitySet abilitySet;

    private int turn;
    private ItemPowers itemPowers;
    private CombatRoom currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame

    IEnumerator SetupBattle()
    {
        Debug.Log(0);
        currentRoom = DungeonManager.Instance.GetCurrentCombatRoom();

        GameObject playerGO = MainCharacter.Instance.gameObject;
        playerGO.transform.position = playerBattleStation.position;
        playerUnit = playerGO.GetComponent<Unit>();


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



        dialogueText.text = currentRoom.encounterName + " encountered";//fajnie by by³a jak¹œ klase machnaæ na czêœæ ui BattleSystem.
                                                                       //A mo¿e nie... Kto wie jak powinno siê to zrobiæ tak ¿eby by³o dobrze
                                                                       //Moim zdaniem to nie ma tak, ¿e dobrze albo ¿e nie dobrze.
                                                                       //Gdybym mia³ powiedzieæ, co ceniê w ¿yciu najbardziej, powiedzia³bym, ¿e ludzi.
                                                                       //Ekhm… Ludzi, którzy podali mi pomocn¹ d³oñ, kiedy sobie nie radzi³em, kiedy by³em sam.
                                                                       //I co ciekawe, to w³aœnie przypadkowe spotkania wp³ywaj¹ na nasze ¿ycie.
                                                                       //Chodzi o to, ¿e kiedy wyznaje siê pewne wartoœci, nawet pozornie uniwersalne, bywa, ¿e nie znajduje siê zrozumienia,
                                                                       //które by tak rzec, które pomaga siê nam rozwijaæ. Ja mia³em szczêœcie, by tak rzec, poniewa¿ je znalaz³em.
                                                                       //I dziêkujê ¿yciu. Dziêkujê mu, ¿ycie to œpiew, ¿ycie to taniec, ¿ycie to mi³oœæ. Wielu ludzi pyta mnie o to samo,
                                                                       //ale jak ty to robisz? Sk¹d czerpiesz tê radoœæ? A ja odpowiadam, ¿e to proste, to umi³owanie ¿ycia,
                                                                       //to w³aœnie ono sprawia, ¿e dzisiaj na przyk³ad budujê maszyny, a jutro… kto wie, dlaczego by nie,
                                                                       //oddam siê pracy spo³ecznej i bêdê ot, choæby sadziæ… znaczy… marchew.


        playerHUD.SetHud(playerUnit);

        for (int i = 0; i < enemyUnitList.Count; i++)
        {
            enemyBattleStationList[2 - i].GetComponentInChildren<BattleHUD>().SetHud(enemyUnitList[i]);//kolejnoœæ jest od prawej do lewej
        }
        for (int i = enemyUnitList.Count; i < 3; i++)//dezaktywacja niepotrzebnych hud
        {
            enemyBattleStationList[2-i].gameObject.SetActive(false);
        }


        deck = Deck.Instance;
        deck.cardDisplay = combatCardDisplay;
        combatCardDisplay.updateDisplay();


        abilitySet = new AbilitySet1();

        itemPowers = new ItemPowers();
        deck.Shuffle();
        deck.SetDeckForCombat(combatCardDisplay);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    void PlayerTurn()
    {

        dialogueText.text = "Your turn";
        currentCard = deck.GetTopCard();
        //cardDisplay.card = currentCard;
        //cardDisplay.updateDisplay();
        turn++;
    }

    IEnumerator PlayerPlayCard()
    {
        // tu powinna byæ ca³a logika z dzia³aniem kart. 
        //nie wiem jak rozwi¹zaæ sprawê kart specjalnych. Pewnie sprawdzanie ich ifami i tutaj dzia³anie wpisywaæ ale to trochê s³abe(ale ³atwe)

        //currentCard = deck.GetTopCard();
        bool check = currentCard.hasAbility;

        while (check)//logika dla kart specjalnych. Wp³ywaj¹ one na nastêpne karty a nawet czasami na nich dzia³aj¹ wiêc trzeba sprawdziæ czy ich nie pozamienia³y
        {
            check = abilitySet.PlayAbility(currentCard);
            Debug.Log("Ability casted");
            deck.SetTopCard();
            currentCard = deck.GetTopCard();

        }
        playerUnit.Heal(currentCard.heal);
        playerUnit.ArmorUp(currentCard.armor);

        DealDamage();//pawe³

        deck.CardPlayed();

        Debug.Log("Card played");


        playerHUD.SetStats(playerUnit.hp, playerUnit.maxHp, playerUnit.armor);
        for (int i = 0; i < enemyUnitList.Count; i++)
        {
            enemyHUDList[i].SetStats(enemyUnitList[i].hp, enemyUnitList[i].maxHp, enemyUnitList[i].armor);
        }
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
    public void OnItemActivationButton()
    {

        if (state != BattleState.PLAYERTURN)
            return;
        bool noActionPointsLeft = itemPowers.ActivateItemPower(deck.item);
        currentCard = deck.GetTopCard();
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


    IEnumerator EnemyTurn()
    {
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
                isDead = playerUnit.TakeDamage(enemyUnitList[i].combatAi.doSomething(playerUnit, turn, i, enemyUnitList[i].hp, dialogueText));
                yield return new WaitForSeconds(1f);
            }

        }




        playerHUD.SetStats(playerUnit.hp, playerUnit.maxHp, playerUnit.armor);
        for (int i = 0; i < enemyUnitList.Count; i++)
        {
            enemyHUDList[i].SetStats(enemyUnitList[i].hp, enemyUnitList[i].maxHp, enemyUnitList[i].armor);
        }


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
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won";

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
    public bool GetUnitsLength()
    {
        return enemyUnitList.Count == 3;
    }

}

