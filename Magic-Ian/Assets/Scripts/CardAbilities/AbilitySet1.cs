using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySet1 : AbstractAbilitySet
{
    public AbilitySet1(Deck _deck, DeckBuilder _deckBuilder) : base(_deck)
    {

    }

    public override void JackAbility(Card card)//
    {
        int count = 3;

        if (count > deck.GetCards().Count - 1)//-1 poniewa¿ .Count zwróci razem z górn¹ kart¹ a jej nie jestesmy w stanie przestasowaæ
            count = deck.GetCards().Count - 1;//dziwna logika ale dzia³a
        deckbuilder.jackListCount = count;


        deckbuilder.jackList = new List<Card>();


        deckbuilder.deckDisplay.ShowJackBuilder(count);

    }

    public override void JokerAbility(Card card)//zamienia siê w poprzedni¹ kartê
    {
        throw new System.NotImplementedException();
    }

    public override void KingAbility(Card card)// nastêpna karta bêdzie aoe. Robi to co 5 danego koloru ale bez dmg
    {
        throw new System.NotImplementedException();
    }

    public override void QueenAbility(Card card) // Daje do x kart buff swojego koloru (np dama serce daje ka¿dej karcie dodatkowo efekt serca00)
    {
        throw new System.NotImplementedException();
    }
}
