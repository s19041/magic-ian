using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonCreator :MonoBehaviour
{

    private int diff;//my�l� �e wst�pinie 0,1,2,3
    private int length;//chyba bez limitu
    public static DungeonCreator Instance;
    Room[] dungeonRooms;
    //normal
    [SerializeField] GameObject pigeonPrefab;
    [SerializeField] GameObject rabbitPrefab;
    [SerializeField] GameObject princePrefab;
    //elite
    [SerializeField] GameObject activistPrefab;
    [SerializeField] GameObject knowerPrefab;
    [SerializeField] GameObject assistantPrefab;
    //bosses
    [SerializeField] GameObject leruaPrefab;
    [SerializeField] GameObject groubiniPrefab;
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
        dungeonRooms = new Room[7 + length*2];
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
        bool eliteRoom = false;
        dungeonRooms[0] = ScriptableObject.CreateInstance<EntranceRoom>();
        for (int i = 1; i <= dungeonRooms.Length-2; i++)
        {
            if (i % 2 == 1&&i!=dungeonRooms.Length-1)
            {
                dungeonRooms[i] = CombatRoomCreator();
                if(i % 3 == 0 && !eliteRoom)
                {
                    dungeonRooms[i] = EliteCombatRoomCreator();
                    eliteRoom = true;
                }
            }
            else
            {
                dungeonRooms[i] = NeutralRoomCreator();
            }
        }
        dungeonRooms[dungeonRooms.Length - 2] = BossRoomCreator();
        dungeonRooms[dungeonRooms.Length - 1] = TreasureRoomCreator();
    }

    private CombatRoom CombatRoomCreator()//napisa� tu jak�� fajn� proceduralke
    {
        CombatRoom combatRoom = ScriptableObject.CreateInstance<CombatRoom>();
        
        int roomDifficulty;
        roomDifficulty = 1;
        /*
        //tutaj zrobi� jeszcze wyb�r przeciwnika
        if (diff != 0)
        {
            roomDifficulty++;
        }
        double d  = Random.value;
        if (d > 0.7)//szansza na trudniejszy pok�j
        {
            roomDifficulty++;
        }
        */
        OpponentPicker(roomDifficulty, combatRoom);
          
        
        combatRoom.goldReward = 75;
        return combatRoom;
    }
    private void OpponentPicker(int roomDifficulty, CombatRoom _combatRoom)
    {



        //mysl� ze tutaj trzeba jakie� ustawienia przeciwnik�w zrobi� (w sensie rozne walki)
        //np 3 go��bie
        //albo 2 kr�liki czy cos

        //np. tak
        List<GameObject> opponentLayout = new List<GameObject>();
        double d = Random.value;
        
        if (d <0.25)
        {
            opponentLayout.Add(rabbitPrefab);
            _combatRoom.encounterName = "The Killer Rabbit";
        }
        else if (d < 0.50)
        {
            opponentLayout.Add(rabbitPrefab);
            opponentLayout.Add(pigeonPrefab);
            _combatRoom.encounterName = "The Dynamic duo";
        }
        else if (d < 0.75)
        {
            opponentLayout.Add(princePrefab);
            _combatRoom.encounterName = "The Frog Prince";
        }
        else 
        {
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(pigeonPrefab);
            _combatRoom.encounterName = "Angry pigeons";
        }

      

        AdjustOpponentsDifficulty(opponentLayout);


        
        foreach (GameObject opponent in opponentLayout)
        {
            _combatRoom.AddOponent(opponent);
        }


    }
    private CombatRoom EliteCombatRoomCreator()//napisa� tu jak�� fajn� proceduralke
    {
        CombatRoom combatRoom = ScriptableObject.CreateInstance<CombatRoom>();

        int roomDifficulty;
        roomDifficulty = 1;
        /*
        //tutaj zrobi� jeszcze wyb�r przeciwnika
        if (diff != 0)
        {
            roomDifficulty++;
        }
        double d  = Random.value;
        if (d > 0.7)//szansza na trudniejszy pok�j
        {
            roomDifficulty++;
        }
        */
        EliteOpponentPicker(roomDifficulty, combatRoom);


        combatRoom.goldReward = 125;
        return combatRoom;
    }
    private void EliteOpponentPicker(int roomDifficulty, CombatRoom _combatRoom)
    {



        //mysl� ze tutaj trzeba jakie� ustawienia przeciwnik�w zrobi� (w sensie rozne walki)
        //np 3 go��bie
        //albo 2 kr�liki czy cos

        //np. tak
        List<GameObject> opponentLayout = new List<GameObject>();
        double d = Random.value;
        if (d >0.99)
        {
            opponentLayout.Add(assistantPrefab);
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(rabbitPrefab);
            _combatRoom.encounterName = "The unlucky Trio";
        }
        else if (d > 0.6)
        {
            opponentLayout.Add(assistantPrefab);

            _combatRoom.encounterName = "The unpaid one";
        }
        else if(d > 0.3)
        {
            opponentLayout.Add(rabbitPrefab);
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(activistPrefab);

            _combatRoom.encounterName = "The hypocrite";
        }
        else
        {
            opponentLayout.Add(knowerPrefab);

            _combatRoom.encounterName = "The know it all";
        }
    



        AdjustOpponentsDifficulty(opponentLayout);



        foreach (GameObject opponent in opponentLayout)
        {
            _combatRoom.AddOponent(opponent);
        }


    }
    private Room NeutralRoomCreator()//napisa� tu jak�� fajn� proceduralke
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
        BossRoom bossRoom = ScriptableObject.CreateInstance<BossRoom>();
        double d = Random.value;
        if (d > 1)
        {
            bossRoom.AddOponent(leruaPrefab);
            bossRoom.encounterName = "Lerua Merlin";
        }
        else
        {
            bossRoom.AddOponent(groubiniPrefab);
            bossRoom.encounterName = "Larry Groubini";
        }
        
        return bossRoom;
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
        TreasureRoom treasureRoom = ScriptableObject.CreateInstance<TreasureRoom>();
        return treasureRoom;
    }

    
}
