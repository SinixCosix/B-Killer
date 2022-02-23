using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mechanics.MapGeneration;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mechanics
{
    public class TilemapPainter : MonoBehaviour
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
        public TileBase bushTile;

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

        public void PaintForest(IEnumerable<Vector2Int> points)
        {
            FillTileMap(forestTilemap, bushTile);
            PaintTiles(points, forestTilemap, treeTile);
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

        public void Cut(IEnumerable<Room> rooms)
        {
            foreach (var room in rooms)
                Cut(room.Points);
        }

        public void Cut(IEnumerable<Vector2Int> cutSpace)
        {
            PaintTiles(cutSpace, obstaclesTilemap, null);
            PaintTiles(cutSpace, forestTilemap, null);
        }
        
        public void PaintRooms(IEnumerable<Room> rooms)
        {
            foreach (var room in rooms)
            foreach (var point in room.Points)
                PaintTile(point, floorTilemap, lawnTile);
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