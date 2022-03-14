using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit {CLUBS, DIAMONDS, HEARTS, SPADES,SPECIAL,EMPTY }
public enum Rank {ACE,TWO,THREE,FOUR,FIVE,SIX,SEVEN,EIGHT,NINE,TEN,JACK,QUEEN,KING,JOKER,EMPTY }

[CreateAssetMenu(fileName = "new Card",menuName ="Card")]
public class Card : ScriptableObject
{
    public Suit suit;
    public Rank rank;

    public string cardName;
    public string description;
    public Sprite artwork;

    public int damage;
    public int heal;
    public int armor;
    public int stunStacks;

    public bool aoe;
    public bool hasAbility;



    public Card()
    {
        SetCard();
    }
    
    public Card(Suit _suit, Rank _rank)
    {
        rank = _rank;
        suit = _suit;
        SetCard();
    }
    
    // Update is called once per frame
    public void SetCard()
    {
        SetName();
        SetDamage();
        SetStats();
        SetHasAbility();
        aoe = false;
    }
    public void SetName()
    {
        cardName = rank.ToString() + " Of " + suit.ToString();
    }
    public void SetDamage()
    {
        switch (rank)
        {
            case Rank.ACE:
                
                    damage = 1;
                    break;
                
            case Rank.TWO:
                
                    damage = 2;
                    break;
                
            case Rank.THREE:
                
                    damage = 3;
                    break;
                
            case Rank.FOUR:
                
                    damage = 4;
                    break;
                
            case Rank.FIVE:
                
                    damage = 5;
                    break;

            case Rank.SIX:
                
                    damage = 6;
                    break;
                
            case Rank.SEVEN:
                
                    damage = 7;
                    break;
                
            case Rank.EIGHT:
                
                    damage = 8;
                    break;
                
            case Rank.NINE:
                
                    damage = 9;
                    break;
                
            case Rank.TEN:
                
                    damage = 10;
                    break;

            default:
                damage = 0;
                break;

        }
    }
    public void SetStats()
    {//tu mo¿na dodaæ warunek który wyklucza karty specjalne
        if (damage == 0)
        {
            heal = 0;
            stunStacks = 0;
            armor = 0;
            return;
        }

        switch (suit)
        {
            case Suit.HEARTS:

                heal = (int)(10 - damage) / 2;
                break;

            case Suit.CLUBS:

                armor = 10 - damage;
                break;

            case Suit.SPADES:

                damage += (int)(damage / 2);
                break;

            case Suit.DIAMONDS:

                stunStacks = 10 - damage;
                break;

            default:
                break;

        }
    }
    public void SetHasAbility()
    {
        switch (rank)
        {
            case Rank.KING:

                hasAbility = true;
                break;

            case Rank.QUEEN:

                hasAbility = true;
                break;

            case Rank.JACK:

                hasAbility = true;
                break;

            case Rank.JOKER:

                hasAbility = true;
                break;

            default:
                hasAbility = false;
                break;

        }
    }

}
