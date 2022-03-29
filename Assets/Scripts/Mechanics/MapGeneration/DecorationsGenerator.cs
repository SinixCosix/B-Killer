using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics.MapGeneration
{
    public static class DecorationsGenerator 
    {
        public static void Generate()
        {
            GenerateForRooms();
            GenerateForPaths();
        }

        private static void GenerateForRooms()
        {
            var rooms = GameManager.Instance.Rooms;
            var decorations = MapGenerator.Instance.Decorations;
            
            foreach (var room in rooms)
            {
                var rect = room.Rect;
                var decorationsCount = rect.width * rect.height * 0.6;
                for (var i = 0; i < decorationsCount; ++i)
                {
                    var x = Random.Range((int) rect.x, (int) rect.xMax);
                    var y = Random.Range((int) rect.y, (int) rect.yMax);
                    decorations.Add(new Vector2Int(x, y));
                }
            }
        }

        private static void GenerateForPaths()
        {
            var decorations = MapGenerator.Instance.Decorations;
            var decorationsCount = MapGenerator.Instance.Paths.Count * 0.6;
            for (var i = 0; i < decorationsCount; ++i)
            {
                var index = Random.Range(0, MapGenerator.Instance.Paths.Count);
                var point = MapGenerator.Instance.Paths.ElementAt(index);
                decorations.Add(point);
            }
        }
    }
}