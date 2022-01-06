using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MapGeneration
{
    public class LawnGenerator : IGenerator
    {
        public List<Rect> Lawns { get; private set; }
        public HashSet<Vector2Int> PointsOfLawns { get; }

        public LawnGenerator()
        {
            PointsOfLawns = new HashSet<Vector2Int>();
        }

        public void Generate()
        {
            var bspTree = new BinarySpacePartitionTree();
            Lawns = BinarySpacePartitionTree.Split().ToList();

            for (var i = 0; i < Lawns.Count; i++)
            {
                var room = Lawns[i];
                room = ReshapeRect(room);
                Lawns[i] = room;
            }

            Generate(Lawns);
        }

        private static Rect ReshapeRect(Rect rect)
        {
            var gameManager = GameManager.Instance;
            var minLength = Math.Min(rect.width, rect.height);
            var width = Random.Range(minLength * gameManager.splitRatio, rect.width);
            if (width < gameManager.minRoomSize)
                width = gameManager.minRoomSize;
            var height = Random.Range(minLength * gameManager.splitRatio, rect.height);
            if (height < gameManager.minRoomSize)
                height = gameManager.minRoomSize;
            var x = Random.Range(rect.x, rect.xMax - width);
            var y = Random.Range(rect.y, rect.yMax - height);

            return new Rect(x, y, width, height);
        }

        private void Generate(IEnumerable<Rect> rooms)
        {
            foreach (var room in rooms)
            {
                for (var i = room.x; i < room.xMax; ++i)
                for (var j = room.y; j < room.yMax; ++j)
                {
                    var position = new Vector2Int((int) i, (int) j);
                    PointsOfLawns.Add(position);
                }
            }
        }
    }
}