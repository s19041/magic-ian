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
    public bool inDeckColor;//zmienna robi¹ca to ¿e tylko podczas pokazywania deckBuilder karty w decku s¹ zielone
    void Start()
    {
        var db = FindObjectOfType<DeckBuilder>();
        inDeck = db.deck.cardInDeck(card);
        if (inDeck && inDeckColor)
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
            if (db.addCardToDeck(card) && inDeckColor)
            {
                gameObject.GetComponent<Image>().color = new Color32(87, 183, 78, 255);
                inDeck = true;
            }
        }
        else
        {
            
            var db = FindObjectOfType<DeckBuilder>();
            if ( inDeckColor)//tutaj by³o db.addCardToDeck(card) && building 
            {
                db.deck.removeCard(card);
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            inDeck = false;
        }
        

    }
    public void addToDeckOnClickJack()
    {
        var db = FindObjectOfType<DeckBuilder>();
        if (inDeck == true)
        {
            db.jackList.Add(card);
                gameObject.GetComponent<Image>().color = new Color32(87, 183, 78, 255);
                inDeck = true;
       
        }
        else
        {

                db.jackList.Remove(card);
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            
            inDeck = false;
        }
        if (db.jackList.Count==db.jackListCount)
        {
            db.addCardsFromJack();
            gameObject.transform.parent.gameObject.SetActive(false);
            Time.timeScale = 1;
            db.deck.setTopCard();
        }
        

    }


}

    
