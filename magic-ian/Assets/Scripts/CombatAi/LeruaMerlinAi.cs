using System.Collections.Generic;
using TMPro;

public class LeruaMerlinAi : CombatAi
{
    public override int doSomething(Unit playerUnit, List<Unit> opponentUnits, int turn, int indexPosition, int unitHp, TextMeshProUGUI dialogueText)
    {
        //turn += indexPosition;//pierwszy oponent ma pierwsz¹ turê, drugi ma drug¹ turê itd...
        if (turn % 5 == 1)
        {
            dialogueText.text = thisUnit.unitName + ": That's not the right screw";
            playerUnit.AddStunStacks(3);
            return thisUnit.damage;
        }
        if (turn % 5 == 2)
        {
            dialogueText.text = thisUnit.unitName + " Pass me the handdrill";
            thisUnit.DamageUp(3);
        }
        if (turn % 5 == 2)
        {
            dialogueText.text = thisUnit.unitName + ": I found the right screw";
            thisUnit.ArmorUp(5);
            thisUnit.Heal(5);
        }
        if (turn % 5 == 3)
        {
            dialogueText.text = thisUnit.unitName + ": You hold the vacuum";
            playerUnit.AddStunStacks(5);
        }
        if (turn % 5 == 4)
        {
            dialogueText.text = thisUnit.unitName + ": Let me do it myself";
            Card card = Deck.Instance.topCard;
            thisUnit.armor += card.armor;
            return card.damage;
        }
        if (turn % 5 == 0)
        {
            dialogueText.text = thisUnit.unitName + ": Deck makeover!!!";
            Deck.Instance.Shuffle();
        }
        return 0;
    }
}