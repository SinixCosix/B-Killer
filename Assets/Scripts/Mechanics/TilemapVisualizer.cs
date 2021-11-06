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
        public Tilemap obstaclesTilemap;

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
            obstaclesTilemap.ClearAllTiles();
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

        public void PaintWalls()
        {
            FillTileMap(obstaclesTilemap, lawnTile);
        }

        public void PaintForest()
        {
            FillTileMap(forestTilemap, treeTile);
        }

        private void FillTileMap(Tilemap tilemap, TileBase tile)
        {
            for (var i = minMapPoint; i < maxMapPoint; ++i)
            for (var j = minMapPoint; j < maxMapPoint; ++j)
            {
                var position = new Vector2Int(i, j);
                PaintTile(position, tilemap, tile);
            }
        }

        public void Cut(IEnumerable<Rect> cutSpace)
        {
            foreach (var room in cutSpace)
            {
                for (var i = room.x; i < room.xMax; ++i)
                for (var j = room.y - forestOffset; j < room.yMax; ++j)
                {
                    var position = new Vector2Int((int) i, (int) j);
                    PaintTile(position, obstaclesTilemap, null);
                    PaintTile(position, forestTilemap, null);
                }
            }
        }

        public void Cut(IEnumerable<Vector2Int> cutSpace)
        {
            PaintTiles(cutSpace, obstaclesTilemap, null);
            PaintTiles(cutSpace, forestTilemap, null);
        }
        
        public void PaintLawns(IEnumerable<Rect> cutSpace)
        {
            foreach (var room in cutSpace)
            {
                for (var i = room.x; i < room.xMax; ++i)
                for (var j = room.y - forestOffset; j < room.yMax; ++j)
                {
                    var position = new Vector2Int((int) i, (int) j);
                    PaintTile(position, floorTilemap, lawnTile);
                }
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