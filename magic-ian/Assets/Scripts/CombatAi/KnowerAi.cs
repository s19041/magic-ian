using System.Collections.Generic;
using TMPro;

public class KnowerAi : CombatAi
{
    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        if (Deck.Instance.lastCard.hasAbility)//jak zagrasz specjaln¹ kartê to jest przypa³ i siê bufuje mocno
        {
            dialogueText.text = thisUnit.unitName + " : I know how you did that!";
            thisUnit.ArmorUp(6);
            thisUnit.DamageUp(4);
        }
        if (turn % 3 == 1)
        {
            dialogueText.text = thisUnit.unitName + " mocks you";
            thisUnit.AddBarriers(1);
        }
        if (turn % 3 == 2)
        {
            dialogueText.text = thisUnit.unitName + " attacks";
            return thisUnit.damage;
        }
        if (turn % 3 == 0)
        {
            dialogueText.text = thisUnit.unitName + " rants about how magic is for children";
            playerUnit.AddStunStacks(8);
        }

        return 0;
    }
}