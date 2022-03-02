using System.Collections.Generic;
using Mechanics.MapGeneration;
using Mechanics.Rooms;
using UnityEngine;

namespace Mechanics.Spawners
{
    public class MobSpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;

        private readonly List<GameObject> _mobs = new List<GameObject>();

        public void Spawn(Room room)
        {
            if (room.Id == 0) return;
            
            for (var i = 0; i < room.MobsCount; i++)
            {
                var position = new Vector2(room.Rect.center.x + i,
                    room.Rect.center.y);
                var mobObject = Instantiate(enemyPrefab, position, Quaternion.identity);
                var mobHealth = mobObject.GetComponent<Health>();
                mobHealth.Death += () => --room.MobsCount;
                _mobs.Add(mobObject);
            }
        }

        public void Clear()
        {
            foreach (var mob in _mobs)
            {
                Destroy(mob);
            }

            _mobs.Clear();
        }
    }
}