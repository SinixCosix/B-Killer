using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        public MobSpawner mobSpawner;
        public Vector2 StartPoint
        {
            get
            {
                var index = Random.Range(0, _lawns.Count);
                return _lawns[index].center;
            }
        }

        public TilemapVisualizer tilemap;

        private List<Rect> _lawns = new List<Rect>();
        private HashSet<Vector2Int> _roomPoints = new HashSet<Vector2Int>();

        private readonly HashSet<Vector2Int> _paths = new HashSet<Vector2Int>();
        private readonly HashSet<Vector2Int> _decorations = new HashSet<Vector2Int>();
        private readonly HashSet<Vector2Int> _trees = new HashSet<Vector2Int>();

    

        public void Generate()
        {
            Clear();

            GenerateLawns();
            GeneratePaths();
            GenerateDecorations(_lawns);
            GenerateDecorations(_paths);
            GenerateTrees();
            mobSpawner.Spawn(_lawns);

            tilemap.PaintWalls();
            tilemap.PaintLawns(_roomPoints);
            tilemap.PaintPaths(_paths);
            tilemap.PaintForest(_trees);
            tilemap.PaintDecorations(_decorations, _paths);

            tilemap.Cut(_roomPoints);
            tilemap.Cut(_paths);
        }

        private void Clear()
        {
            mobSpawner.Clear();
            _lawns.Clear();
            _paths.Clear();
            _decorations.Clear();
            tilemap.Clear();
        }

        private void GenerateLawns()
        {
            var generator = new LawnGenerator();
            generator.Generate();

            _lawns = generator.Lawns;
            _roomPoints = generator.PointsOfLawns;
        }

        private void GenerateTrees()
        {
            for (var y = 0; y < GameManager.Instance.mapSize; y += 4)
            for (var x = 0; x < GameManager.Instance.mapSize; x += Random.Range(2, 6))
                _trees.Add(new Vector2Int(x, y + Random.Range(-2, 2)));
        }


        private void GenerateDecorations(IEnumerable<Rect> lawns)
        {
            foreach (var rect in lawns)
            {
                var minDecorationsCount = rect.width;
                var maxDecorationsCount = rect.width + rect.height;
                var decorationsCount = Random.Range(minDecorationsCount, maxDecorationsCount);
                for (var i = 0; i < decorationsCount; ++i)
                {
                    var x = Random.Range((int) rect.x, (int) rect.xMax);
                    var y = Random.Range((int) rect.y, (int) rect.yMax);
                    _decorations.Add(new Vector2Int(x, y));
                }
            }
        }

        private void GenerateDecorations(ICollection<Vector2Int> paths)
        {
            var minDecorationsCount = paths.Count / 6;
            var maxDecorationsCount = paths.Count / 5;
            var decorationsCount = Random.Range(minDecorationsCount, maxDecorationsCount);
            for (var i = 0; i < decorationsCount; ++i)
            {
                var index = Random.Range(0, paths.Count);
                var point = paths.ElementAt(index);
                _decorations.Add(point);
            }
        }


        private void GeneratePaths()
        {
            for (var i = 0; i < _lawns.Count - 1; ++i)
            {
                var room = _lawns[i].center;
                var nextRoom = _lawns[i + 1].center;
                var position = room;

                while ((int) position.x != (int) nextRoom.x)
                {
                    if (nextRoom.x > position.x)
                        position += Vector2.right;
                    else if (nextRoom.x < position.x)
                        position += Vector2.left;

                    for (var j = 0; j < 2; ++j)
                    {
                        var y = (int) position.y + j;
                        _paths.Add(new Vector2Int((int) position.x, y));
                    }
                }

                while ((int) position.y != (int) nextRoom.y)
                {
                    if (nextRoom.y > position.y)
                        position += Vector2.up;
                    else if (nextRoom.y < position.y)
                        position += Vector2.down;

                    for (var j = 0; j < 2; ++j)
                    {
                        var x = (int) position.x + j;
                        _paths.Add(new Vector2Int(x, (int) position.y));
                    }
                }
            }
        }
    }
}