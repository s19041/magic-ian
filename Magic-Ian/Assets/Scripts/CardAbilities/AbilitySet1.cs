using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySet1 : AbstractAbilitySet
{
    

    public override void JackAbility(Card card)//
    {
        int count = 3;

        if (count > Deck.Instance.GetCards().Count - 1)//-1 poniewa¿ .Count zwróci razem z górn¹ kart¹ a jej nie jestesmy w stanie przestasowaæ
            count = Deck.Instance.GetCards().Count - 1;//dziwna logika ale dzia³a
        deckbuilder.jackListCount = count;


        DeckBuilder.Instance.jackList = new List<Card>();


        DeckBuilder.Instance.deckDisplay.ShowJackBuilder(count);

    }

    public override void JokerAbility(Card card)// nastêpna karta bêdzie aoe a¿ do jej zagrania.
    {
        Deck.Instance.SetCardAoe(1);
    }

    public override void KingAbility(Card card)// Daje do x kart buff swojego koloru (np krol serce daje ka¿dej karcie dodatkowo efekt serca00) // na razie 2 nastêpne karty
    {
        int kingPower = 5;//tu zmieniac dla balansu
        int x = 2;//iloœæ zbufowanych kart
        if (Deck.Instance.GetCards().Count < 2)
            x = Deck.Instance.GetCards().Count;
        for (int i = 0; i < x; i++)
        {
            if (card.suit == Suit.HEARTS)
            {
                Deck.Instance.GetCards()[i].heal += (int)(kingPower / 2);
            }
            if (card.suit == Suit.CLUBS)
            {
                Deck.Instance.GetCards()[i].armor += kingPower;
            }
            if (card.suit == Suit.SPADES)
            {
                Deck.Instance.GetCards()[i].damage += (int)(kingPower / 2);
            }
            if (card.suit == Suit.DIAMONDS)
            {
                Deck.Instance.GetCards()[i].stunStacks = kingPower;
            }
        }
    }

    public override void QueenAbility(Card card) //zamienia siê w poprzedni¹ kartê
    {
       if(Deck.Instance.lastCard!=null)
            Deck.Instance.ChangeCardAt(0, Deck.Instance.lastCard);
    }
}
