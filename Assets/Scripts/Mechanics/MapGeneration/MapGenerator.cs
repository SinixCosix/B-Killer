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
        public TilemapPainter painter;

        public readonly HashSet<Vector2Int> Paths = new HashSet<Vector2Int>();
        public readonly HashSet<Vector2Int> Decorations = new HashSet<Vector2Int>();
        public readonly HashSet<Vector2Int> Forest = new HashSet<Vector2Int>();

        public MapGenerator()
        {
            Instance = this;
        }

        public void Generate()
        {
            roomsGenerator.Generate();
            var rooms = GameManager.Instance.Rooms;
            
            PathsGenerator.Generate();
            DecorationsGenerator.Generate();
            ForestGenerator.Generate();

            painter.PaintWalls();
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
            
            painter.Clear();
            
        }
    }
}