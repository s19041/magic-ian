using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool Paused = false;

    public GameObject pauseMenuCanvas;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void MainMenu()
    {
        if (MainCharacter.Instance != null)
            Destroy(MainCharacter.Instance.gameObject);
        if (PlayerManager.Instance != null)
            Destroy(PlayerManager.Instance.gameObject);
        if (DeckBuilder.Instance != null)
            Destroy(DeckBuilder.Instance.gameObject);
        if (PlayerManager.Instance != null)
            Destroy(PlayerManager.Instance.gameObject);
        if (UnitUiPopupsManager.Instance != null)
            Destroy(UnitUiPopupsManager.Instance.gameObject);
        if (SceneLoader.Instance != null)
            Destroy(SceneLoader.Instance.gameObject);
        if (DungeonManager.Instance != null)
            Destroy(DungeonManager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
}