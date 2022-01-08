using System;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.BaseCommands;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        public static MapGenerator Instance;
        public MobSpawner mobSpawner;
        public Vector2 StartPoint
        {
            get
            {
                var index = Random.Range(0, Lawns.Count);
                return Lawns[index].center;
            }
        }

        public TilemapVisualizer tilemap;
        [NonSerialized]
        public List<Rect> Lawns = new List<Rect>();
        public HashSet<Vector2Int> RoomPoints = new HashSet<Vector2Int>();

        public  HashSet<Vector2Int> Paths = new HashSet<Vector2Int>();
        public  HashSet<Vector2Int> Decorations = new HashSet<Vector2Int>();
        public  HashSet<Vector2Int> Trees = new HashSet<Vector2Int>();

        public MapGenerator()
        {
            Instance = this;
        }

        public void Generate()
        {
            Clear();

            GenerateLawns();
            GeneratePaths();
            GenerateDecorations();
            GenerateTrees();
            mobSpawner.Spawn(Lawns);

            tilemap.PaintWalls();
            tilemap.PaintLawns(RoomPoints);
            tilemap.PaintPaths(Paths);
            tilemap.PaintForest(Trees);
            tilemap.PaintDecorations(Decorations, Paths);

            tilemap.Cut(RoomPoints);
            tilemap.Cut(Paths);
        }

        private void Clear()
        {
            mobSpawner.Clear();
            Lawns.Clear();
            Paths.Clear();
            Decorations.Clear();
            tilemap.Clear();
        }

        private void GenerateLawns()
        {
            var generator = new LawnGenerator();
            generator.Generate();

            Lawns = generator.Lawns;
            RoomPoints = generator.PointsOfLawns;
        }

        private void GenerateTrees()
        {
            for (var y = 0; y < GameManager.Instance.mapSize; y += 4)
            for (var x = 0; x < GameManager.Instance.mapSize; x += Random.Range(2, 6))
                Trees.Add(new Vector2Int(x, y + Random.Range(-2, 2)));
        }


        private void GenerateDecorations()
        {
            var generator = new DecorationGenerator();
            generator.Generate();
        }

        
        
        private void GeneratePaths()
        {
            for (var i = 0; i < Lawns.Count - 1; ++i)
            {
                var room = Lawns[i].center;
                var nextRoom = Lawns[i + 1].center;
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
                        Paths.Add(new Vector2Int((int) position.x, y));
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
                        Paths.Add(new Vector2Int(x, (int) position.y));
                    }
                }
            }
        }
    }
}