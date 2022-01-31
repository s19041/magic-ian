using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public GameObject smallPanel;
    public GameObject cardPrefab;
    public GameObject jackCardPrefab;
    List<GameObject> cardsDisplayed;
    //static DeckDisplay instance;

    public DeckDisplay()
    {
        cardsDisplayed = new List<GameObject>();
    }
    public Deck deck;
    public DeckBuilder deckBuilder;

    
    int jackRange;

    public void ShowDeckBuilder(List<Card> list,bool isEnabled)
    {
        
        foreach (Card card in list)
        {
            CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cardDisplay.updateDisplay();

            GameObject cardObject = Instantiate(cardPrefab);
            cardObject.GetComponent<CardDisplay>().inDeckColor = isEnabled;
            cardsDisplayed.Add(cardObject);
            cardObject.transform.SetParent(panel.transform, false);
            cardObject.GetComponent<Button>().enabled = isEnabled;
           
        }
    }
    public void ClearDeckDisplay()
    {
        for(int i = 0; i < cardsDisplayed.Count; i++)
        {
            GameObject.Destroy(cardsDisplayed[i]);
        }
        cardsDisplayed.Clear();
        
    }
    public void ShowDeck()
    {
        ClearDeckDisplay();
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            ShowDeckBuilder(deck.getCards(), false);


        }


    }
    public void ShowDeckBuilder()
    {
        ClearDeckDisplay();
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            ShowDeckBuilder(deckBuilder.avalibleCards, true);


        }

    }
    
    public void ShowGraveyard()
    {
        ClearDeckDisplay();
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            ShowDeckBuilder(deck.getGraveyard(), true);


        }

    }
    public void ShowJackBuilder(int count)
    {
        ClearDeckDisplay();
        if (smallPanel.activeSelf)
        {
            smallPanel.SetActive(false);
        }
        else
        {
            smallPanel.SetActive(true);
            ShowJackBuilder(deck.getCards(), count);


        }

    }
    public void ShowJackBuilder(List<Card> list,int count)
    {
        //List<Card> threeCardsInOrder = new List<Card>();
        for (int i=1;i< count+1; i++)
        {
            CardDisplay cardDisplay = jackCardPrefab.GetComponent<CardDisplay>();
            cardDisplay.card = list[i];
            cardDisplay.updateDisplay();

            GameObject cardObject = Instantiate(jackCardPrefab);
            cardObject.GetComponent<CardDisplay>().inDeckColor = false;
            cardsDisplayed.Add(cardObject);
            cardObject.transform.SetParent(smallPanel.transform, false);
            cardObject.GetComponent<Button>().enabled = true;

        }//trzeba dopisa� logike wstawiania tego odpowiednio
        //najlepiej mo�e nowy prefab? w kt�rym dopiero po zaznaczeniu wszystkich kart znika display i sie ustawiaja.
        
    }
    public void CloseJackBuilder()
    {
        ClearDeckDisplay();
        
        smallPanel.SetActive(false);
        
        
    }



}