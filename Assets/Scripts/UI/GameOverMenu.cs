using System;
using Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class GameOverMenu : Menu
    {
        private void Awake()
        {
            GameManager.Instance.player.player.health.Death += Pause;
        }

        public void Restart()
        {
            Resume();
            GameManager.Instance.CreateMap();
        }
        
        public void LoadMainMenu()
        {
            Resume();
            SceneManager.LoadScene("MainMenu");
        }
    }
}