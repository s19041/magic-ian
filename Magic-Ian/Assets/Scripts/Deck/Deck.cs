using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    List<Card> cards;
    [SerializeField]
    Suit[] deckSuits;
    [SerializeField]
    List<Card> cardsBackup;
    [SerializeField]
    List<Card> graveyard;

    public Card topCard;


    public Card lastCard;

    public Item item;

    public CardDisplay cardDisplay;

    static Deck instance;
    //myœli na temat tej klasy:
    //mo¿e by rozdzielic to na graveyard i deck ale trochê useless
    //fajnie by zrobiæ jakieœ przekl¹danie graveyard ¿eby sprawdziæ jakie karty siê zagra³o
    //nawet bardzo fajnie /\
    private static Deck _instance;

    public static Deck Instance { get { return _instance; } }
    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
        deckSuits = new Suit[2];

    }

    private void Start()
    {
        
        graveyard = new List<Card>();
        cardsBackup = new List<Card>();
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i] = Instantiate(cards[i]);//kopiowanie scriptable object ¿eby na nich nie pracowaæ a na ich obiektach
            cards[i].SetCard();
        }
    }
    public void SetDeckForCombat(CardDisplay cd)
    {
        lastCard = null;
        cardsBackup = new List<Card>();
        cardsBackup.AddRange(cards);
        cardDisplay = cd;
        cardDisplay.setItemButtonText(item);
        Shuffle();
        SetTopCard();
    }
    public void AddCard(Card card)
    {
        if (card.suit != Suit.SPECIAL)
        {
            if (deckSuits[0] == 0)
            {
                deckSuits[0] = card.suit;
            }
            else
            {
                if(deckSuits[1] == 0)
                    deckSuits[1] = card.suit;
            }
            
        }        
        cards.Add(card);
    }
    public bool RemoveCard(Card card)
    {
        return cards.Remove(card);
    }
    public bool SetTopCard()
    {
        if (cards.Count > 0)
        {
            topCard = cards[0];
            cardDisplay.card = topCard;
            cardDisplay.updateDisplay();// nie jestem pewien czy display powinien tutaj byæ ale chyba lepiej tutaj niz w battle system(nawet napewno lepiej)
            return false;
        }
        return true;

    }
    public void Shuffle()
    {
        var shuffledcards = cards.OrderBy(a => Guid.NewGuid()).ToList();
        cards = shuffledcards;
        SetTopCard();
    }

    public void ReShuffle()
    {
        cards = graveyard;
        graveyard = new List<Card>();//z tego bêdzie animacja powrotu z graveyard do decku
        Shuffle();
    }
    public Card GetTopCard()
    {
        return topCard;

    }
    public void ThrowCard()
    {
        cards.Remove(topCard);
        graveyard.Add(topCard);
        lastCard = topCard;
    }
    public void NextCard()
    {
        var isEmpty = SetTopCard();
        if (isEmpty)
        {
            ReShuffle();
            SetTopCard();
        }
    }
    public void CardPlayed()
    {
        ThrowCard();
        NextCard();

    }

    public List<Card> GetCards()
    {

        return cards;
    }
    public bool CardInDeck(Card card)
    {
        return cards.Contains(card);
    }

    public List<Card> GetGraveyard()
    {
        return graveyard;
    }
    public void AddCardsFromJack(List<Card> jackList)
    {
        for (int i = 0; i < jackList.Count; i++)
        {
            cards[i] = jackList[i];
        }
    }
    public void Reset()
    {
        cards = cardsBackup;
    }
    public void ChangeCardAt(int index,Card _card)
    {
        cards[index] = _card;
        if(index==0)
            SetTopCard();
    }
    public void SetCardAoe(int index)
    {
        if(cards.Count>index)
            cards[index].aoe = true;
    }
    public void AddStatOfSuit(Suit suit, int power, int cardIndex)
    {
        if (suit == Suit.HEARTS)
        {
            cards[cardIndex].heal += (int)(power / 2);
        }
        if (suit == Suit.CLUBS)
        {
            cards[cardIndex].armor += power;
        }
        if (suit == Suit.SPADES)
        {
            cards[cardIndex].damage += (int)(power / 2);
        }
        if (suit == Suit.DIAMONDS)
        {
            cards[cardIndex].stunStacks = power;
        }
    }
    public bool HasOpenSuit()
    {
        if (deckSuits[0] == 0)
            return true;
        if (deckSuits[1] == 0)
            return true;
        return false;
    }
}
