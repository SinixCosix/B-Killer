using UnityEngine;

namespace Mechanics.MapGeneration
{
    public static class ForestGenerator
    {
        public static void Generate()
        {
            var forest = MapGenerator.Instance.Forest;
            for (var y = Settings.Instance.MinMapPoint; y < Settings.Instance.MaxMapPoint; y += 4)
            for (var x = Settings.Instance.MinMapPoint; x < Settings.Instance.MaxMapPoint; x += Random.Range(2, 6))
                forest.Add(new Vector2Int(x, y + Random.Range(-2, 2)));
        }
    }
}