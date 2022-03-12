using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbilitySet 
{
    protected Deck deck;
    protected DeckBuilder deckbuilder;
    public AbstractAbilitySet()
    {
        deck = Deck.Instance;
        deckbuilder = DeckBuilder.Instance;
    }
    public bool PlayAbility(Card card)
    {

        if (!card.hasAbility)
            return false;
        switch (card.rank)
        {
            case Rank.KING:
                return KingAbility(card);

            case Rank.QUEEN:
                return QueenAbility(card);

            case Rank.JACK:
                return JackAbility(card);

            case Rank.JOKER:
                return JokerAbility(card);

            default:
                return false;

        }
    }

    public abstract bool KingAbility(Card card);
    public abstract bool QueenAbility(Card card);
    public abstract bool JackAbility(Card card);
    public abstract bool JokerAbility(Card card);

}
