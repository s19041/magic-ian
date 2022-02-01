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

    private static DeckBuilder _instance;

    public static DeckBuilder Instance { get { return _instance; } }
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
        deckDisplay = DeckDisplay.Instance;
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {//ta metoda wymaga przerobienia bo to placeholder do sprawdzania czy dzia³a

        colors = new List<List<Card>>();
        colors.Add(hearts);
        colors.Add(spades);
        colors.Add(clubs);
        colors.Add(diamonds);

        if (deckDisplay == null)
        {
            deckDisplay = GameObject.FindObjectOfType<DeckDisplay>();
        }
        avalibleCards.AddRange(hearts);
        avalibleCards.AddRange(spades);
        avalibleCards.AddRange(diamonds);
        avalibleCards.AddRange(clubs);
        //deck.addCard(avalibleCards[0]);
        //deck.addCard(avalibleCards[1]);
        //deck.addCard(avalibleCards[2]);





    }

    public bool addCardToDeck(Card card)
    {
        if (deck.AddCard(card))
            return true;
        return false;

    }
    public void addCardToAvalible(Card card)
    {
        avalibleCards.Add(card);
    }
    public void addCardsFromJack()
    {
        deck.AddCardsFromJack(jackList);
        jackList.Clear();

    }





}
