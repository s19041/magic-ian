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
        if (item.itemName == ItemName.CYLINDER)
        {
            return ShuffleDeck();
        }
        if (item.itemName == ItemName.CAPE)
        {

        }
        if (item.itemName == ItemName.MONOCLE)
        {

        }
        if (item.itemName == ItemName.SLEEVE)
        {

        }
        return false;
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
