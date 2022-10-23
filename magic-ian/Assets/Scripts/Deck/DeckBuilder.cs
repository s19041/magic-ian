using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour// w tej klasie przechowujemy wszystkie karty w grze(te które s¹ odblokowane znajduj¹ siê w playerData)
{
    public Deck deck;
    public List<Card> savedDeckCards;
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

    public List<Item> items;

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
        PlayerManager.Instance.LoadDataXML();
        if (PlayerManager.Instance.GetUnlockedCards().Count < 1)
            PlayerManager.Instance.();
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
    public void ResetDeckAfterRun()
    {
        Deck.Instance.GetCards().Clear();
        Deck.Instance.GetCards().AddRange(savedDeckCards);
        Deck.Instance.PrepareDeck();
    }





}
