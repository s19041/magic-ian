using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{

    public Card card;

    public Image artworkImage;

    public Text descriptionText;

    public Text itemButtonText;// nie jestem przekonany ze to tutaj powinno byc ale nie mam pomys³u
    // Start is called before the first frame update
    public bool inDeck;
    public bool building;//zmienna robi¹ca to ¿e tylko podczas pokazywania deckBuilder kartty w decku s¹ zielone
    void Start()
    {
        var db = FindObjectOfType<DeckBuilder>();
        inDeck = db.deck.cardInDeck(card);
        if (inDeck && building)
        {
            gameObject.GetComponent<Image>().color = new Color32(87, 183, 78, 255);
        }
        
        //updateDisplay();
    }
 
    public void updateDisplay()
    {
        artworkImage.sprite = card.artwork;

        descriptionText.text = card.description;
    }
    public void setItemButtonText(Item item)
    {
        itemButtonText.text = item.buttonText;
    }
    public void addToDeckOnClick()
    {
        if (inDeck == false)
        {
            var db = FindObjectOfType<DeckBuilder>();
            if (db.addCardToDeck(card) && building)
            {
                gameObject.GetComponent<Image>().color = new Color32(87, 183, 78, 255);
                inDeck = true;
            }
            
            


        }
        else
        {
            
            var db = FindObjectOfType<DeckBuilder>();
            db.deck.removeCard(card);
            if (db.addCardToDeck(card) && building)
            {
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            inDeck = false;
        }
        

    }
    

}

    
