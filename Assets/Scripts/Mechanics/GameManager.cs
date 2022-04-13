using System;
using System.Collections.Generic;
using Gameplay;
using Gameplay.Enemies;
using Mechanics.MapGeneration;
using Mechanics.Rooms;
using Mechanics.Spawners;
using UnityEngine;

namespace Mechanics
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsGamePaused { get; private set; }
        public static GameManager Instance;

        public TilemapPainter painter;
        public PlayerController player;


        private MobSpawner _mobSpawner;
        private RoomWallSpawner _wallSpawner;
        private MapGenerator _generator;

        public List<Room> Rooms { get; set; }

        private float _time;

        public GameManager()
            => Instance = this;

        private void Awake()
        {
            Debug.Log("awake");
            _generator = GameObject.Find("MapGenerator").GetComponent<MapGenerator>();
            var spawner = GameObject.Find("Spawner");
            _mobSpawner = spawner.GetComponent<MobSpawner>();
            _wallSpawner = spawner.GetComponent<RoomWallSpawner>();
        }

        private void Start()
        {
            Debug.Log("start");
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

        public void CreateMap()
        {
            Clear();
            _generator.Generate();
            SubscribeRooms();
            SpawnPlayer();
            
            _generator.Paint();
        }

        private void Subscribe()
        {
            // player.
            SubscribeRooms();
        }

        private void SubscribeRooms()
        {
            if (Rooms == null) return;
            foreach (var room in Rooms)
            {
                room.PlayerTriggered += _mobSpawner.Spawn;
                room.PlayerTriggered += _wallSpawner.Spawn;
                room.PlayerTriggered += AiManager.Recalculate;
                room.MobsAreKilled += _wallSpawner.Despawn;
            }
        }

        private void Clear()
        {
            _generator.Clear();
            _mobSpawner.Clear();
            painter.Clear();
            ClearRooms();
        }

        private void ClearRooms()
        {
            if (Rooms == null) return;

            foreach (var room in Rooms)
                Destroy(room.gameObject);
            Rooms.Clear();
        }

        private void SpawnPlayer()
        {
            var room = Rooms[0];
            player.transform.position = room.transform.position;
        }

        public static void Pause()
        {
            Time.timeScale = 0f;
            IsGamePaused = true;
        }

        public static void Resume()
        {
            Time.timeScale = 1f;
            IsGamePaused = false;
        }

        public static void Quit()
        {
            Debug.Log("Quitting game...");
            Application.Quit();
        }
    }
}