using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PigeonAi : CombatAi
{
    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz� tur�, drugi ma drug� tur� itd...
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
            thisUnit.DamageUp(3);
        }
        return 0;
    }


}
