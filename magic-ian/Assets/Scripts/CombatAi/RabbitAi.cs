using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RabbitAi : CombatAi
{
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz� tur�, drugi ma drug� tur� itd...
        if (turn % 4 == 1)
        {
            dialogueText.text = thisUnit.unitName + " seems cute";
            playerUnit.armor = 0;
        }
        if (turn % 4 == 2)
        {
            dialogueText.text = thisUnit.unitName + " attacks";
            animator.SetTrigger("Attacking");
            return thisUnit.damage;
        }
        if (turn % 4 == 3)
        {
            dialogueText.text = thisUnit.unitName + " stares at you";
        }
        if (turn % 4 == 0)
        {
            dialogueText.text = thisUnit.unitName + " attacks with ferocity";//double attack
            animator.SetTrigger("Attacking");
            return (thisUnit.damage * 2);
        }
        return 0;
    }
}