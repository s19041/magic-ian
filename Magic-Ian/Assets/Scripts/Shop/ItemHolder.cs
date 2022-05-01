using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] ShopItem item;
    [SerializeField] Image logo;
    [SerializeField] Text price;
    int gold;
    


    // Start is called before the first frame update
    void Start()
    {
        logo.sprite = item.GetComponent<Image>().sprite;
        price.text = item.GetPrice();

        gold = FindObjectOfType<GoldDisplay>().gold;

        
        
    }

    public void BuyHeal()
    {
        

    }


   

    
}
