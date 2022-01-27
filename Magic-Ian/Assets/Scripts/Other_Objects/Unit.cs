using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int hp;
    public int armor;
    public int damage;
    public int stunStacks;

    public int maxHp;

    public CombatAi combatAi;

    public bool TakeDamage(int dmg)
    {
        if (armor > 0)
        {
            if (armor > dmg)
                armor -= dmg;
            else
            {
                
                hp -= (dmg-armor);
                armor = 0;
            }
            
        }
        else
        {
            hp -= dmg;
        }
        

        if (hp <= 0)
            return true;
        return false;
    }
    public void Heal(int healAmount)
    {
        if (hp < maxHp)
        {
            if (healAmount + hp > maxHp)
            {
                hp = maxHp;
            }
            else
            {
                hp += healAmount;
            }
        }
    }
    public void ArmorUp(int armorAmount)
    {
        armor += armorAmount;
    }
    public void AddStunStacks(int stacks)
    {
        stunStacks += stacks;
    }
   
}
