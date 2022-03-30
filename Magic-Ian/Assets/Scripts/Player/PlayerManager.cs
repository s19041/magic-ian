using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int gold;
    public int tutorialRuns;
    public int succesfulRuns;
    public int failedRuns;

    // Update is called once per frame
    public List<Rank> unlockedRanks;
    public List<Suit> unlockedSuits;
    private static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }
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
        DontDestroyOnLoad(gameObject);
        unlockedRanks = new List<Rank>();
        unlockedSuits = new List<Suit>();
        gold = 0;
        tutorialRuns = 0;
        succesfulRuns = 0;
        failedRuns = 0;
    }
    public void NewGame()
    {
        unlockedRanks.Add(Rank.THREE);
        unlockedRanks.Add(Rank.FOUR);
        unlockedRanks.Add(Rank.FIVE);
        unlockedRanks.Add(Rank.SIX);
        unlockedRanks.Add(Rank.SEVEN);

        unlockedSuits.Add(Suit.HEARTS);
        unlockedSuits.Add(Suit.CLUBS);
    }
    public bool UnlockCard(Card card)
    {
        if (unlockedRanks.Contains(card.rank))
            return false;
        
        
        if ( card.suit == Suit.SPECIAL)
            UnlockSuit(Suit.SPECIAL);
        unlockedRanks.Add(card.rank);
        return true;

    }
    public bool UnlockSuit(Suit suit)
    {
        if (unlockedSuits.Contains(suit))
            return false;
        unlockedSuits.Add(suit);
        return true;
    }
}
