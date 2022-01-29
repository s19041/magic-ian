using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbilitySet : MonoBehaviour
{
    protected Deck deck;
    protected DeckDisplay deckDisplay;
    public AbstractAbilitySet(Deck _deck, DeckDisplay _deckDisplay)
    {
        deck = _deck;
        deckDisplay = _deckDisplay;
    }
    public void playAbility(Card card)
    {
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
