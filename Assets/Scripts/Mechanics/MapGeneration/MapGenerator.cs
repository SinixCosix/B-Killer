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

        public void Clear()
        {
            Lawns.Clear();
            PointsOfLawns.Clear();
            Paths.Clear();
            Decorations.Clear();
            Forest.Clear();
            
            painter.Clear();
            
        }

        public void SelectStartPoint()
        {
            var index = Random.Range(0, Lawns.Count);
            StartPoint = Lawns[index].center;
        }
    }
}