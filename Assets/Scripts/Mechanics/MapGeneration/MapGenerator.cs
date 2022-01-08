using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        public static MapGenerator Instance;

        public Vector2 StartPoint { get; private set; }
        public TilemapPainter painter;
        [NonSerialized] public List<Rect> Lawns = new List<Rect>();
        public readonly HashSet<Vector2Int> PointsOfLawns = new HashSet<Vector2Int>();

        public readonly HashSet<Vector2Int> Paths = new HashSet<Vector2Int>();
        public readonly HashSet<Vector2Int> Decorations = new HashSet<Vector2Int>();
        public readonly HashSet<Vector2Int> Forest = new HashSet<Vector2Int>();

        public MapGenerator()
        {
            Instance = this;
        }

        public void Generate()
        {
            Clear();

            LawnsGenerator.Generate();
            PathsGenerator.Generate();
            DecorationsGenerator.Generate();
            ForestGenerator.Generate();

            painter.PaintWalls();
            painter.PaintLawns(PointsOfLawns);
            painter.PaintPaths(Paths);
            painter.PaintForest(Forest);
            painter.PaintDecorations(Decorations, Paths);

            painter.Cut(PointsOfLawns);
            painter.Cut(Paths);
        }

        private void Clear()
        {
            Lawns.Clear();
            Paths.Clear();
            Decorations.Clear();
            painter.Clear();
        }

        public Vector2 SelectStartPoint()
        {
            var index = Random.Range(0, Lawns.Count);
            StartPoint = Lawns[index].center;
            return StartPoint;
        }
    }
}