using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSettings : MonoBehaviour
{
    [SerializeField]
    InputField difficulty;
    [SerializeField]
    InputField additionalCombatRooms;//ta nazwa jest okropna
    public void OnDifficultyEdit()
    {
        if (Int32.Parse(difficulty.text) > 3)
            difficulty.text = "3";
        DungeonManager.Instance.diff = Int32.Parse(difficulty.text);
    }
    public void OnLengthEdit()
    {
        DungeonManager.Instance.additionalCombatRooms = Int32.Parse(additionalCombatRooms.text);
    }
}
