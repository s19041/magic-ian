using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CombatAi : MonoBehaviour
{
    protected Unit thisUnit;
    private void Awake()
    {
        thisUnit = gameObject.GetComponent<Unit>();
    }
    // Start is called before the first frame update
    public abstract int doSomething(Unit playerUnit, int turn, TextMeshProUGUI dialogueText);//trzeba popracowa� nad tym bo troch� nudne takie Ai by by�o


}
