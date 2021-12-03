using Gameplay;
using UnityEngine;

namespace Mechanics
{
    public class GameManager : MonoBehaviour
    {
        public LevelGenerator generator;
        public PlayerController player;
        public float time = 2;
        public bool infinityGeneration;

        private float _time;

        private void Start()
        {
            _time = time;
            generator.Generate();
            SpawnPlayer();
        }

        private void Update()
        {
            if (!infinityGeneration)
                return;

            if (_time < 0)
            {
                _time = time;

                generator.Generate();
                SpawnPlayer();
            }
            else
                _time -= Time.deltaTime;
        }

        private void SpawnPlayer()
        {
            Debug.Log(player);
            player.SpawnPoint = generator.StartPoint;
            player.Respawn();
        }
    }
}