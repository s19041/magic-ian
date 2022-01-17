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
    
    private void Start()
    {
        graveyard = new List<Card>();
    }
    public void addCard(Card card)
    {
        if (cards.Count < 12)
        {
            cards.Add(card);
        }
    }
    public bool removeCard(Card card)
    {
        return cards.Remove(card);
    }
    public bool showCard()
    {
        if (cards.Count > 0)
        {
            topCard = cards[0];
            return false;
        }
        return true;
            
    }
    public void Shuffle()
    {
        var shuffledcards = cards.OrderBy(a => Guid.NewGuid()).ToList();
        cards = shuffledcards;
    }

    public void reShuffle()
    {
        cards = graveyard;
        graveyard = new List<Card>();
        Shuffle();
    }
    public Card playCard()
    {
        
        //logic for playing the topcard
        graveyard.Add(topCard);
        cards.Remove(topCard);
        var isEmpty=showCard();
        Card c = topCard;
        if (isEmpty)
        {
            reShuffle();
            showCard();
            c = topCard;
        }
        return c;
        
    }
    public List<Card> getCards()
    {
        return cards;
    }
   
}
