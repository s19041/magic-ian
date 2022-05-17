using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuffs :MonoBehaviour
{
    Deck deck;


    public void Awake()
    {
        deck = Deck.Instance;
    }

    public void BuffAllCardDmg(int amount)//wzmocnienie dmg kart (1-10)
    {
        foreach (Card card in deck.GetCards())
        {
            if (!card.hasAbility)
            {
                card.damage += 1;
            }
        }
        deck.SaveDeckToBackup();
    }
    public void BuffAllSuitPower(Suit suit)//wzmocnienie koloru kart (1-10)
    {
        foreach (Card card in deck.GetCards())
        {
            if (card.hasAbility)
                continue;
            switch (suit)
            {
                case Suit.HEARTS:

                    card.heal += (int)(card.heal / 3);
                    break;

                case Suit.CLUBS:

                    card.armor += (int)(card.armor / 3);
                    break;

                case Suit.SPADES:

                    card.damage += (int)(card.damage / 3);
                    break;

                case Suit.DIAMONDS:

                    card.stunStacks += (int)(card.stunStacks / 3);
                    break;

                default:
                    break;

            }
        }
        deck.SaveDeckToBackup();
    }
    public void BuffKings(int amount)
    {
        deck.abilitySet.kingCards+=amount;//zwiêkszenie iloœci kart na jakie dzia³a król
        deck.SaveDeckToBackup();
    }
    public void QueensGivesBarrier()//zagranie królowej daje bariere
    {
        deck.abilitySet.barierQueen = true;
        deck.SaveDeckToBackup();
    }
    public void BuffJacks(int amount)
    {
        deck.abilitySet.jackCards += amount;//zwiêkszenie iloœci kart na jakie dzia³a walet
        deck.SaveDeckToBackup();
    }
    public void BuffJokers(int amount)
    {
        deck.abilitySet.jokerCards += amount;//zwiêkszenie iloœci kart na jakie dzia³a joker
        deck.SaveDeckToBackup();
    }


}
