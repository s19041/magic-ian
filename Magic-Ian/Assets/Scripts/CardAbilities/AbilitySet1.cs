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

        if (count > deck.GetCards().Count - 1)//-1 poniewa� .Count zwr�ci razem z g�rn� kart� a jej nie jestesmy w stanie przestasowa�
            count = deck.GetCards().Count - 1;//dziwna logika ale dzia�a
        deckbuilder.jackListCount = count;


        deckbuilder.jackList = new List<Card>();


        deckbuilder.deckDisplay.ShowJackBuilder(count);

    }

    public override void JokerAbility(Card card)//zamienia si� w poprzedni� kart�
    {
        throw new System.NotImplementedException();
    }

    public override void KingAbility(Card card)// nast�pna karta b�dzie aoe. Robi to co 5 danego koloru ale bez dmg
    {
        throw new System.NotImplementedException();
    }

    public override void QueenAbility(Card card) // Daje do x kart buff swojego koloru (np dama serce daje ka�dej karcie dodatkowo efekt serca00)
    {
        throw new System.NotImplementedException();
    }
}
