using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        public static MapGenerator Instance;

        public RoomsGenerator roomsGenerator;

        public readonly HashSet<Vector2Int> Paths = new HashSet<Vector2Int>();
        public readonly HashSet<Vector2Int> Decorations = new HashSet<Vector2Int>();
        public readonly HashSet<Vector2Int> Forest = new HashSet<Vector2Int>();

        public MapGenerator()
        {
            Instance = this;
        }

        public void Generate()
        {
            var rooms = GameManager.Instance.Rooms;
            roomsGenerator.Generate();
            
            PathsGenerator.Generate();
            DecorationsGenerator.Generate();
            ForestGenerator.Generate();
        }

        public void Paint()
        {
            var rooms = GameManager.Instance.Rooms;
            var painter = GameManager.Instance.painter;
            
            painter.PaintMapWalls();
            painter.PaintRooms(rooms);
            painter.PaintPaths(Paths);
            painter.PaintForest(Forest);
            painter.PaintDecorations(Decorations, Paths);

            painter.Cut(rooms);
            painter.Cut(Paths);
        }

        public void Clear()
        {
            Paths.Clear();
            Decorations.Clear();
            Forest.Clear();
        }
    }
}