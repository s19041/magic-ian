using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour//note to self: wiêkszoœæ klas 'display' jest troche syfem wiêc kiedyœ warto siê bêdzie temu przyjrzeæ
{
    // Start is called before the first frame update
    [SerializeField] GameObject panel;
    [SerializeField] GameObject smallPanel;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject jackCardPrefab;
    private List<GameObject> cardsDisplayed;

    public Deck deck;
    public DeckBuilder deckBuilder;

    private static DeckDisplay _instance;

    public static DeckDisplay Instance { get { return _instance; } }

    private void Awake()
    {

        cardsDisplayed = new List<GameObject>();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }

    }







    public void ClearDeckDisplay()
    {
        for (int i = 0; i < cardsDisplayed.Count; i++)
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
            ShowDeckBuilder(deck.GetCards(), false);


        }


    }
    public void ShowDeckShuffled()
    {
        ClearDeckDisplay();
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            List<Card> cards = new List<Card>();
            cards.AddRange(deck.GetCards()); ;
            for (int i = 0; i < cards.Count; i++)
            {
                Card temp = cards[i];
                int randomIndex = Random.Range(i, cards.Count);
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
            
            ShowDeckBuilder(cards, false);


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
            ShowDeckBuilder(PlayerManager.Instance.GetUnlockedCards(), true);


        }

    }
    public void CloseDeckDisplay()
    {
        ClearDeckDisplay();
        panel.SetActive(false);
    }
    public void ShowDeckBuilder(List<Card> list, bool isEnabled)//deck, czy przyciski s¹ klikalne
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
            ShowDeckBuilder(deck.GetGraveyard(), true);


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
            ShowJackBuilder(deck.GetCards(), count);


        }

    }
    public void ShowJackBuilder(List<Card> list, int count)
    {
        //List<Card> threeCardsInOrder = new List<Card>();
        for (int i = 1; i < count + 1; i++)
        {
            CardDisplay cardDisplay = jackCardPrefab.GetComponent<CardDisplay>();
            cardDisplay.card = list[i];
            cardDisplay.updateDisplay();

            GameObject cardObject = Instantiate(jackCardPrefab);
            cardObject.GetComponent<CardDisplay>().inDeckColor = false;
            cardsDisplayed.Add(cardObject);
            cardObject.transform.SetParent(smallPanel.transform, false);
            cardObject.GetComponent<Button>().enabled = true;

        }//trzeba dopisaæ logike wstawiania tego odpowiednio
         //najlepiej mo¿e nowy prefab? w którym dopiero po zaznaczeniu wszystkich kart znika display i sie ustawiaja.

    }
    public void CloseJackBuilder()
    {
        ClearDeckDisplay();

        smallPanel.SetActive(false);


    }



}
