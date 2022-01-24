using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public GameObject cardPrefab;
    List<GameObject> cardsDisplayed;
    public DeckDisplay()
    {
        cardsDisplayed = new List<GameObject>();
    }



    public void showDeckBuilder(List<Card> list,bool isEnabled)
    {
        
        foreach (Card card in list)
        {
            CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cardDisplay.updateDisplay();

            GameObject cardObject = Instantiate(cardPrefab);
            cardObject.GetComponent<CardDisplay>().building = isEnabled;
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
