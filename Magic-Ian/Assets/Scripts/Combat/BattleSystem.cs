using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST };
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

    public TextMeshProUGUI dialogueText;
    Unit playerUnit;
    Unit enemyUnit;

    public Deck deck;
    Card currentCard;


    AbstractAbilitySet abilitySet;

    private int turn;
    private int shuffleCount;

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

        dialogueText.text = enemyUnit.unitName+" approaches";

        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);

        FindObjectOfType<Deck>().setDisplay();
        FindObjectOfType<CardDisplay>().updateDisplay();


        abilitySet = new AbilitySet1(deck, deck.gameObject.GetComponent<DeckBuilder>());//strasznie brzydkie ale jest póŸno

        
        deck.Shuffle();


        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    void PlayerTurn()
    {
        shuffleCount = 0;
        dialogueText.text = "Your turn";
        currentCard = deck.getTopCard();
        //cardDisplay.card = currentCard;
        //cardDisplay.updateDisplay();
        turn++;

    }

    IEnumerator PlayerPlayCard()
    {
        // tu powinna byæ ca³a logika z dzia³aniem kart. 
        //nie wiem jak rozwi¹zaæ sprawê kart specjalnych. Pewnie sprawdzanie ich ifami i tutaj dzia³anie wpisywaæ ale to trochê s³abe(ale ³atwe)



        

        playerUnit.Heal(currentCard.heal);
        playerUnit.ArmorUp(currentCard.armor);
        enemyUnit.AddStunStacks(currentCard.stunStacks);
        if (currentCard.hasAbility)
        {
            Debug.Log("eldoka");
            abilitySet.playAbility(currentCard);
            Time.timeScale = 0;
        }
        deck.cardPlayed();

        bool isDead = enemyUnit.TakeDamage(currentCard.damage);
        //deck.cardPlayed();



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
    public void OnPlayCardButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerPlayCard());

    }
    public void OnItemActivationButton()//tutaj zrobiæ 
    {
        if (deck.item.itemName == ItemName.CYLINDER)
        {
            ShuffleDeck();
        }
        if (deck.item.itemName == ItemName.CAPE)
        {

        }
        if (deck.item.itemName == ItemName.MONOCLE)
        {

        }
        if (deck.item.itemName == ItemName.SLEEVE)
        {

        }
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
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won";

        }else if(state == BattleState.LOST){
            dialogueText.text = "You lost";
        }
    }
    // BELOW - ALL ITEM POWERS
    public void ShuffleDeck()//przypisz to do przycisku kaju
    {
        if (state != BattleState.PLAYERTURN)
            return;
        shuffleCount++;
        deck.Shuffle();

        //cardDisplay.updateDisplay();
        if (shuffleCount >= 2)
            StartCoroutine(EnemyTurn());
    }
}

