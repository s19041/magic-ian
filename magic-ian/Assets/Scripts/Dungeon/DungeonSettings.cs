using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSettings : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown difficulty;

    [SerializeField]
    private InputField additionalCombatRooms;//ta nazwa jest okropna

    [SerializeField]
    private Button generateButton;

    public void OnDifficultyEdit()
    {
        DungeonManager.Instance.diff = difficulty.value;
        generateButton.enabled = true;
    }

    public void OnLengthEdit()
    {
        DungeonManager.Instance.additionalCombatRooms = Int32.Parse(additionalCombatRooms.text);
        generateButton.enabled = true;
    }

    public void OnGenerateDungeonButton()
    {
        DungeonManager.Instance.OnCreateDungeonButton();
        generateButton.enabled = false;
    }
}