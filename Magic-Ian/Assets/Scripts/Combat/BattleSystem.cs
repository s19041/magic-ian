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
        List<GameObject> enemyGOs=new List<GameObject>();
        for(int i = 0; i < enemyPrefabs.Count; i++)
        {
            
            enemyGOs.Add(Instantiate(enemyPrefabs[i], enemyBattleStationList[i]));
            
        }
        enemyUnitList = new List<Unit>();
        foreach(GameObject enemyGO in enemyGOs)
        {
            
            enemyUnitList.Add(enemyGO.GetComponent<Unit>());
            

        }



        dialogueText.text = currentRoom.encounterName + " encountered";//fajnie by by³a jak¹œ klase machnaæ na czêœæ ui BattleSystem


        playerHUD.SetHud(playerUnit);

        for (int i = 0; i < enemyUnitList.Count; i++)
        {
            enemyHUDList[i].SetHud(enemyUnitList[i]);
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


        bool checkNext = abilitySet.PlayAbility(currentCard);
        Debug.Log("Ability casted");
        deck.SetTopCard();
        currentCard = deck.GetTopCard();
        while (checkNext)
        {
            checkNext=abilitySet.PlayAbility(currentCard);
            Debug.Log("Ability casted");
            deck.SetTopCard();
            currentCard = deck.GetTopCard();
            //zagrywanie karty
        }
        playerUnit.Heal(currentCard.heal);
        playerUnit.ArmorUp(currentCard.armor);


        deck.CardPlayed();

        Debug.Log("Card played");
        //
        
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
        Debug.Log("Damage done");






        playerHUD.SetStats(playerUnit.hp, playerUnit.maxHp, playerUnit.armor);
        for(int i = 0; i < enemyUnitList.Count; i++)
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

        Debug.Log("Not working");
    }
    public void OnItemActivationButton()//tutaj zrobiæ 
    {
        
        if (state != BattleState.PLAYERTURN)
            return;
        bool noActionPointsLeft = itemPowers.ActivateItemPower(deck.item);
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
        for (int i = enemyUnitList.Count-1; i >=0; i--)
        {
            if (enemyUnitList[i].hp > 0)
            {
                dialogueText.text = enemyUnitList[i].unitName+ " move";
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

    public bool GetUnitsLength()
    {
       return (enemyUnitList.Count == 3) ;
    }
 
}

