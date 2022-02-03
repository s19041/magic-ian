using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    //public CardDisplay cardDisplay;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public CardDisplay combatCardDisplay;

    public TextMeshProUGUI dialogueText;
    Unit playerUnit;
    Unit enemyUnit;

    public Deck deck;
    Card currentCard;


    AbstractAbilitySet abilitySet;

    private int turn;
    private ItemPowers itemPowers;

    // Start is called before the first frame update
    void Start()
    {

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        playerUnit.unitName = "Ian";
        enemyUnit.unitName = "Incensed Pigeon";

        playerUnit.armor = 0;
        enemyUnit.armor = 0;

        dialogueText.text = enemyUnit.unitName + " approaches";

        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);

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
        // tu powinna by� ca�a logika z dzia�aniem kart. 
        //nie wiem jak rozwi�za� spraw� kart specjalnych. Pewnie sprawdzanie ich ifami i tutaj dzia�anie wpisywa� ale to troch� s�abe(ale �atwe)




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
    public void OnItemActivationButton()//tutaj zrobi� 
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
        yield return new WaitForSeconds(1f);//Poni�ej legendarne GOLOMP AI
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

