using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public int gold;
    public int price;


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
}
