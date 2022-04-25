using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] string name;




    public string GetPrice()
    {
        return price.ToString();
    }


}
