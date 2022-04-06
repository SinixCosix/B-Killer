using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool IsGamePaused;

        public GameObject pauseMenuUi;
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (IsGamePaused)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            pauseMenuUi.SetActive(false);
            Time.timeScale = 1f;
            IsGamePaused = false;       
        }

        private void Pause()
        {
            pauseMenuUi.SetActive(true);
            Time.timeScale = 0f;
            IsGamePaused = true;
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            Debug.Log("Quitting game...");
            Application.Quit();
        }
    }
}
