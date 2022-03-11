using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbilitySet 
{
    protected Deck Deck;
    protected DeckBuilder deckbuilder;
    public AbstractAbilitySet()
    {
        Deck = Deck.Instance;
        deckbuilder = DeckBuilder.Instance;
    }
    public void PlayAbility(Card card)
    {
        Time.timeScale = 0;//NAD TYM SIÊ ZASTANOWIÆ BO TO CHYBA ŒREDNIE
        if (!card.hasAbility)
            return;
        switch (card.rank)
        {
            case Rank.KING:

                KingAbility(card);
                break;

            case Rank.QUEEN:

                QueenAbility(card);
                break;

            case Rank.JACK:
                JackAbility(card);
                break;

            case Rank.JOKER:
                JokerAbility(card);
                break;

            default:
                break;

        }
    }

    public abstract void KingAbility(Card card);
    public abstract void QueenAbility(Card card);
    public abstract void JackAbility(Card card);
    public abstract void JokerAbility(Card card);

}
