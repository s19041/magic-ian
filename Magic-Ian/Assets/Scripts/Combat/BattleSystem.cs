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
    private List<GameObject> enemyPrefabs;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public CardDisplay combatCardDisplay;

    public TextMeshProUGUI dialogueText;
    Unit playerUnit;
    Unit enemyUnit;
    [SerializeField]
    List<Unit> enemyUnits;

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
        currentRoom = (CombatRoom)DungeonManager.Instance.GetCurrentRoom();

        GameObject playerGO = MainCharacter.Instance.gameObject;
        playerGO.transform.position = playerBattleStation.position;
        playerUnit = playerGO.GetComponent<Unit>();

        enemyPrefabs = new List<GameObject>();
        enemyPrefabs.AddRange(currentRoom.GetOpponents());
        List<GameObject> enemyGOs=new List<GameObject>();
        foreach (GameObject opponent in enemyPrefabs)
        {
            enemyGOs.Add(Instantiate(opponent, enemyBattleStation));
        }
        enemyUnits = new List<Unit>();
        enemyUnits.Add(enemyGOs[0].GetComponent<Unit>());
        enemyUnit = enemyUnits[0];// ¿eby b³êdów nie wywala³o póki work in progress
        //usun¹æ liniê powy¿ej 


        dialogueText.text = enemyUnit.unitName + " approaches";//fajnie by by³a jak¹œ klase machnaæ na czêœæ ui BattleSystem

        playerHUD.SetHud(playerUnit);
        
        enemyHUD.SetHud(enemyUnits[0]);//work in progress

        deck = Deck.Instance;
        deck.cardDisplay = combatCardDisplay;
        combatCardDisplay.updateDisplay();


        abilitySet = new AbilitySet1(deck, DeckBuilder.Instance);

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




        //zagrywanie karty
        playerUnit.Heal(currentCard.heal);
        playerUnit.ArmorUp(currentCard.armor);
        enemyUnit.AddStunStacks(currentCard.stunStacks);
        
        if (currentCard.hasAbility)
        {
            abilitySet.playAbility(currentCard);
        }
        deck.CardPlayed();
        //
        bool isDead = enemyUnit.TakeDamage(currentCard.damage);



        playerHUD.SetStats(playerUnit.hp, playerUnit.maxHp, playerUnit.armor);
        enemyHUD.SetStats(enemyUnit.hp, enemyUnit.maxHp, enemyUnit.armor);


        if (isDead)
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
    public void OnItemActivationButton()//tutaj zrobiæ 
    {
        
        if (state != BattleState.PLAYERTURN)
            return;
        bool noActionPointsLeft = itemPowers.ActivateItemPower(deck.item);
        if(noActionPointsLeft)
            StartCoroutine(EnemyTurn());

    }
    public void OnPlayCardButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerPlayCard());

    }


    IEnumerator EnemyTurn()
    {
        dialogueText.text = "Ruch " + enemyUnit.unitName;
        bool isDead = false;
        yield return new WaitForSeconds(1f);//Poni¿ej legendarne GOLOMP AI
        isDead = playerUnit.TakeDamage(enemyUnit.combatAi.doSomething(playerUnit, turn, dialogueText));


        playerHUD.SetStats(playerUnit.hp, playerUnit.maxHp, playerUnit.armor);
        enemyHUD.SetStats(enemyUnit.hp, enemyUnit.maxHp, enemyUnit.armor);

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
    }
 
}

