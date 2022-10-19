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
    Button nextSceneButtonCanvas;
    [SerializeField]
    Button anotherCombatButtonCanvas;
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
        if (currentRoomIndex>=dungeonRooms.Count)
        {
            
            nextSceneButtonCanvas.gameObject.SetActive(false);
            OnWinDungeonButton();
            return;
        }
        
        sceneLoader.LoadRoom(dungeonRooms[currentRoomIndex]);
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
            {
                DeckBuilder.Instance.savedDeckCards.AddRange(Deck.Instance.GetCards());
                OnLoadNextRoomButton();
                PlayerManager.Instance.IncrementRuns();
                PlayerManager.Instance.AddGold(100);
            }
                

        }
        else
        {
            //tutaj zrobiæ jakiœ popup ¿e niewystarczaj¹ca iloœæ kart
            Debug.Log("Niewystarczaj¹ca iloœæ kart w decku do wystartowania dungeona");
        }
        
    }
    public void EnableNextSceneButton()
    {
        if (GetCurrentRoom().type == Type.COMBAT)
            anotherCombatButtonCanvas.gameObject.SetActive(true);
        nextSceneButtonCanvas.gameObject.SetActive(true);
    }
    public void DisableNextSceneButton()
    {
        anotherCombatButtonCanvas.gameObject.SetActive(false);
        nextSceneButtonCanvas.gameObject.SetActive(false);
        
    }
    public void OnWinDungeonButton()//przy wygranej
    {
        PlayerManager.Instance.SuccesfulRun();
        EndRun();
       
    }
    public void OnLeaveDungeonButton()//przy przegranej
    {
        EndRun();
        

    }
    private void EndRun()
    {
        PlayerManager.Instance.SaveDataXML();
        Destroy(PlayerManager.Instance);
        Destroy(GameObject.Find("Ian"));
        dungeonRooms.Clear();
        currentRoomIndex = -1;
        sceneLoader.HubScene();
        DeckBuilder.Instance.ResetDeckAfterRun();
    }
    public void AnotherCombatRoom()
    {
        if (GetCurrentRoom().type == Type.COMBAT)
        {
            CombatRoom cr = DungeonCreator.Instance.CreateAnotherFightRoom();
            cr.goldReward += (int)(GetCurrentCombatRoom().goldReward / 4);
            dungeonRooms[currentRoomIndex] = cr;
            sceneLoader.LoadRoom(dungeonRooms[currentRoomIndex]);
        }
            
    }
  



}
