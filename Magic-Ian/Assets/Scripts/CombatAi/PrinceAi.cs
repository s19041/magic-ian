using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrinceAi : CombatAi
{

    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        if (turn % 4 == 1)
        {
            dialogueText.text = thisUnit.unitName + " slaps you with his tongue";
            return thisUnit.damage;
        }
        if (turn % 4 == 2)
        {
            dialogueText.text = thisUnit.unitName + " licks your card";
            Deck.Instance.CardPlayed();
        }
        if (turn % 4 == 3)
        {
            dialogueText.text = thisUnit.unitName + " slaps you with his tongue";
            return thisUnit.damage;
        }
        if (turn % 4 == 0)
        {
            dialogueText.text = thisUnit.unitName + " catches a fly";
            thisUnit.Heal(10);
        }
        return 0;
    }


}
