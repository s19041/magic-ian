using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;


    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
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

        playerData = new PlayerData();
        LoadDataXML();
    }
    public bool UnlockCard(Card card)
    {
        return playerData.UnlockCard(card);

    }
    public bool UnlockSuit(Suit suit)
    {
        return playerData.UnlockSuit(suit);
    }
    public List<Card> GetUnlockedCards()
    {
        return playerData.unlockedCards;
    }
    public void LoadDataXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream stream = new FileStream(Application.dataPath + "/../Saves/save.xml", FileMode.Open);

        PlayerData tmp = serializer.Deserialize(stream) as PlayerData;
        if (tmp != null)
        {
            playerData = tmp;
        }

        stream.Close();

    }
    public void SaveDataXML()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream stream = new FileStream(Application.dataPath + "/../Saves/save.xml", FileMode.Create);
        serializer.Serialize(stream, playerData);
        stream.Close();
    }

}
