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
    List<Card> graveyard;

    public Card topCard;

    public Item item;

    public CardDisplay cardDisplay;

    static Deck instance;
    //my�li na temat tej klasy:
    //mo�e by rozdzielic to na graveyard i deck ale troch� useless
    //fajnie by zrobi� jakie� przekl�danie graveyard �eby sprawdzi� jakie karty si� zagra�o
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

    }

    private void Start()
    {
        cardDisplay = new CardDisplay();
        graveyard = new List<Card>();

        foreach (Card card in cards)
        {
            card.setCard();
        }
    }
    public void SetDeckForCombat(CardDisplay cd)
    {
        cardDisplay = cd;
        cardDisplay.setItemButtonText(item);
        Shuffle();
        SetTopCard();
    }
    public bool AddCard(Card card)
    {
        if (cards.Count < 12)
        {
            card.setCard();
            cards.Add(card);
            return true;
        }
        return false;
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
            cardDisplay.updateDisplay();// nie jestem pewien czy display powinien tutaj by� ale chyba lepiej tutaj niz w battle system(nawet napewno lepiej)
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
        graveyard = new List<Card>();//z tego b�dzie animacja powrotu z graveyard do decku
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
}
