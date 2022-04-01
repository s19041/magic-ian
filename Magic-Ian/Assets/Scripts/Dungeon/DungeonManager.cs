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
    [SerializeField]
    Canvas leaveDungeonButtonCanvas;

    private SceneLoader sceneLoader;
    [SerializeField]
    List<Room> dungeonRooms;
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
        dungeonRooms = new List<Room>(DungeonCreator.Instance.GetDungeon());
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
        sceneLoader.LoadRoom(dungeonRooms[currentRoomIndex]);
        EnableNextSceneButton();
        if(dungeonRooms[currentRoomIndex].type != Type.ENTRANCE)
            nextSceneButtonCanvas.gameObject.SetActive(false);
        
    }
    public CombatRoom GetCurrentCombatRoom()
    {
        return (CombatRoom)dungeonRooms[currentRoomIndex];
    }
    public Room GetCurrentRoom()
    {
        return dungeonRooms[currentRoomIndex];
    }
    public void StartDungeon()
    {
        if (Deck.Instance.GetCards().Count == 12)
        {
            if (dungeonRooms.Count > 0)
                OnLoadNextRoomButton();
        }
        else
        {
            //tutaj zrobiæ jakiœ popup ¿e niewystarczaj¹ca iloœæ kart
        }
        
    }
    public void EnableNextSceneButton()
    {
        nextSceneButtonCanvas.gameObject.SetActive(true);
    }
    public void OnExitDungeonButton()//przy wygranej
    {
        PlayerManager.Instance.SaveDataXML();
        sceneLoader.HubScene();
        dungeonRooms.Clear();

    }
    public void OnLeaveDungeonButton()//przy przegranej
    {
        PlayerManager.Instance.SaveDataXML();
        sceneLoader.StartScene();
        

    }



}
