using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics.MapGeneration
{
    public class DecorationGenerator : IGenerator
    {
        public void Generate()
        {
            GenerateForLawns();
            GenerateForPaths();
        }
        
        private static void GenerateForLawns()
        {
            var lawns = MapGenerator.Instance.Lawns;
            foreach (var rect in lawns)
            {
                var minDecorationsCount = rect.width;
                var maxDecorationsCount = rect.width + rect.height;
                var decorationsCount = Random.Range(minDecorationsCount, maxDecorationsCount);
                for (var i = 0; i < decorationsCount; ++i)
                {
                    var x = Random.Range((int) rect.x, (int) rect.xMax);
                    var y = Random.Range((int) rect.y, (int) rect.yMax);
                    MapGenerator.Instance.Decorations.Add(new Vector2Int(x, y));
                }
            }
        }

        private static void GenerateForPaths()
        {
            var minDecorationsCount =  MapGenerator.Instance.Paths.Count / 6;
            var maxDecorationsCount =  MapGenerator.Instance.Paths.Count / 5;
            var decorationsCount = Random.Range(minDecorationsCount, maxDecorationsCount);
            for (var i = 0; i < decorationsCount; ++i)
            {
                var index = Random.Range(0,  MapGenerator.Instance.Paths.Count);
                var point = MapGenerator.Instance.Paths.ElementAt(index);
                MapGenerator.Instance.Decorations.Add(point);
            }
        }
    }
}