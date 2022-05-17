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
        deck.abilitySet.kingCards+=amount;//zwi�kszenie ilo�ci kart na jakie dzia�a kr�l
        deck.SaveDeckToBackup();
    }
    public void QueensGivesBarrier()//zagranie kr�lowej daje bariere
    {
        deck.abilitySet.barierQueen = true;
        deck.SaveDeckToBackup();
    }
    public void BuffJacks(int amount)
    {
        deck.abilitySet.jackCards += amount;//zwi�kszenie ilo�ci kart na jakie dzia�a walet
        deck.SaveDeckToBackup();
    }
    public void BuffJokers(int amount)
    {
        deck.abilitySet.jokerCards += amount;//zwi�kszenie ilo�ci kart na jakie dzia�a joker
        deck.SaveDeckToBackup();
    }


}
