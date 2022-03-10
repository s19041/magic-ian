using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalHUB : MonoBehaviour
{
    [SerializeField] BattleSystem battleSystem;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(battleSystem.GetUnitsLength());
    }

    
}
