using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class GameOverMenu : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }

        public void LoadMainMenu()
        { 
            SceneManager.LoadScene("MainMenu");
        }
    }
}