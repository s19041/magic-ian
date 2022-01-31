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

    public Item item;

    public CardDisplay cardDisplay;

    static Deck instance;
    //myœli na temat tej klasy:
    //mo¿e by rozdzielic to na graveyard i deck ale trochê useless
    //fajnie by zrobiæ jakieœ przekl¹danie graveyard ¿eby sprawdziæ jakie karty siê zagra³o
    //nawet bardzo fajnie /\
    private void Start()
    {
        cardDisplay = FindObjectOfType<CardDisplay>();

        cardDisplay.setItemButtonText(item);
        graveyard = new List<Card>();

        foreach (Card card in cards){
            card.setCard();
        }
    }
    public bool addCard(Card card)
    {
        if (cards.Count < 12)
        {
            card.setCard();
            cards.Add(card);
            return true;
        }
        return false;
    }
    public bool removeCard(Card card)
    {
        return cards.Remove(card);
    }
    public bool setTopCard()
    {
        if (cards.Count > 0)
        {
            topCard = cards[0];
            cardDisplay.card = topCard;
            cardDisplay.updateDisplay();// nie jestem pewien czy display powinien tutaj byæ ale chyba lepiej tutaj niz w battle system(nawet napewno lepiej)
            return false;
        }
        return true;

    }
    public void Shuffle()
    {
        var shuffledcards = cards.OrderBy(a => Guid.NewGuid()).ToList();
        cards = shuffledcards;
        setTopCard();
    }

    public void reShuffle()
    {
        cards = graveyard;
        graveyard = new List<Card>();//z tego bêdzie animacja powrotu z graveyard do decku
        Shuffle();
    }
    public Card getTopCard()
    {
        return topCard;

    }
    public void throwCard()
    {
        cards.Remove(topCard);
        graveyard.Add(topCard);
    }
    public void nextCard()
    {
        var isEmpty = setTopCard();
        if (isEmpty)
        {
            reShuffle();
            setTopCard();
        }
    }
    public void cardPlayed()
    {
        throwCard();
        nextCard();
        
    }

    public List<Card> getCards()
    {

        return cards;
    }
    public bool cardInDeck(Card card)
    {
        return cards.Contains(card);
    }

    private void Awake()
    {
        //int counter = FindObjectsOfType(GetType()).Length;

        if(instance!=null)//if (counter > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void setDisplay()
    {
        cardDisplay = FindObjectOfType<CardDisplay>();
    }
    public List<Card> getGraveyard()
    {
        return graveyard;
    }
    public void addCardsFromJack(List<Card> jackList)
    {
        for(int i = 0; i < jackList.Count; i++)
        {
            cards[i] = jackList[i];
        }
    }
}
