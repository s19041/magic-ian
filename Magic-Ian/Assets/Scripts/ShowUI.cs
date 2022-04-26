using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameObject itemUI;
    public void TurnOnUI()
    {
        UI.SetActive(true);
        itemUI.SetActive(true);
    }

    public void TurnOffUI()
    {
        UI.SetActive(false);
        itemUI.SetActive(false);

    }
}
