using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    public Deck deck;
    public List<Card> hearts;
    public List<Card> spades;
    public List<Card> clubs;
    public List<Card> diamonds;
    public List<List<Card>> colors;
    public List<Card> avalibleCards;

    public DeckDisplay deckDisplay;

    public List<Card> jackList;
    public int jackListCount;


    // Start is called before the first frame update
    void Start()
    {//ta metoda wymaga przerobienia bo to placeholder do sprawdzania czy dzia�a
        
        colors = new List<List<Card>>();
        colors.Add(hearts);
        colors.Add(spades);
        colors.Add(clubs);
        colors.Add(diamonds);

    
        avalibleCards.AddRange(hearts);
        avalibleCards.AddRange(spades);
        avalibleCards.AddRange(diamonds);
        avalibleCards.AddRange(clubs);
        //deck.addCard(avalibleCards[0]);
        //deck.addCard(avalibleCards[1]);
        //deck.addCard(avalibleCards[2]);

        deck.Shuffle();
        deck.setTopCard();


        
    }
    public bool addCardToDeck(Card card)
    {
        if (deck.addCard(card))
            return true;
        return false;
        
    }
    public void addCardToAvalible(Card card)
    {
        avalibleCards.Add(card);
    }
    public void addCardsFromJack()
    {
        deck.addCardsFromJack(jackList);
        jackList.Clear();
            
    }

   



}