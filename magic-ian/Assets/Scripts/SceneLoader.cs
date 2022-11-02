using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;

    public static SceneLoader Instance
    { get { return _instance; } }

    public void Awake()
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
    }

    public void LoadRoom(Room room)
    {
        switch (room.type)
        {
            case Type.ENTRANCE:
                EntranceScene();
                DungeonManager.Instance.EnableNextSceneButton();
                break;

            case Type.COMBAT:

                CombatScene();
                break;

            case Type.BOSS:

                BossScene();
                break;

            case Type.SECRET:

                SecretScene();
                break;

            case Type.TRADER:

                TraderScene();
                break;

            case Type.TREASURE:

                TreasureScene();
                break;

            default:
                break;
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void HubScene()
    {
        SceneManager.LoadScene(1);
    }

    public void EntranceScene()
    {
        SceneManager.LoadScene(2);
    }

    public void CombatScene()
    {
        SceneManager.LoadScene(3);
    }

    public void SecretScene()
    {
        throw new NotImplementedException();
    }

    public void TreasureScene()
    {
        SceneManager.LoadScene(6);
    }

    public void TraderScene()
    {
        SceneManager.LoadScene(5);
    }

    public void BossScene()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}