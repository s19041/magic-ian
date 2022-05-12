using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    private TextMeshPro damageText;
    [SerializeField] private Transform pfDamageNumber;
    private Unit unit;


    public void Setup(int damage)
    {
        damageText.SetText(damage.ToString());
        unit = FindObjectOfType<Unit>();

    }

    private void Awake()
    {
        damageText = transform.GetComponent<TextMeshPro>();
    }

    public DamageNumber Create(Vector3 position, int damage)
    {
            
        Transform damageNumberTransform = Instantiate(pfDamageNumber, position, Quaternion.identity);
        DamageNumber damageNumber = damageNumberTransform.GetComponent<DamageNumber>();
        damageNumber.Setup(damage);

        return damageNumber;

    }

    private void Update()
    {
        damageText.SetText(unit.damage.ToString());
    }


}
