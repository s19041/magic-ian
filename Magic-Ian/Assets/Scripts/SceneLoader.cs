using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void DeckScene()
    {
        SceneManager.LoadScene(1);
    }

    public void FightScene()
    {
        SceneManager.LoadScene(2);

    }
    public void HubScene()
    {
        SceneManager.LoadScene("HubMockup");
    }
    public void KubaScene()
    {
        SceneManager.LoadScene("SCENAKUBY");
    }
}
