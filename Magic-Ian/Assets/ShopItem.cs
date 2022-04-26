using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] string name;
    public int gold;




    public string GetPrice()
    {
        return price.ToString();
    }


    public void BuyHeal()
    {
        gold = FindObjectOfType<PlayerManager>().GetGold();
        if (price <= gold)
        {
            FindObjectOfType<MainCharacter>().Heal(10);
            gold = -price;

            FindObjectOfType<GoldDisplay>().RefreshDisplay();
        }
    }

    public void BuyArmor()
    {
        gold = FindObjectOfType<PlayerManager>().GetGold();
        if (price <= gold)
        {
            FindObjectOfType<MainCharacter>().ArmorUp(10);
            gold = -price;

            FindObjectOfType<GoldDisplay>().RefreshDisplay();
        }
    }


}
