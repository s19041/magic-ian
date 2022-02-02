using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom : Room
{
    List<GameObject> opponents;
    public int goldReward;

    public CombatRoom() : base(Type.COMBAT)
    {
        opponents = new List<GameObject>();
        
    }
    public void AddOponent(GameObject opponent)
    {
        if (opponents.Count > 4)
        {
            return;
        }
        opponents.Add(opponent);
    }
    public List<GameObject> GetOpponents()
    {
        List<GameObject> tmp = new List<GameObject>();
        tmp.AddRange(opponents);
        return tmp;
    }
    
    // Update is called once per frame
    
}
