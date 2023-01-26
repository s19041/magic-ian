using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private static PlayerManager _instance;

    public static PlayerManager Instance
    { get { return _instance; } }

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
    }

    public void NewGame()
    {
        playerData.NewGame();
        SaveDataXML();
        GameObject.Destroy(DeckBuilder.Instance.gameObject);

        SceneLoader.Instance.HubScene();
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
        return playerData.GetUnlockedCards();
    }

    public List<Item> GetUnlockedItems()
    {
        return playerData.GetUnlockedItems();
    }

    public void LoadDataXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream stream = new FileStream(System.IO.Directory.GetCurrentDirectory() + "/Saves/save.xml", FileMode.Open);

        PlayerData tmp = serializer.Deserialize(stream) as PlayerData;
        if (tmp != null)
        {
            playerData = tmp;
        }

        stream.Close();
        playerData.Deserialize();
    }

    public void SaveDataXML()
    {
        playerData.Serialize();
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream stream = new FileStream(System.IO.Directory.GetCurrentDirectory() + "/Saves/save.xml", FileMode.Create);
        serializer.Serialize(stream, playerData);
        stream.Close();
    }

    public int GetRuns()
    {
        return playerData.runs;
    }

    public int GetGold()
    {
        return playerData.gold;
    }

    public void AddGold(int amount)
    {
        playerData.gold += amount;
    }

    public void RemoveGold(int amount)
    {
        playerData.gold -= amount;
    }

    public void IncrementRuns()
    {
        playerData.runs++;
    }

    public void SuccesfulRun()
    {
        playerData.succesfulRuns++;
    }

    public void UnlockItem(Item item)
    {
        playerData.UnlockItem(item);
    }

    public string UnlockNextUnlockables()
    {
        DeckBuilder db = DeckBuilder.Instance;
        int succesfulRuns = playerData.succesfulRuns;
        if (succesfulRuns > 5)
        {
            return "To_Be_Implemented";
        }
        if (succesfulRuns == 0) //serca i pik 2,8,9
        {
            UnlockCard(db.hearts[1]);
            UnlockCard(db.hearts[7]);
            UnlockCard(db.hearts[8]);

            UnlockCard(db.spades[1]);
            UnlockCard(db.spades[7]);
            UnlockCard(db.spades[8]);
            return "1,7,8 of Hearts and Spades";
        }
        else if (succesfulRuns == 1)//serca i pik, 1,10
        {
            UnlockCard(db.hearts[0]);
            UnlockCard(db.hearts[9]);

            UnlockCard(db.spades[0]);
            UnlockCard(db.spades[9]);
            return "0,9 Hearts and Spades";
        }
        else if (succesfulRuns == 2)//deck kier(1-10)
        {
            for (int i = 0; i <= 9; i++)
            {
                UnlockCard(db.clubs[0]);
            }
            return "Deck of Hearts";
        }
        else if (succesfulRuns == 3)//item(Sleeve)
        {
            playerData.UnlockItem(db.items[1]);
            return "Magical Sleeve";
        }
        else if (succesfulRuns == 4)//dama serce i pik
        {
            UnlockCard(db.hearts[11]);

            UnlockCard(db.spades[11]);
            return "Queen of Hearts and Queen of Spades";
        }
        else if (succesfulRuns == 5)//walet serce i pik
        {
            UnlockCard(db.hearts[12]);

            UnlockCard(db.spades[12]);
            return "Jack of Hearts and Jack of Spades";
        }
        return "nothing";
    }
}