using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Type { ENTRANCE,COMBAT,TRADER,TREASURE,BOSS,SECRET }
public abstract class Room 
{
    public Type type;
    public Room(Type _type)
    {
        type = _type;
    }

}
