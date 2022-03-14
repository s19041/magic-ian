using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPowers 
{
    private int shuffleCount;
    public ItemPowers()
    {
        shuffleCount = 0;
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
}
