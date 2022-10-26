using System.Collections.Generic;
using TMPro;

public class ActivistAI : CombatAi
{
    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        int allHp = 0;
        for (int i = 0; i < opponentUnits.Count; i++)
        {
            if (i != indexPosition)
            {
                allHp += opponentUnits[i].hp;
            }
        }
        if (allHp <= 0)
        {
            dialogueText.text = thisUnit.unitName + " : YOU SAVAGE";
            return 10;
        }
        if (turn % 4 == 1)
        {
            dialogueText.text = thisUnit.unitName + " : Go get him";
            for (int i = 0; i < opponentUnits.Count; i++)
            {
                if (i != indexPosition)
                {
                    opponentUnits[i].DamageUp(1);
                }
            }
        }
        if (turn % 4 == 2)
        {
            dialogueText.text = thisUnit.unitName + " : How can you do it to them?";
            playerUnit.AddStunStacks(5);
        }
        if (turn % 3 == 0)
        {
            dialogueText.text = thisUnit.unitName + " : My sweet little cupcakes";
            for (int i = 0; i < opponentUnits.Count; i++)
            {
                if (i != indexPosition)
                {
                    opponentUnits[i].ArmorUp(3);
                }
            }
        }
        return 0;
    }
}