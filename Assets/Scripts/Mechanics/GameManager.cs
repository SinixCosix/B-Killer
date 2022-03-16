using System;
using System.Collections.Generic;
using Gameplay;
using Gameplay.Enemy;
using Mechanics.MapGeneration;
using Mechanics.Rooms;
using Mechanics.Spawners;
using UnityEngine;

namespace Mechanics
{
    public class GameManager : MonoBehaviour
    {
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
            _generator = GameObject.Find("MapGenerator").GetComponent<MapGenerator>();
            var spawner = GameObject.Find("Spawner");
            _mobSpawner = spawner.GetComponent<MobSpawner>();
            _wallSpawner = spawner.GetComponent<RoomWallSpawner>();
            
            
        }

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

        public void CreateMap()
        {
            Clear();
            
            _generator.Generate();
            SubscribeRooms();
            SpawnPlayer();
            
            _generator.Paint();
        }

        private void SubscribeRooms()
        {
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

            Debug.Log(Rooms.Count);
            foreach (var room in Rooms)
                Destroy(room.gameObject);
            Rooms.Clear();
        }

        private void SpawnPlayer()
        {
            Debug.Log(player);
            var room = Rooms[0];
            player.transform.position = room.transform.position;
        }
    }
}