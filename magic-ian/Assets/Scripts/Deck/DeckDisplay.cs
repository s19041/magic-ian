using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour//note to self: wi�kszo�� klas 'display' jest troche syfem wi�c kiedy� warto si� b�dzie temu przyjrze�
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panel;

    [SerializeField] private GameObject smallPanel;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject jackCardPrefab;
    private List<GameObject> cardsDisplayed;

    public Deck deck;
    public DeckBuilder deckBuilder;

    private static DeckDisplay _instance;

    public static DeckDisplay Instance
    { get { return _instance; } }

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

    public void ShowUnlockedCards()
    {
        ClearDeckDisplay();
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            var cards = PlayerManager.Instance.GetUnlockedCards();
            ShowDeckBuilder(cards, true);
        }
    }

    public void CloseDeckDisplay()
    {
        ClearDeckDisplay();
        panel.SetActive(false);
    }

    public void ShowDeckBuilder(List<Card> list, bool isEnabled)//deck, czy przyciski s� klikalne
    {
        foreach (Card card in list)
        {
            CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cardDisplay.UpdateDisplay();

            GameObject cardObject = Instantiate(cardPrefab);
            if (isEnabled)
            {
                if (Deck.Instance.GetCards().FindAll(x => x.suit == card.suit && card.rank == x.rank).Count != 0)
                    cardObject.GetComponent<CardDisplay>().inDeckColor = true;
                else
                    cardObject.GetComponent<CardDisplay>().inDeckColor = false;
            }
            else
                cardObject.GetComponent<CardDisplay>().inDeckColor = false;
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
            cardDisplay.UpdateDisplay();

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