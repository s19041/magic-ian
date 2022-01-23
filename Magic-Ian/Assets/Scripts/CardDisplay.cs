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
    void Start()
    {
        var db = FindObjectOfType<DeckBuilder>();
        inDeck = db.deck.removeCard(card);
        
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
            db.addCardToDeck(card);
            gameObject.GetComponent<Image>().color = new Color(87, 183, 78);
            inDeck = true;


        }
        else
        {
            inDeck = false;
            var db = FindObjectOfType<DeckBuilder>();
            db.deck.removeCard(card);
            gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
            inDeck = false;
        }
        

    }

}

    
