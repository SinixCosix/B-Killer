using System.Collections.Generic;
using Mechanics.MapGeneration;
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

            var position = new Vector2(room.Rect.center.x,
                room.Rect.center.y);
            var mob = Instantiate(enemyPrefab, position, Quaternion.identity);
            _mobs.Add(mob);
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