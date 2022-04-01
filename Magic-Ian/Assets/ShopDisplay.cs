using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowItems()
    {
        /*
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
        */
    }
}
