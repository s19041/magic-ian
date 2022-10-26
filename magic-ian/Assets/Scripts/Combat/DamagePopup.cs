using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro popupText;
    private float dissapearTimer;
    private Color textColor;
    [SerializeField] private Transform pfdamagePopup;

    public DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(pfdamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private void Awake()
    {
        popupText = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        popupText.SetText(damageAmount.ToString());
        textColor = popupText.color;
    }

    private void Update()
    {
        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer < 0)
        {
            float dissapearSpeed = 0.5f;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            popupText.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}