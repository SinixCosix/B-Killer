using Mechanics;
using UnityEngine;

namespace Ui
{
    public class Menu : MonoBehaviour
    {
        public GameObject panel;
        
        public void Pause()
        {
            panel.SetActive(true);
            GameManager.Pause();
        }

        public void Resume()
        {
            panel.SetActive(false);
            GameManager.Resume();
        }
    }
}