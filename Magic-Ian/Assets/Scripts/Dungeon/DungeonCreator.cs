using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator 
{

    int diff;
    int length;
    Room[] dungeonRooms;
    GameObject pigeonPrefab;
    GameObject evilMagicianPrefab;
    //dungeon creation variables



    public DungeonCreator(int _diff, int _length)
    {
        diff = _diff;
        length = _length;
        dungeonRooms =new Room[5 + length];
    }
    
    public void CreateDungeon()
    {
        dungeonRooms[0] = new EntranceRoom();
        for (int i = 1; i <= dungeonRooms.Length; i++)
        {
            if (i % 2 == 1%%i!=dungeonRooms.Length-1)
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

    private CombatRoom CombatRoomCreator()
    {
        CombatRoom combatRoom = new CombatRoom();
        int roomDifficulty;
        roomDifficulty = 1;
        //tutaj zrobi� jeszcze wyb�r przeciwnika
        if (diff != 0)
        {
            roomDifficulty++;
        }
        float d  = Random.value;
        if (d > 0.3)
        {
            roomDifficulty++;
            if (d>0.8)
                roomDifficulty++;
        }
        List<GameObject> opps=OpponentPicker(roomDifficulty)
            foreach(GameObject opponent in opps)
        {
            combatRoom.AddOponent(opponent);
        }
        
        combatRoom.goldReward = roomDifficulty;
        return new CombatRoom();
    }
    private List<GameObject> OpponentPicker(int roomDifficulty)
    {



        //mysl� ze tutaj trzeba jakie� ustawienia przeciwnik�w zrobi�
        //np 3 go��bie
        //albo 2 kr�liki czy cos

        //np. tak
        List<GameObject> opponentLayout = new List<GameObject>();
        if (true)
        {
            
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(pigeonPrefab);
            opponentLayout.Add(evilMagicianPrefab);
        }
        else
        {

        }
        
       
        return opponentLayout;
    }
    private Room NeutralRoomCreator()
    {
        Room room = TraderRoomCreator();
        
        
        
        float d = Random.value;
        if (d > 0.7)
        {
            room = TreasureRoomCreator();
            if (d > 0.95)
                room = SecretRoomCreator();
        }
        return room;

        
    }
    private BossRoom BossRoomCreator()
    {

    }
   
    private TraderRoom TraderRoomCreator()
    {

    }
    private SecretRoom SecretRoomCreator()
    {

    }
    private TreasureRoom TreasureRoomCreator()
    {

    }
    private void SetDungeonBalance()
    {

    }
}
