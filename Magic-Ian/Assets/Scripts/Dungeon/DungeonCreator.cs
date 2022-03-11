using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator :MonoBehaviour
{

    private int diff;//myœlê ¿e wstêpinie 0,1,2,3
    private int length;//chyba bez limitu
    public static DungeonCreator Instance;
    Room[] dungeonRooms;
    [SerializeField] GameObject pigeonPrefab;
    //[SerializeField] GameObject evilMagicianPrefab;
    private double secretRoomChance;
    private double treasureRoomChance;

    //dungeon creation variables



    public void Awake()
    {
        Instance = this;
    }
    public void SetupCreator(int _diff, int _length)
    {
        diff = _diff;
        length = _length;
        dungeonRooms = new Room[5 + length*2];
        SetDungeonBalance();
    }
    public Room[] GetDungeon()
    {
        return dungeonRooms;
    }
    private void SetDungeonBalance()
    {
        secretRoomChance = 0.95;
        treasureRoomChance = 0.5;
    }
    private void AdjustOpponentsDifficulty(List<GameObject> opponents)
    {
        foreach(GameObject opponentObject in opponents)
        {
            if (diff > 0)
                opponentObject.GetComponent<Unit>().maxHp += (int)(opponentObject.GetComponent<Unit>().maxHp / 5);
            if(diff>1)
                opponentObject.GetComponent<Unit>().damage += (int)(opponentObject.GetComponent<Unit>().damage / 4);
            if (diff > 2)
                opponentObject.GetComponent<Unit>().armor += 10;
        }
        
    }

    public void CreateDungeon()
    {
        dungeonRooms[0] = ScriptableObject.CreateInstance<EntranceRoom>();
        for (int i = 1; i <= dungeonRooms.Length-2; i++)
        {
            if (i % 2 == 1&&i!=dungeonRooms.Length-1)
            {
                dungeonRooms[i] = CombatRoomCreator();
            }
            else
            {
                dungeonRooms[i] = NeutralRoomCreator();
            }
        }
        dungeonRooms[dungeonRooms.Length - 1] = BossRoomCreator();
    }

    private CombatRoom CombatRoomCreator()//napisaæ tu jak¹œ fajn¹ proceduralke
    {
        CombatRoom combatRoom = ScriptableObject.CreateInstance<CombatRoom>();
        int roomDifficulty;
        roomDifficulty = 1;
        //tutaj zrobiæ jeszcze wybór przeciwnika
        if (diff != 0)
        {
            roomDifficulty++;
        }
        double d  = Random.value;
        if (d > 0.7)//szansza na trudniejszy pokój
        {
            roomDifficulty++;
        }
        OpponentPicker(roomDifficulty, combatRoom);
          
        
        combatRoom.goldReward = roomDifficulty;
        return combatRoom;
    }
    private void OpponentPicker(int roomDifficulty, CombatRoom _combatRoom)
    {



        //myslê ze tutaj trzeba jakieœ ustawienia przeciwników zrobiæ (w sensie rozne walki)
        //np 3 go³êbie
        //albo 2 króliki czy cos

        //np. tak
        List<GameObject> opponentLayout = new List<GameObject>();
        if (true)
        {
            
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(pigeonPrefab);
            _combatRoom.encounterName = "Mad pigeons";
            
        }
        else
        {
            //opponentLayout.Add(evilMagicianPrefab);
            //opponentLayout.Add(evilMagicianPrefab);
        }
        

        AdjustOpponentsDifficulty(opponentLayout);


        
        foreach (GameObject opponent in opponentLayout)
        {
            _combatRoom.AddOponent(opponent);
        }


    }
    private Room NeutralRoomCreator()//napisaæ tu jak¹œ fajn¹ proceduralke
    {
        Room room = TraderRoomCreator();
        
        
        
        double d = Random.value;
        if (d > 1-treasureRoomChance+secretRoomChance)
        {
            room = TreasureRoomCreator();
            if (d > 1-secretRoomChance)
                room = SecretRoomCreator();
        }
        return room;

        
    }
    private BossRoom BossRoomCreator()
    {
        return ScriptableObject.CreateInstance<BossRoom>();
    }
   
    private TraderRoom TraderRoomCreator()
    {
        return ScriptableObject.CreateInstance<TraderRoom>();
    }
    private SecretRoom SecretRoomCreator()
    {
        return ScriptableObject.CreateInstance<SecretRoom>();
    }
    private TreasureRoom TreasureRoomCreator()
    {
        return ScriptableObject.CreateInstance<TreasureRoom>();
    }
    
}
