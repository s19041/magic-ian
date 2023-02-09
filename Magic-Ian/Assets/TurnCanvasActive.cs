using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCanvasActive : MonoBehaviour
{
    public GameObject CreditsCanvas;
    public GameObject StartMenuCanvas;


    public void TurnCreditsCanvasOn()
    {
        CreditsCanvas.SetActive(true);
        StartMenuCanvas.SetActive(false);
    }

    public void TurnStartMenuOn()
    {
        CreditsCanvas.SetActive(false);
        StartMenuCanvas.SetActive(true);
    }
}
