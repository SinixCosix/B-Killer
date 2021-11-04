using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mechanics
{
    public class TilemapVisualizer : MonoBehaviour
    {
        public Tilemap floorTilemap;
        public Tilemap forestTilemap;
        public Tilemap decorationsTilemap;

        public TileBase pathTile;
        public TileBase lawnTile;
        public TileBase treeTile;
        public TileBase stoneTile;
        public TileBase grassTile;

        public uint forestOffset = 3;
        public int minMapPoint = -25;
        public int maxMapPoint = 175;

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            forestTilemap.ClearAllTiles();
            decorationsTilemap.ClearAllTiles();
        }

        public void PaintDecorations(IEnumerable<Vector2Int> points, IEnumerable<Vector2Int> paths)
        {
            foreach (var point in points)
            {
                var tile = Random.Range(0f, 1f) > 0.5 ? stoneTile : grassTile;
                if (paths.Contains(point) && tile == grassTile)
                    continue;

                PaintTile(point, decorationsTilemap, tile);
            }
        }

        public void PaintForest()
        {
            for (var i = minMapPoint; i < maxMapPoint; ++i)
            for (var j = minMapPoint; j < maxMapPoint; ++j)
            {
                var position = new Vector2Int(i, j);
                PaintTile(position, forestTilemap, treeTile);
            }
        }

        public void CutForest(IEnumerable<Rect> cutSpace)
        {
            foreach (var room in cutSpace)
            {
                for (var i = room.x; i < room.xMax; ++i)
                for (var j = room.y - forestOffset; j < room.yMax; ++j)
                {
                    var position = new Vector2Int((int) i, (int) j);
                    PaintTile(position, forestTilemap, null);
                }
            }
        }

        public void CutForest(IEnumerable<Vector2Int> cutSpace)
        {
            foreach (var point in cutSpace)
            {
                var position = new Vector2Int(point.x, point.y);
                PaintTile(position, forestTilemap, null);
            }
        }

        public void PaintLawns()
        {
            for (var i = minMapPoint; i < maxMapPoint; ++i)
            for (var j = minMapPoint; j < maxMapPoint; ++j)
            {
                var position = new Vector2Int(i, j);
                PaintTile(position, floorTilemap, lawnTile);
            }
        }

        public void PaintPaths(IEnumerable<Vector2Int> pathPositions)
        {
            PaintTiles(pathPositions, floorTilemap, pathTile);
        }

        private static void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var position in positions)
                PaintTile(position, tilemap, tile);
        }

        private static void PaintTile(Vector2Int position, Tilemap tilemap, TileBase tile)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int) position);
            tilemap.SetTile(tilePosition, tile);
        }
    }
}