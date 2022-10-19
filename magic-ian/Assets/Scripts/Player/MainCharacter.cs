using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Unit
{



    private static MainCharacter _instance;
    public static MainCharacter Instance { get { return _instance; } }
    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
        base.Awake();

    }
    public void ResetStats()
    {
        armor = 0;
    }


}
