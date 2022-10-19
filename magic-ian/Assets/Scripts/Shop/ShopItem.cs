using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour//listenery dla usuwania golda s¹ w ItemHolder
{
    public int price;
    [SerializeField] string name;






    public string GetPrice()
    {
        return price.ToString();
    }

    public void BuyHeal()
    {
        FindObjectOfType<MainCharacter>().Heal(10);
    }

    public void BuyArmor()
    {
        FindObjectOfType<MainCharacter>().ArmorUp(10);
    }
    public void BuyStunClear()
    {
        FindObjectOfType<MainCharacter>().stunStacks = 0;
        
    }



}
