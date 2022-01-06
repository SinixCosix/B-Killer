using Gameplay;
using Mechanics.MapGeneration;
using UnityEngine;

namespace Mechanics
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public MapGenerator generator;
        public PlayerController player;
        public float time = 2;
        public bool infinityGeneration;
        public uint mapSize = 100;
        public uint splitCount = 4;

        public float splitRatio = 0.25f;
        public float _splitRatio;

        public uint minRoomSize = 6;

        private float _time;

        public GameManager()
        {
            Instance = this;
        }

        private void Awake()
        {
            _splitRatio = 1 - splitRatio;
        }

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