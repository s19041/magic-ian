using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySet1 : AbstractAbilitySet
{
    

    public override bool JackAbility(Card card)//
    {
        Time.timeScale = 0;//NAD TYM SIÊ ZASTANOWIÆ BO TO CHYBA ŒREDNIE
        int count = 3;

        if (count > deck.GetCards().Count - 1)//-1 poniewa¿ .Count zwróci razem z górn¹ kart¹ a jej nie jestesmy w stanie przestasowaæ
            count = deck.GetCards().Count - 1;//dziwna logika ale dzia³a
        deckbuilder.jackListCount = count;


        deckbuilder.jackList = new List<Card>();


        deckbuilder.deckDisplay.ShowJackBuilder(count);
        return false;
    }

    public override bool JokerAbility(Card card)// nastêpna ZADAJ¥CA OBRA¯ENIA karta bêdzie aoe a¿ do jej zagrania.
    {
        for(int i=1;i<deck.GetCards().Count;i++)//i=1 poniewa¿ chcemy ¿eby nastêpna karta by³a aoe a nie aktualna
        {
            if (deck.GetCards()[i].damage != 0)
            {
                deck.SetCardAoe(i);
                break;
            }
            
                
        }
        return false;
    }

    public override bool KingAbility(Card card)// Daje do x kart buff swojego koloru (np krol serce daje ka¿dej karcie dodatkowo efekt serca00) // na razie 2 nastêpne karty
    {
        int kingPower = 5;//tu zmieniac dla balansu
        int x = 2;//iloœæ zbufowanych kart
        if (deck.GetCards().Count < x-1)
            x = deck.GetCards().Count-1;
        for (int i = 1; i < x+1; i++)
        {
            if (deck.GetCards()[i].damage != 0)
            {
                deck.AddStatOfSuit(card.suit, kingPower, i);
            }
            else
            {
                if (x <= deck.GetCards().Count)
                    x++;
            }
                
        }
        return false;
    }

    public override bool QueenAbility(Card card) //zamienia siê w poprzedni¹ kartê
    {
        if (deck.lastCard != null)
        {
            deck.ChangeCardAt(0, Deck.Instance.lastCard);
            return true;
        }
        return false;
        
    }
}
