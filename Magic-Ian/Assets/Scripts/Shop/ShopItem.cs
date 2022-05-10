using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price;
    [SerializeField] string name;






    public string GetPrice()
    {
        return price.ToString();
    }


    public void BuyHeal()
    {
        int gold = PlayerManager.Instance.GetGold();

        if (price <= gold)
        {
            FindObjectOfType<MainCharacter>().Heal(10);
            PlayerManager.Instance.RemoveGold(price);

            FindObjectOfType<GoldDisplay>().RefreshDisplay();
        }
    }

    public void BuyArmor()
    {
        int gold = PlayerManager.Instance.GetGold();
        if (price <= gold)
        {
            FindObjectOfType<MainCharacter>().ArmorUp(10);
            PlayerManager.Instance.RemoveGold(price);

            FindObjectOfType<GoldDisplay>().RefreshDisplay();
        }
    }
    public void BuyStunClear()
    {
        int gold = PlayerManager.Instance.GetGold();
        if (price <= gold)
        {
            FindObjectOfType<MainCharacter>().stunStacks=0;
            PlayerManager.Instance.RemoveGold(price);
                
            FindObjectOfType<GoldDisplay>().RefreshDisplay();
        }
    }


}
