using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    // Start is called before the first frame update

    public Text hpText;
    public Text nameText;
    public Text armorText;
    
    

    public void SetHud(Unit unit)
    {
        hpText.text = unit.hp.ToString()+"/"+ unit.maxHp.ToString();
        nameText.text = unit.unitName;
        armorText.text = unit.armor.ToString();
    }
    public void SetStats(int hp,int maxHp,int armor)
    {
        hpText.text = hp.ToString() + "/" + maxHp.ToString();
        armorText.text = armor.ToString();
    }
}
