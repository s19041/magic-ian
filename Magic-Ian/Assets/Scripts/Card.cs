using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit {CLUBS, DIAMONS, HEARTS, SPADES }
public enum Rank {ACE,TWO,THREE,FOUR,FIVE,SIX,SEVEN,EIGHT,NINE,TEN,JACK,QUEEN,KING }

[CreateAssetMenu(fileName = "new Card",menuName ="Card")]
public class Card : ScriptableObject
{
    public string name;
    public Suit suit;
    public Rank rank;
    public string description;

    public int damage;
    public int heal;
    public int armor;
    public int stunStacks;

    public Sprite artwork;
    void Start()
    {
        name = rank.ToString() + " Of " + suit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
