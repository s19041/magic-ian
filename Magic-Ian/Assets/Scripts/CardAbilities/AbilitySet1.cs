using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySet1 : AbstractAbilitySet
{
    public AbilitySet1(Deck _deck, DeckBuilder _deckBuilder) : base(_deck)
    {

    }

    public override void JackAbility(Card card)
    {
        int count = 3;

        if (count > deck.GetCards().Count - 1)//-1 poniewa¿ .Count zwróci razem z górn¹ kart¹ a jej nie jestesmy w stanie przestasowaæ
            count = deck.GetCards().Count - 1;
        deckbuilder.jackListCount = count;


        deckbuilder.jackList = new List<Card>();


        deckbuilder.deckDisplay.ShowJackBuilder(count);

    }

    public override void JokerAbility(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override void KingAbility(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override void QueenAbility(Card card)
    {
        throw new System.NotImplementedException();
    }
}
