using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LarryGroubiniAi : CombatAi
{
    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        
        //turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        if(thisUnit.hp<10)
        {
            dialogueText.text = thisUnit.unitName + ": Im starting to get hungry";
            thisUnit.DamageUp(3);
            thisUnit.Heal(30);
            return 0;
        }
        if (turn % 4 == 1)
        {
            dialogueText.text = thisUnit.unitName + " burps";
            playerUnit.AddStunStacks(2);
            return (int)thisUnit.damage/3 ;
        }
        if (turn % 4 == 2)
        {
            dialogueText.text = thisUnit.unitName + " If i were in my prime...";
            thisUnit.Heal(6);
            thisUnit.AddStunStacks(3);
        }
        if (turn % 4 == 3)
        {
            dialogueText.text = thisUnit.unitName + " : If you are so good, escape this!";
            playerUnit.AddStunStacks(20);
            if (playerUnit.armor <= 0)
            {
                return (int)thisUnit.damage/2;
            }
        }
        if (turn % 4 == 0)
        {
            dialogueText.text = thisUnit.unitName + " You are no magician";
            return thisUnit.damage;
        }

        return 0;
    }



}
