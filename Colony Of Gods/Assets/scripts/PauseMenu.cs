using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;  // << add this

public class PauseMenu : MonoBehaviour
{
    [Header("UI (assign in inspector)")]
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject darkOverlay;

    [Header("Optional")]
    public GameObject hudPauseButton;
    public GameObject player;

    [Header("Scene names")]
    public string mainMenuSceneName = "MainMenu";

    bool isPaused = false;

    void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (darkOverlay) darkOverlay.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        // Escape key to toggle pause
        if (kb.escapeKey.wasPressedThisFrame)
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

        Time.timeScale = 0f;
        AudioListener.pause = true;

        if (player) player.SendMessage("OnPause", SendMessageOptions.DontRequireReceiver);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        isPaused = false;

        if (pausePanel) pausePanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (darkOverlay) darkOverlay.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (player) player.SendMessage("OnResume", SendMessageOptions.DontRequireReceiver);
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
        Time.timeScale = 1f;
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
