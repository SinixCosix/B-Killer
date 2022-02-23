using System.Collections.Generic;
using Gameplay;
using Mechanics.MapGeneration;
using UnityEngine;

namespace Mechanics
{
    public class MobSpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;

        private readonly List<GameObject> _mobs = new List<GameObject>();

        public void Spawn(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                if (room.Rect.center == MapGenerator.Instance.StartPoint)
                    continue;

                var mobsCount = room.MinLength / 3;
                for (var i = 0f; i < mobsCount; ++i)
                {
                    var position = new Vector2(room.Rect.center.x + i,
                                                 room.Rect.center.y);
                    var mob = Instantiate(enemyPrefab, position, Quaternion.identity);
                    _mobs.Add(mob);
                }
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