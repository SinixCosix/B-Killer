using Gameplay;
using Mechanics.MapGeneration;
using UnityEngine;

namespace Mechanics
{
    public class GameManager : MonoBehaviour
    {
        public MapGenerator generator;
        public PlayerController player;
        
        private float _time;

        private void Start()
        {
            _time = Settings.Instance.time;
            generator.Generate();
            SpawnPlayer();
        }

        private void Update()
        {
            if (!Settings.Instance.infinityGeneration)
                return;

            if (_time < 0)
            {
                _time = Settings.Instance.time;

                generator.Generate();
                SpawnPlayer();
            }
            else
                _time -= Time.deltaTime;
        }

        private void SpawnPlayer()
        {
            Debug.Log(player);
            player.SpawnPoint = generator.SelectStartPoint();
            player.Respawn();
        }
    }
}