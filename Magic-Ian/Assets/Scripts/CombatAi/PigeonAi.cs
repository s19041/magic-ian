using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PigeonAi : CombatAi
{
    public override int doSomething(Unit playerUnit, int turn, TextMeshProUGUI dialogueText)
    {
        if (turn % 3 == 1)
        {
            dialogueText.text = thisUnit.unitName + " attacks";
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
            thisUnit.damage += 3;
        }
        return 0;
    }


}