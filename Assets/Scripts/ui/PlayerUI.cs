using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    private static PlayerUI _instance;

    public static PlayerUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerUI>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("PlayerUI");
                    _instance = obj.AddComponent<PlayerUI>();
                }
            }
            return _instance;
        }
    }

    public SpellUI spellUI;
    public HealthBar healthBar;
    public PointCounter pointCounter;
    

    private void Awake()
    {
        isGamePaused = false;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        ActivateGameUI();
        LockCursor();
    }

    public GameObject gameUI;
    public GameObject pauseScreen;
    public GameObject settingsScreen;
    public GameObject pixelFilter;
    public bool isGamePaused = false;

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void OpenSettingsScreen()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void CloseSettingsScreen()
    {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void OpenPauseScreen()
    {
        DeactivateGameUI();
        PauseGame();
        UnlockCursor();
    }

    public void ExitPauseScreen()
    {
        ActivateGameUI();
        ResumeGame();
        LockCursor();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
        ResumeGame();
    }

    private void ActivateGameUI()
    {
        pixelFilter.SetActive(true);
        gameUI.SetActive(true);
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    private void DeactivateGameUI()
    {
        pixelFilter.SetActive(false);
        gameUI.SetActive(false);
        pauseScreen.SetActive(true);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
