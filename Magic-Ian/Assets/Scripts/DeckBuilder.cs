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



    // Start is called before the first frame update
    void Start()
    {//ta metoda wymaga przerobienia bo to placeholder do sprawdzania czy dzia³a
        colors = new List<List<Card>>();
        colors.Add(hearts);
        colors.Add(spades);
        colors.Add(clubs);
        colors.Add(diamonds);

        for (int i = 0; i <= 5; i++)
        {
            avalibleCards.Add(colors[1][i]);
            avalibleCards.Add(colors[2][i]);
        }
        deck.Shuffle();
        deck.setTopCard();

        
    }
    public void addCardToDeck(Card card)
    {
        deck.addCard(card);
    }
    public void addCardToAvalible(Card card)
    {
        avalibleCards.Add(card);
    }
    public void showDeckBuilder()
    {
        deckDisplay.clearDeckBuilder();
        if (deckDisplay.gameObject.active)
        {
            deckDisplay.gameObject.SetActive(false);
        }
        else
        {
            deckDisplay.gameObject.SetActive(true);
            deckDisplay.showDeckBuilder(avalibleCards, true);


        }
       
    }
    public void showDeck()
    {
        deckDisplay.clearDeckBuilder();
        if (deckDisplay.gameObject.active)
        {
            deckDisplay.gameObject.SetActive(false);
        }
        else
        {
            deckDisplay.gameObject.SetActive(true);
            deckDisplay.showDeckBuilder(deck.getCards(), false);


        }
        
       
    }
   



}
