using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        { 
            Time.timeScale = 1f;
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
            Debug.Log("Quitting game...");
            Application.Quit();
        }
        
    }
}
