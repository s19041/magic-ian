using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoldDisplay : MonoBehaviour
{
    public int gold;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<PlayerManager>() != null)
        {
            gold = FindObjectOfType<PlayerManager>().GetGold();
        }
        RefreshDisplay();
    }

   public void RefreshDisplay()
    {
        this.GetComponent<Text>().text = PlayerManager.Instance.GetGold().ToString();
    }
}
