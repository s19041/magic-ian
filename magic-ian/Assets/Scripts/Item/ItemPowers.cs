using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPowers 
{
    public int shuffleCount;
    Card sleeve;
    public Card capeCard;
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
                return UseCape();


            case ItemName.MONOCLE:
                return UseMonocle();


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
    public bool UseCape()
    {
        List<Card> graveyard = Deck.Instance.GetGraveyard();
        if (graveyard.Count < 1)
            return false;
        int i=Random.Range(0, graveyard.Count);
        capeCard = graveyard[i];
        graveyard.RemoveAt(i);
        Deck.Instance.GetCards().Add(capeCard);
        return false;
    }
    public bool UseMonocle()
    {
        DeckDisplay.Instance.ShowDeck();
        return false;
    }
}
