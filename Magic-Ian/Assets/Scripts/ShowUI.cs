using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] GameObject UI;
    public void TurnOnUI()
    {
        UI.SetActive(true);
    }

    public void TurnOffUI()
    {
        UI.SetActive(false);
        
    }
}
