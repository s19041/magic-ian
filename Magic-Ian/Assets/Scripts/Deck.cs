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

    public CardDisplay cardDisplay;
    //my�li na temat tej klasy:
    //mo�e by rozdzielic to na graveyard i deck ale troch� useless
    //fajnie by zrobi� jakie� przekl�danie graveyard �eby sprawdzi� jakie karty si� zagra�o
    //nawet bardzo fajnie /\
    private void Start()
    {
        graveyard = new List<Card>();
    }
    public void addCard(Card card)
    {
        if (cards.Count < 12)
        {
            card.setCard();
            cards.Add(card);
        }
    }
    public bool removeCard(Card card)
    {
        return cards.Remove(card);
    }
    public bool setTopCard()
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
        setTopCard();
    }

    public void reShuffle()
    {
        cards = graveyard;
        graveyard = new List<Card>();//z tego b�dzie animacja powrotu z graveyard do decku
        Shuffle();
    }
    public Card getTopCard()
    {
        return topCard;

    }
    public void throwCard()
    {
        cards.Remove(topCard);
        graveyard.Add(topCard);
    }
    public void nextCard()
    {
        var isEmpty = setTopCard();
        if (isEmpty)
        {
            reShuffle();
            setTopCard();
        }
    }
    public void cardPlayed()
    {
        throwCard();
        nextCard();
    }

    public List<Card> getCards()
    {
        return cards;
    }

}
