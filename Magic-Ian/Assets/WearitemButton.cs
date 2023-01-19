using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WearitemButton : MonoBehaviour
{
    public Item Item;

    private void Start()
    {
        if (!PlayerManager.Instance.GetUnlockedItems().Contains(Item))
            gameObject.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!PlayerManager.Instance.GetUnlockedItems().Contains(Item))
            gameObject.GetComponent<Button>().Select();
    }
}