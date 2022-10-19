using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{

    public Card card;

    [SerializeField] Image artworkImage;

    [SerializeField] Text descriptionText;

    public bool inDeck;
    public bool inDeckColor;//zmienna robi¹ca to ¿e tylko podczas pokazywania deckBuilder karty w decku s¹ zielone
    DeckBuilder db;
    void Start()
    {
         db = DeckBuilder.Instance;
        inDeck = db.deck.CardInDeck(card);
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

    public void addToDeckOnClick()
    {
        if (inDeck == false)
        {
            //varr db = FindObjectOfType<DeckBuilder>();
            if (db.addCardToDeck(card) && inDeckColor)
            {
                gameObject.GetComponent<Image>().color = new Color32(87, 183, 78, 255);
                inDeck = true;
            }
        }
        else
        {

            //var db = FindObjectOfType<DeckBuilder>();
            if (inDeckColor)//tutaj by³o db.addCardToDeck(card) && building 
            {
                db.deck.RemoveCard(card);
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            inDeck = false;
        }


    }
    public void addToDeckOnClickJack()
    {
        //var db = DeckBuilder.Instance;
        if (inDeck == true)
        {
            db.jackList.Add(card);
            gameObject.GetComponent<Image>().color = new Color32(87, 183, 78, 255);
            inDeck = false;

        }
        else
        {

            db.jackList.Remove(card);
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            inDeck = true;
        }
        if (db.jackList.Count == db.jackListCount)
        {
            db.addCardsFromJack();
            gameObject.transform.parent.gameObject.SetActive(false);
            Time.timeScale = 1;
            db.deck.SetTopCard();
        }


    }
    public void UnlockCardOnClick()
    {
        PlayerManager.Instance.UnlockCard(card);
        GetComponent<Button>().interactable = false;

    }


}


