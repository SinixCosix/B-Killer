using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }

        public void QuitGame()
        {
            Debug.Log("Quit game");
            Application.Quit();
        }
        
    }
}
