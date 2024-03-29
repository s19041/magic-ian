using UnityEngine;
using UnityEngine.UI;

public class GoldDisplay : MonoBehaviour
{
    public int gold;

    // Start is called before the first frame update
    private void Awake()
    {
        if (FindObjectOfType<PlayerManager>() != null)
        {
            gold = PlayerManager.Instance.GetGold();
        }
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        this.GetComponent<Text>().text = PlayerManager.Instance.GetGold().ToString();
    }
}