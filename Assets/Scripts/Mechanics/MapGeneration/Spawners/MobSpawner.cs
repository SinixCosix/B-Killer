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

        // public void Spawn(List<Room> rooms)
        // {
        //     foreach (var room in rooms)
        //     {
        //         if (room.Id == 0)
        //             continue;
        //
        //         var mobsCount = room.MinLength / 3;
        //         for (var i = 0f; i < mobsCount; ++i)
        //             Spawn(room, i);
        //     }
        // }

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