using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPowers 
{
    private int shuffleCount;
    Card sleeve;
    public ItemPowers()
    {
        shuffleCount = 0;
        sleeve = null;
    }
   public bool ActivateItemPower(Item item)
    {
        switch (item.itemName)
        {
            case ItemName.CYLINDER:

                return ShuffleDeck();

            case ItemName.CAPE:



            case ItemName.MONOCLE:



            case ItemName.SLEEVE:
                return UseSleeve();


            default:
                return false;

        }
    }
    public bool ShuffleDeck()//Cylinder power
    {
        shuffleCount++;
        Deck.Instance.Shuffle();
        if (shuffleCount >= 2)
        {
            shuffleCount = 0;
            return true;
        }
        return false;
            
    }
    public bool UseSleeve()
    {
        if (sleeve == null)
        {
            sleeve = Deck.Instance.topCard;
            Deck.Instance.ThrowOutTopCard();

            return false;
        }
        else
        {
            Deck.Instance.GetCards().Insert(0,sleeve);
            Deck.Instance.SetTopCard();
            sleeve = null;
            return false;
        }

    }
}
