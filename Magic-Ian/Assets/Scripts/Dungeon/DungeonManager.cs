using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;


    [SerializeField]
    Transform playerBattleStation;//tutaj bêdzie ca³y gameloop(albo w game manager)

    
    public int diff;
    public int additionalCombatRooms;

    private SceneLoader sceneLoader;
    [SerializeField]
    List<Room> dungeon;
    public int currentRoomIndex;

    // Start is called before the first frame update
    private void CreateDungeon(int _diff,int _length)
    {
        DungeonCreator.Instance.SetupCreator(_diff, _length);
        DungeonCreator.Instance.CreateDungeon();
        dungeon = new List<Room>(DungeonCreator.Instance.GetDungeon());
    }

    private void OnCreateDungeonButton()
    {
        CreateDungeon(diff, additionalCombatRooms);
    }
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
        currentRoomIndex = -1;
        sceneLoader = SceneLoader.Instance;
        CreateDungeon(diff, additionalCombatRooms);
    }
    public void PrepareForRun()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    }
    public void LoadNextRoom()
    {
        currentRoomIndex++;
        sceneLoader.LoadRoom(dungeon[currentRoomIndex]);
    }
    public Room GetCurrentRoom()
    {
        return dungeon[currentRoomIndex];
    }
    public void StartDungeon()
    {
        CreateDungeon(diff, additionalCombatRooms);
        LoadNextRoom();
    }
    
}
