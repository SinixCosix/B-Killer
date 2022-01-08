using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MapGeneration
{
    public static class LawnsGenerator
    {
        public static void Generate()
        {
            var lawns = BinarySpacePartitionTree.Split().ToList();
            ReshapeRects(lawns);
            GeneratePoints(lawns);

            MapGenerator.Instance.Lawns = lawns;
        }

        private static void ReshapeRects(IList<Rect> lawns)
        {
            for (var i = 0; i < lawns.Count; i++)
            {
                var room = lawns[i];
                room = ReshapeRect(room);
                lawns[i] = room;
            }
        }

        private static Rect ReshapeRect(Rect rect)
        {
            var gameManager = Settings.Instance;
            var minLength = Math.Min(rect.width, rect.height);
            var width = Random.Range(minLength * gameManager.splitRatioFrom, rect.width);
            if (width < gameManager.minRoomSize)
                width = gameManager.minRoomSize;
            var height = Random.Range(minLength * gameManager.splitRatioFrom, rect.height);
            if (height < gameManager.minRoomSize)
                height = gameManager.minRoomSize;
            var x = Random.Range(rect.x, rect.xMax - width);
            var y = Random.Range(rect.y, rect.yMax - height);

            return new Rect(x, y, width, height);
        }

        private static void GeneratePoints(IEnumerable<Rect> rooms)
        {
            var pointsOfLawns = MapGenerator.Instance.PointsOfLawns;
            foreach (var room in rooms)
            {
                for (var i = room.x; i < room.xMax; ++i)
                for (var j = room.y; j < room.yMax; ++j)
                {
                    var position = new Vector2Int((int) i, (int) j);
                    pointsOfLawns.Add(position);
                }
            }
        }
    }
}