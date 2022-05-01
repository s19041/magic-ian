using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{

    public Canvas mainCanvas;
    public Canvas tableCanvas;
    public Canvas displayCanvas;








    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
        mainCanvas.gameObject.SetActive(true);

    }
    public void OnDisplayButton()
    {
        displayCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }
    public int itemNumber = 1;
    public void OnWearItemButton(Item item)
    {
        if (Deck.Instance.item1.itemName == item.itemName || Deck.Instance.item1.itemName == item.itemName)
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
            //tutaj jakaœ logika oznaczenia w Hubie ¿e item jest za³o¿ony (np zielona poœwiata)


    }
}
