using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour//tutaj bêdzie ca³y gameloop(albo w game manager(jeœli taka klasa powstanie))
{

    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    Transform playerBattleStation;

    public int diff;
    public int additionalCombatRooms;

    [SerializeField]
    Canvas nextSceneButtonCanvas;

    private SceneLoader sceneLoader;
    [SerializeField]
    List<Room> dungeon;
    [SerializeField]
    int currentRoomIndex;

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
        //CreateDungeon(diff, additionalCombatRooms);
    }
    public void CreateDungeon(int _diff,int _length)
    {
        DungeonCreator.Instance.SetupCreator(_diff, _length);
        DungeonCreator.Instance.CreateDungeon();
        dungeon = new List<Room>(DungeonCreator.Instance.GetDungeon());
    }

    public void OnCreateDungeonButton()
    {
        CreateDungeon(diff, additionalCombatRooms);
    }
    
    
    

    public void PrepareForRun()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    }
    public void OnLoadNextRoomButton()
    {
        
        currentRoomIndex++;
        sceneLoader.LoadRoom(dungeon[currentRoomIndex]);
        EnableNextSceneButton();
        if(dungeon[currentRoomIndex].type != Type.ENTRANCE)
            nextSceneButtonCanvas.gameObject.SetActive(false);
        
    }
    public CombatRoom GetCurrentCombatRoom()
    {
        return (CombatRoom)dungeon[currentRoomIndex];
    }
    public Room GetCurrentRoom()
    {
        return dungeon[currentRoomIndex];
    }
    public void StartDungeon()
    {
        if (dungeon.Count>0)
            OnLoadNextRoomButton();
    }
    public void EnableNextSceneButton()
    {
        nextSceneButtonCanvas.gameObject.SetActive(true);
    }
  
    
}
