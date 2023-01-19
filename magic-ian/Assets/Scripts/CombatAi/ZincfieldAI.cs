using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZincfieldAI : CombatAi
{
    public Animator animator;
    private Card card;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        if (turn % 5 == 1)
        {
            dialogueText.text = thisUnit.unitName + " :Pick a card";
            card = Deck.Instance.topCard;
            Deck.Instance.CardPlayed();
        }
        if (turn % 5 == 2)
        {
            dialogueText.text = thisUnit.unitName + " is this your card?";
            animator.SetTrigger("Attacking");
            if (card.stunStacks != 0)
            {
                playerUnit.AddStunStacks(card.stunStacks);
            }

            if (card.heal != 0)
            {
                thisUnit.Heal(card.heal);
            }
            if (card.armor != 0)
            {
                thisUnit.ArmorUp(card.armor);
            }
            return card.damage;
        }
        if (turn % 5 == 3)
        {
            dialogueText.text = thisUnit.unitName + " vanishes";// tutaj animacja znikania
            thisUnit.AddBarriers(2);
        }
        if (turn % 5 == 4)
        {
            dialogueText.text = thisUnit.unitName + " :Now i will vanish you!";//double attack
            animator.SetTrigger("Attacking");
            return (thisUnit.damage);
        }
        if (turn % 5 == 0)
        {
            dialogueText.text = thisUnit.unitName + " starts levitating!";//double attack
            thisUnit.Heal(5);
            thisUnit.ArmorUp(5);
        }
        return 0;
    }
}