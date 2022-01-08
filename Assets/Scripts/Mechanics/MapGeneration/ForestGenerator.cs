using UnityEngine;

namespace Mechanics.MapGeneration
{
    public static class ForestGenerator
    {
        public static void Generate()
        {
            var forest = MapGenerator.Instance.Forest;
            for (var y = 0; y < Settings.Instance.mapSize; y += 4)
            for (var x = 0; x < Settings.Instance.mapSize; x += Random.Range(2, 6))
                forest.Add(new Vector2Int(x, y + Random.Range(-2, 2)));
        }
    }
}