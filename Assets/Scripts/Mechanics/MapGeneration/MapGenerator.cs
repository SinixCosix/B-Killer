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
        public Vector2 StartPoint { get; private set; }
        public TilemapPainter painter;
        [NonSerialized] public List<Room> Rooms = new List<Room>();

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
            PathsGenerator.Generate();
            DecorationsGenerator.Generate();
            ForestGenerator.Generate();

            painter.PaintWalls();
            painter.PaintRooms(Rooms);
            painter.PaintPaths(Paths);
            painter.PaintForest(Forest);
            painter.PaintDecorations(Decorations, Paths);

            painter.Cut(Rooms);
            painter.Cut(Paths);
        }

        public void Clear()
        {
            Rooms.Clear();
            Paths.Clear();
            Decorations.Clear();
            Forest.Clear();
            
            painter.Clear();
            
        }

        public void SelectStartPoint()
        {
            var index = Random.Range(0, Rooms.Count);
            StartPoint = Rooms[index].Rect.center;
        }
    }
}