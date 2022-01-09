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
            foreach (var rect in rooms)
            {
                var ellipsePoints = GenerateEllipse(rect);

                foreach (var point in ellipsePoints)
                    pointsOfLawns.Add(point);
            }
        }

        private static HashSet<Vector2Int> GenerateEllipse(Rect rect)
        {
            var points = new HashSet<Vector2Int>();
            const float increment = 0.5f;
            for (var x = rect.x; x < rect.xMax; x += increment)
            for (var y = rect.y; y < rect.yMax; y += increment)
            {
                var result = CalculateEllipsePoint(
                    x, y,
                    rect.center.x, rect.center.y,
                    rect.width/ 2, rect.height / 2);

                if (result < 1)
                    points.Add(new Vector2Int((int) x, (int) y));
            }

            return points;
        }

        private static float CalculateEllipsePoint(
            float x, float y,
            float x0, float y0,
            float a, float b)
        {
            var term1 = Mathf.Pow(x - x0, 2) / (a * a);
            var term2 = Mathf.Pow(y - y0, 2) / (b * b);

            return term1 + term2;
        }
    }
}