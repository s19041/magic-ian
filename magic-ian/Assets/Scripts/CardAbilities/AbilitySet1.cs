using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySet1 : AbstractAbilitySet
{

    public override bool JackAbility(Card card)//
    {
        Time.timeScale = 0;//NAD TYM SIÊ ZASTANOWIÆ BO TO CHYBA ŒREDNIE

        if (jackCards > cards.Count - 1)//-1 poniewa¿ .Count zwróci razem z górn¹ kart¹ a jej nie jestesmy w stanie przestasowaæ
            jackCards = cards.Count - 1;//dziwna logika ale dzia³a
        deckbuilder.jackListCount = jackCards;


        deckbuilder.jackList = new List<Card>();


        deckbuilder.deckDisplay.ShowJackBuilder(jackCards);
        return false;
    }

    public override bool JokerAbility(Card card)// nastêpna ZADAJ¥CA OBRA¯ENIA karta bêdzie aoe a¿ do jej zagrania.
    {
        int x = 0;
        for(int i=1;i< cards.Count;i++)//i=1 poniewa¿ chcemy ¿eby nastêpna karta by³a aoe a nie aktualna
        {
            
            if (cards[i].damage != 0)
            {
                if (cards.Count > i)
                {
                    cards[i].aoe = true;
                    x++;
                }
                if(x>=jokerCards)
                    break;
            }
            
                
        }
        return false;
    }

    public override bool KingAbility(Card card)// Daje do x kart buff swojego koloru (np krol serce daje ka¿dej karcie dodatkowo efekt serca00) // na razie 2 nastêpne karty
    {
        
        int kingPower = 5;//tu zmieniac dla balansu
        if (cards.Count < kingCards - 1)
            kingCards = cards.Count-1;
        for (int i = 1; i < kingCards+1; i++)
        {
            if (cards[i].damage != 0)
            {
                AddStatOfSuit(card.suit, kingPower, i);
            }
            else
            {
                if (kingCards <= cards.Count)
                    kingCards++;
            }
                
        }
        return false;
    }

    public override bool QueenAbility(Card card) //zamienia siê w poprzedni¹ kartê
    {
        if (barierQueen)
        {
            MainCharacter.Instance.AddBarriers(1);
        }
        if (deck.lastCard != null)
        {
            deck.ChangeCardAt(0, Deck.Instance.lastCard);
            return true;
        }
        return false;
        
    }
    public void AddStatOfSuit(Suit suit, int power, int cardIndex)
    {
        if (suit == Suit.HEARTS)
        {
            cards[cardIndex].heal += (int)(power / 2);
        }
        if (suit == Suit.CLUBS)
        {
            cards[cardIndex].armor += power;
        }
        if (suit == Suit.SPADES)
        {
            cards[cardIndex].damage += (int)(power / 2);
        }
        if (suit == Suit.DIAMONDS)
        {
            cards[cardIndex].stunStacks = power;
        }
    }
}
