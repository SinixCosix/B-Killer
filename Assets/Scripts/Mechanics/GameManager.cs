using Gameplay;
using Mechanics.MapGeneration;
using UnityEngine;

namespace Mechanics
{
    public class GameManager : MonoBehaviour
    {
        public MobSpawner mobSpawner;
        public MapGenerator generator;
        public PlayerController player;

        private float _time;

        private void Start()
        {
            _time = Settings.Instance.time;
            CreateMap();
        }

        private void Update()
        {
            if (!Settings.Instance.infinityGeneration)
                return;

            if (_time < 0)
            {
                _time = Settings.Instance.time;
                CreateMap();
            }
            else
                _time -= Time.deltaTime;
        }

        private void CreateMap()
        {
            generator.Generate();
            SpawnPlayer();
            mobSpawner.Spawn(generator.Lawns);
        }

        private void SpawnPlayer()
        {
            Debug.Log(player);
            generator.SelectStartPoint();
            player.Respawn();
        }
    }
}