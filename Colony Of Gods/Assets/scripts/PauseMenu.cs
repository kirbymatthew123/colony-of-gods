using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI (assign in inspector)")]
    public GameObject pausePanel;      // big pause menu panel (set inactive in inspector)
    public GameObject settingsPanel;   // settings panel inside pause menu (optional)
    public GameObject darkOverlay;     // dim background image (optional)

    [Header("Optional")]
    public GameObject hudPauseButton;  // small pause button in HUD (optional)
    public GameObject player;          // optional: used to notify player scripts (SendMessage)

    [Header("Scene names")]
    public string mainMenuSceneName = "MainMenu"; // change to your main menu scene name

    bool isPaused = false;

    void Start()
    {
        // make sure UI is hidden at start and game is unpaused
        if (pausePanel) pausePanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (darkOverlay) darkOverlay.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    void Update()
    {
        // allow Escape key to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;

        if (pausePanel) pausePanel.SetActive(true);
        if (darkOverlay) darkOverlay.SetActive(true);
        if (settingsPanel) settingsPanel.SetActive(false);

        Time.timeScale = 0f;            // freeze game time
        AudioListener.pause = true;     // pause audio

        // optional: notify player scripts to stop input (they can implement OnPause/OnResume)
        if (player) player.SendMessage("OnPause", SendMessageOptions.DontRequireReceiver);

        // make cursor visible if you hide it during play
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        // Always force resume
        isPaused = false;

        if (pausePanel) pausePanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (darkOverlay) darkOverlay.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (player) player.SendMessage("OnResume", SendMessageOptions.DontRequireReceiver);

        // restore cursor state to what your game uses during play
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleSettings()
    {
        if (settingsPanel == null || pausePanel == null) return;
        bool opening = !settingsPanel.activeSelf;
        settingsPanel.SetActive(opening);
        pausePanel.SetActive(!opening);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;         // unpause before loading
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
