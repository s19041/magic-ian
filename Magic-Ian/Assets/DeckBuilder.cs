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
            deck.addCard(colors[1][i]);
            deck.addCard(colors[2][i]);
        }
        deck.Shuffle();
        deck.setTopCard();
        
        
    }


    
}
