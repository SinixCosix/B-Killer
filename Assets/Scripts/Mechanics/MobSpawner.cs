using System.Collections.Generic;
using Gameplay;
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
                // var mobsCount = (int)room.width / 3;
                var mob = Instantiate(enemyPrefab, room.center, Quaternion.identity);
                _mobs.Add(mob);
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