using System;
using System.Collections.Generic;
using Gameplay;
using Mechanics.MapGeneration;
using Mechanics.Rooms;
using Mechanics.Spawners;
using UnityEngine;

namespace Mechanics
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public MobSpawner mobSpawner;
        public MapGenerator generator;
        public PlayerController player;
        public RoomWallSpawner wallSpawner;
        public TilemapPainter painter;
        
        public List<Room> Rooms { get; set; }

        private float _time;
        
        public GameManager()
            => Instance = this;

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
            Clear();
            
            generator.Generate();
            SubscribeRooms();
            SpawnPlayer();
            
            generator.Paint();
        }

        private void SubscribeRooms()
        {
            foreach (var room in Rooms)
            {
                room.PlayerTriggered += mobSpawner.Spawn;
                room.PlayerTriggered += wallSpawner.Spawn;
                room.MobsAreKilled += wallSpawner.Despawn;
            }
        }

        private void Clear()
        {
            generator.Clear();
            mobSpawner.Clear();
            Rooms?.Clear();
            painter.Clear();
        }

        private void SpawnPlayer()
        {
            Debug.Log(player);
            player.Respawn();
        }
    }
}