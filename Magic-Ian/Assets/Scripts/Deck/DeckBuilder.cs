using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    public Deck deck;
    [SerializeField] List<Card> hearts;
    [SerializeField] List<Card> spades;
    [SerializeField] List<Card> clubs;
    [SerializeField] List<Card> diamonds;
    [SerializeField] List<Card> special;
    [SerializeField] List<List<Card>> colors;
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
        avalibleCards.AddRange(special);
        //deck.addCard(avalibleCards[0]);
        //deck.addCard(avalibleCards[1]);
        //deck.addCard(avalibleCards[2]);





    }

    public bool addCardToDeck(Card card)
    {
        
        if (deck.GetCards().Count < 12 
            && deck.HasSuit(card.suit) 
            &&!deck.CardInDeck(card))//tutaj trzeba zmienic chyba
        {
            deck.AddCard(card);
            return true;
        }
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
