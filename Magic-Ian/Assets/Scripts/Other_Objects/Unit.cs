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
    public int barrierStacks;
    public DamagePopup damagePopup;
    public HealPopup healPopup;
    public HealPopup armorPopup;
    public HealPopup damageUpPopup;
    public DamageNumber damageNumber;
    public Vector3 numberPosition;
    public Animator animator;


    public int maxHp;

    public CombatAi combatAi;
    public Unit()
    {
        barrierStacks = 0;
        armor = 0;
        stunStacks = 0;
    }
    protected void Awake()
    {
        damagePopup = UnitUiPopupsManager.Instance.damagePopup;
        healPopup = UnitUiPopupsManager.Instance.healPopup;
        armorPopup = UnitUiPopupsManager.Instance.armorPopup;
        damageUpPopup = UnitUiPopupsManager.Instance.damageUpPopup;
        damageNumber = UnitUiPopupsManager.Instance.damageNumber;
        animator = GetComponent<Animator>();

        if (damage != 0)
        {
            numberPosition = this.transform.TransformPoint(-2, -2.7f, 0);
            damageNumber.Create(numberPosition, damage);
        }
        

        
    }
  
    public bool TakeDamage(int dmg)
    {
        if (barrierStacks > 0)
        {
            barrierStacks--;
        }
        else
        {
            if (armor > 0)
            {
                if (armor > dmg)
                    armor -= dmg;
                else
                {

                    hp -= (dmg - armor);
                    armor = 0;
                }

            }
            else
            {
                hp -= dmg;
            }
        }

        if(dmg>0)
        damagePopup.Create(this.gameObject.transform.position, dmg);



        if (hp <= 0)
        {
            animator.SetBool("IsDead", true);
            return true;
        }
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

                healPopup.Create(this.gameObject.transform.position);
            }
        }
    }
    public void ArmorUp(int armorAmount)
    {
        armor += armorAmount;
        if(armorAmount>0)
        armorPopup.Create(this.gameObject.transform.position);
    }
    public void AddStunStacks(int stacks)
    {
        stunStacks += stacks;
    }
    public void DamageUp(int additionalDamage)
    {
        damage += additionalDamage;
        if (additionalDamage > 0)
        {
            damageUpPopup.Create(this.gameObject.transform.position);
            
        }
    }
    public void AddBarriers(int stacks)
    {
        barrierStacks += stacks;
    }

    

}
