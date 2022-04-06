using Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class PauseMenu : Menu
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (GameManager.IsGamePaused)
                Resume();
            else
                Pause();
        }

        public void LoadMainMenu()
        {
            Resume();
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            GameManager.Quit();
        }
    }
}
