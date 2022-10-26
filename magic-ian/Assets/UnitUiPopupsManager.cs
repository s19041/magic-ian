using UnityEngine;

public class UnitUiPopupsManager : MonoBehaviour
{
    public DamagePopup damagePopup;
    public HealPopup healPopup;
    public HealPopup armorPopup;
    public HealPopup damageUpPopup;
    public DamageNumber damageNumber;
    // Start is called before the first frame update

    private static UnitUiPopupsManager _instance;
    public static UnitUiPopupsManager Instance
    { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}