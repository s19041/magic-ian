using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RabbitAi : CombatAi
{

    public override int doSomething(Unit playerUnit, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        if (turn % 4 == 1)
        {
            dialogueText.text = thisUnit.unitName + " seems cute";
            playerUnit.armor = 0;
        }
        if (turn % 4 == 2)
        {
            dialogueText.text = thisUnit.unitName + " attacks";
            return thisUnit.damage;
        }
        if (turn % 4 == 3)
        {
            dialogueText.text = thisUnit.unitName + " stares at you";
        }
        if (turn % 4 == 0)
        {
            dialogueText.text = thisUnit.unitName + " attacks with ferocity";//double attack
            return (thisUnit.damage * 2);
            

        }
        return 0;
    }


}
