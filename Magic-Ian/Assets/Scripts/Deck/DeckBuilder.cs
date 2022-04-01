using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour// w tej klasie przechowujemy wszystkie karty w grze(te kt�re s� odblokowane znajduj� si� w playerData)
{
    public Deck deck;
    public List<Card> hearts;
    public List<Card> spades;
    public List<Card> clubs;
    public List<Card> diamonds;
    public List<Card> special;
    public List<List<Card>> colors;

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
    {//ta metoda wymaga przerobienia bo to placeholder do sprawdzania czy dzia�a

        colors = new List<List<Card>>();
        colors.Add(hearts);
        colors.Add(spades);
        colors.Add(clubs);
        colors.Add(diamonds);

        if (deckDisplay == null)
        {
            deckDisplay = GameObject.FindObjectOfType<DeckDisplay>();
        }
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

    public void addCardsFromJack()
    {
        deck.AddCardsFromJack(jackList);
        jackList.Clear();

    }





}
