using System.Collections.Generic;

public abstract class AbstractAbilitySet
{
    protected Deck deck;
    protected DeckBuilder deckbuilder;
    protected List<Card> cards;
    public int kingCards;
    public int jokerCards;
    public bool barierQueen;
    public int jackCards;

    public AbstractAbilitySet()
    {
        deck = Deck.Instance;
        deckbuilder = DeckBuilder.Instance;
        cards = deck.GetCards();
        kingCards = 2;//ilo�� kart na jakie dzia�a kr�l
        kingCards = 1;//ilo�� kart na jakie dzia�a joker
        jackCards = 3;//ilo�� kart jakie pokazuje walet
        barierQueen = false;//czy kr�lowa daje bariere
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