using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PigeonAi : CombatAi
{
    public Animator animator;


    public void Start()
    {
        animator = GetComponent<Animator>();
    }





    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        if (turn % 3 == 1)
        {
            dialogueText.text = thisUnit.unitName + " attacks";
            animator.SetTrigger("Attacking");
            return thisUnit.damage;
        }
        if (turn % 3 == 2)
        {
            dialogueText.text = thisUnit.unitName + " defends";
            thisUnit.ArmorUp(6);
        }
        if (turn % 3 == 0)
        {
            dialogueText.text = thisUnit.unitName + " powers up";
            thisUnit.DamageUp(3);
        }
        return 0;
    }


}
