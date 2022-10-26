using System;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSettings : MonoBehaviour
{
    [SerializeField]
    private InputField difficulty;

    [SerializeField]
    private InputField additionalCombatRooms;//ta nazwa jest okropna

    [SerializeField]
    private Button generateButton;

    public void OnDifficultyEdit()
    {
        if (Int32.Parse(difficulty.text) > 3)
            difficulty.text = "3";
        DungeonManager.Instance.diff = Int32.Parse(difficulty.text);
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