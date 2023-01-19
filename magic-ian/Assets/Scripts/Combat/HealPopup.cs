using UnityEngine;

public class HealPopup : MonoBehaviour
{
    private float dissapearTimer;
    private Color iconColor;
    [SerializeField] private Transform pfHealPopup;

    private void Start()
    {
        iconColor = this.GetComponent<SpriteRenderer>().color;
    }

    public HealPopup Create(Vector3 position)
    {
        Transform healPopupTransform = Instantiate(pfHealPopup, position, Quaternion.identity);
        HealPopup healPopup = healPopupTransform.GetComponent<HealPopup>();

        return healPopup;
    }

    private void Update()
    {
        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer < 0)
        {
            float dissapearSpeed = 0.5f;
            iconColor.a -= dissapearSpeed * Time.deltaTime;
            this.GetComponent<SpriteRenderer>().color = iconColor;
            if (iconColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}