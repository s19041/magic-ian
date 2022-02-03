using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Type { ENTRANCE,COMBAT,TRADER,TREASURE,BOSS,SECRET }

//[CreateAssetMenu(fileName = "new Room", menuName = "Room")]
public abstract class Room :ScriptableObject
{
    public Type type;
    public Room(Type _type)
    {
        type = _type;
    }

}
