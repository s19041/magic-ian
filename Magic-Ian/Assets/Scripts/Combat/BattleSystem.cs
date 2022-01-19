using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST };
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public CardDisplay cardDisplay;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;
    Unit playerUnit;
    Unit enemyUnit;

    public Deck deck;
    Card currentCard;

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
        enemyUnit.unitName = "Rozsierdzony Golomp";

        playerUnit.armor = 0;
        enemyUnit.armor = 0;

        dialogueText.text = enemyUnit.unitName+" przybyl";

        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);

        
        cardDisplay.updateDisplay();


        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    void PlayerTurn()
    {
        dialogueText.text = "Twoj ruch";
        currentCard = deck.playCard();
        cardDisplay.card = currentCard;
        cardDisplay.updateDisplay();
    }
    
    IEnumerator PlayerAttack()
    {
        // tu powinna byæ ca³a logika z dzia³aniem kart. 
        // pobieranie wartosci dmg z karty itp zamiast z playerUnit. Tak samo z np healowaniem z kart(kier) wiec sama metoda to raczej PlayerPlayCard powinna byc
        
        
        


        playerUnit.Heal(currentCard.heal);
        playerUnit.ArmorUp(currentCard.armor);
        enemyUnit.AddStunStacks(currentCard.stunStacks);
        bool isDead = enemyUnit.TakeDamage(currentCard.damage);


        //if kier to cos
        //if trefl to cos/
        //blablabla
        playerHUD.SetStats(playerUnit.hp,playerUnit.maxHp,playerUnit.armor);
        enemyHUD.SetStats(enemyUnit.hp,enemyUnit.maxHp, enemyUnit.armor);
        

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
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
       
    }
    IEnumerator EnemyTurn()
    {
        dialogueText.text = "Ruch " + enemyUnit.unitName;
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetStats(playerUnit.hp,playerUnit.maxHp,playerUnit.armor);

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
            dialogueText.text = "Wygrales";

        }else if(state == BattleState.LOST){
            dialogueText.text = "Przegrales";
        }
    }
}
