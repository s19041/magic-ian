using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItemsDisplay : MonoBehaviour
{
    public Item Item1;
    public Item Item2;
    public GameObject item1Icon;
    public GameObject item2Icon;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Item1 = Deck.Instance.item1;
        Item2 = Deck.Instance.item2;
        item1Icon.GetComponent<SpriteRenderer>().sprite = Item1.artwork;
        item2Icon.GetComponent<SpriteRenderer>().sprite = Item2.artwork;
    }
}