using UnityEngine;

public class HubManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas tableCanvas;
    public Canvas displayCanvas;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnBuildDeckButton()
    {
        tableCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }

    public void OnBackButton()
    {
        displayCanvas.gameObject.SetActive(false);
        tableCanvas.gameObject.SetActive(false);
        DeckDisplay.Instance.CloseDeckDisplay();

        mainCanvas.gameObject.SetActive(true);
    }

    public void OnDisplayButton()
    {
        displayCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }

    public void ShowDeckButton()
    {
        DeckDisplay.Instance.ShowDeck();
    }

    public void ShowDeckBuilderButton()
    {
        DeckDisplay.Instance.ShowUnlockedCards();
    }

    public int itemNumber = 1;

    public void OnWearItemButton(Item item)
    {
        if (Deck.Instance.item1.itemName == item.itemName || Deck.Instance.item2.itemName == item.itemName)
            return;
        if (itemNumber == 1)
        {
            Deck.Instance.item1 = item;
            itemNumber = 2;
        }
        else
        {
            Deck.Instance.item2 = item;
            itemNumber = 1;
        }
    }

    public void OnStartDungeonButton()
    {
        DungeonManager.Instance.OnCreateDungeonButton();
        DungeonManager.Instance.StartDungeon();
    }
}