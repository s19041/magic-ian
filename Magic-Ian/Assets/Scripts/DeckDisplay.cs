using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public GameObject cardPrefab;
    List<GameObject> cardsDisplayed= new List<GameObject>();

  
    public void showListOfCards(List<Card> list)
    {
        foreach (Card card in list)
        {
            
            CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cardDisplay.updateDisplay();

            GameObject cardObject = Instantiate(cardPrefab);
            cardsDisplayed.Add(cardObject);
            cardObject.transform.SetParent(panel.transform, false);





        }
    }
    public void showDeckBuilder(List<Card> list,bool isEnabled)
    {
        
        foreach (Card card in list)
        {
            CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cardDisplay.updateDisplay();

            GameObject cardObject = Instantiate(cardPrefab);
            cardsDisplayed.Add(cardObject);
            cardObject.transform.SetParent(panel.transform, false);
            cardObject.GetComponent<Button>().enabled = isEnabled;
        }
    }
    public void clearDeckBuilder()
    {
        for(int i = 0; i < cardsDisplayed.Count; i++)
        {
            GameObject.Destroy(cardsDisplayed[i]);
        }
        cardsDisplayed.Clear();
        
    }
    
}
