using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject playerPrefab;
    

    public Transform playerBattleStation;
    

    

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static DungeonManager _instance;
    public static DungeonManager Instance { get { return _instance; } }
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

    }
}
