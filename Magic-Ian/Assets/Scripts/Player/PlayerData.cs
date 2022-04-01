using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    // Start is called before the first frame update
    public int gold;
    public int tutorialRuns;
    public int succesfulRuns;
    public int failedRuns;

    [SerializeField]
    public List<Card> unlockedCards;

    public PlayerData()
    {
        unlockedCards = new List<Card>();

        gold = 0;
        tutorialRuns = 0;
        succesfulRuns = 0;
        failedRuns = 0;
    }
    public void NewGame()
    {
        DeckBuilder db = DeckBuilder.Instance;
        /*
        List<Rank>  unlockedRanks = new List<Rank>();
        unlockedRanks.Add(Rank.THREE);
        unlockedRanks.Add(Rank.FOUR);
        unlockedRanks.Add(Rank.FIVE);
        unlockedRanks.Add(Rank.SIX);
        unlockedRanks.Add(Rank.SEVEN);

        List<Suit> unlockedSuits = new List<Suit>();
        unlockedSuits.Add(Suit.HEARTS);
        unlockedSuits.Add(Suit.CLUBS);

        foreach(Suit suit in unlockedSuits)
        {
            foreach(Rank rank in unlockedRanks)
            {
                unlockedCards.Add(new Card(suit, rank));
            }
        }
        */
        for (int i = 0; i < 10; i++)
        {
            unlockedCards.Add(db.hearts[i]);
        }
        for (int i = 0; i < 10; i++)
        {
            unlockedCards.Add(db.clubs[i]);
        }
        
    }
    
    public bool UnlockCard(Card card)
    {
        if (unlockedCards.Exists(x => x.rank == card.rank && x.suit == card.suit))
            return false;
        unlockedCards.Add(new Card(card.suit, card.rank));
        return true;

    }
    public bool UnlockSuit(Suit suit)//odblokowane koloru to tylko odblokowanie kart 1-10 bez kart specjalnych które mozna inaczej zdobyc
    {
        if (unlockedCards.Exists(x => x.suit == suit))
            return false;
        var db = DeckBuilder.Instance;
        List<Card> tmp = db.colors.Find(x => x[0].suit == suit);
        for (int i = 0; i < 10; i++)
        {
            unlockedCards.Add(tmp[i]);
        }
        return true;

    }

}
