using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    public Deck deck;
    public List<Card> allCards;

    // Start is called before the first frame update
    void Start()
    {//ta metoda wymaga przerobienia bo to placeholder do sprawdzania czy dzia³a
        

        foreach (Card card in allCards)
        {
            card.setDamage();
            card.setStats();
        }

        for (int i = 0; i <= 5; i++)
        {
            deck.addCard(allCards[i]);
            deck.addCard(allCards[i]);
        }
        deck.Shuffle();
        
        
    }


    
}
