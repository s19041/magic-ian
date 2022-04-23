using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssistantAi : CombatAi
{

    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        if (turn % 5 == 1)
        {
            dialogueText.text = thisUnit.unitName + " blames you for her problems";
            playerUnit.AddStunStacks(8);
        }
        if (turn % 5 == 2)
        {
            dialogueText.text = thisUnit.unitName + " throws a stage prop at you";
            playerUnit.AddStunStacks(5);
            return thisUnit.damage;
        }
        if (turn % 5 == 3)
        {
            dialogueText.text = thisUnit.unitName + " powers up";
            thisUnit.damage += (int)(thisUnit.damage / 2);
        }
        if (turn % 5 == 4)
        {
            dialogueText.text = thisUnit.unitName + " has a mental breakdown";

        }
        if (turn % 5 == 0)
        {
            dialogueText.text = thisUnit.unitName + " talks to herself";//double attack
            thisUnit.ArmorUp(7);
            

        }
        return 0;
    }


}
