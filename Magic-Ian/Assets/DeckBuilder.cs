using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    public Deck deck;
    public List<Card> allCards;
    public Card AceOfHearts;

    // Start is called before the first frame update
    void Start()
    {
        allCards = new List<Card>();
        for(int i = 0; i <= 12; i++)
        {
            deck.addCard(AceOfHearts);
        }
        deck.Shuffle();
    }

    // Update is called once per frame
    
}
