using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : CombatRoom
{
    
    int goldReward;
    //tutaj jakie� pole na nagrodd�
    //my�le �e klasa treasure kt�ra mo�e odblokowa� item/karty
    //Object reward;
    
    public BossRoom()
    {
        type = Type.BOSS;
    }

    
}
