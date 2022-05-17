using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    public ShopItem item;
    public Image logo;
    public Text price;
    int gold;
    


    // Start is called before the first frame update
    void Awake()
    {
        logo.sprite = item.GetComponent<Image>().sprite;
        price.text = item.GetPrice();

        gold = FindObjectOfType<GoldDisplay>().gold;
        RemoveGoldListener();
        
        
    }
    private void Update()
    {
        
        if (PlayerManager.Instance.GetGold() < item.price)
        {
            gameObject.GetComponentInChildren<Button>().interactable=false;
        }
        else
        {
            gameObject.GetComponentInChildren<Button>().interactable = true;
        }
        
    }
    public void RemoveGoldListener()
    {
        gameObject.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            PlayerManager.Instance.RemoveGold(item.price);

            FindObjectOfType<GoldDisplay>().RefreshDisplay();
        });
    }







}
