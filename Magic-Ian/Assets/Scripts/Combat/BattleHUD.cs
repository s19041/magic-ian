using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI armorText;
    

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
