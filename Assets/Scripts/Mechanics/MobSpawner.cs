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

        public void Spawn(List<Rect> rooms)
        {
            foreach (var room in rooms)
            {
                if (room.center == MapGenerator.Instance.StartPoint)
                    continue;

                var mobsCount = Mathf.Min(room.width, room.height);
                mobsCount /= 3;
                Debug.Log($"mobs count at room: {mobsCount}");
                for (var i = 0f; i < mobsCount; ++i)
                {
                    var position = new Vector2(room.center.x + i, room.center.y);
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