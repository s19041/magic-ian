using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    // Start is called before the first frame update
    public int gold;
    public int runs;
    public int succesfulRuns;
    public int failedRuns;

    [SerializeField]
    private List<Card> unlockedCards;
    [SerializeField]
    private List<Item> unlockedItems;

    [HideInInspector]
    public List<Suit> unlockedCardsSuit;//wszystkie unlocked cards s¹ przek³adane na te dwie listy dla serializacji
    [HideInInspector]
    public List<Rank> unlockedCardsRank;
    [HideInInspector]
    public List<ItemName> unlockedItemEnums;//do serializacji

    public PlayerData()
    {
        unlockedCards = new List<Card>();
        unlockedItems= new List<Item>();

        gold = 0;
        runs = 0;
        succesfulRuns = 0;
        failedRuns = 0;
    }
    public void NewGame()
    {
        DeckBuilder db = DeckBuilder.Instance;
     
        for (int i = 2; i < 7; i++)
        {
            unlockedCards.Add(db.hearts[i]);//king
        }
        unlockedCards.Add(db.hearts[10]);
        for (int i = 2; i < 7; i++)
        {
            unlockedCards.Add(db.clubs[i]);
        }
        unlockedCards.Add(db.clubs[10]);//king
        unlockedItems.Add(db.items[0]);

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
    public List<Card> GetUnlockedCards()
    {
        return unlockedCards;
    }
    //rozwi¹zania znalezione na necie by³ nadmiarowe wiêc serializacja w taki dziwny sposób 
    public void Serialize()
    {
        unlockedCardsRank = new List<Rank>();
        unlockedCardsSuit = new List<Suit>();
        foreach(Card card in unlockedCards)
        {
            unlockedCardsRank.Add(card.rank);
            unlockedCardsSuit.Add(card.suit);
        }
        unlockedItemEnums = new List<ItemName>();
        foreach(Item item in unlockedItems)
        {
            unlockedItemEnums.Add(item.itemName);
        }
    }
    public void Deserialize()
    {

        unlockedCards = new List<Card>();
        DeckBuilder db = DeckBuilder.Instance;
        for (int i = 0; i < unlockedCardsRank.Count; i++)
        {
            switch (unlockedCardsSuit[i])
            {
                case Suit.HEARTS:

                    unlockedCards.Add(db.hearts.Find(x => x.rank == unlockedCardsRank[i]));
                    break;

                case Suit.CLUBS:

                    unlockedCards.Add(db.clubs.Find(x => x.rank == unlockedCardsRank[i]));
                    break;

                case Suit.SPADES:

                    unlockedCards.Add(db.spades.Find(x => x.rank == unlockedCardsRank[i]));
                    break;

                case Suit.DIAMONDS:

                    unlockedCards.Add(db.diamonds.Find(x => x.rank == unlockedCardsRank[i]));
                    break;
                case Suit.SPECIAL:

                    unlockedCards.Add(db.special.Find(x => x.rank == unlockedCardsRank[i]));
                    break;

                default:
                    break;

            }
        }
        unlockedItems = new List<Item>();
        foreach(ItemName itemName in unlockedItemEnums)
        {
            unlockedItems.Add(db.items.Find(x => x.itemName == itemName));
        }
    }

    public int GetGold()
    {
        return gold;
    }

}
