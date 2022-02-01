using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void HubScene()
    {
        SceneManager.LoadScene(1);

    }
    public void EntranceScene()
    {
        SceneManager.LoadScene(2);

    }
    public void FightScene()
    {
        SceneManager.LoadScene(3);

    }
    
    
}
