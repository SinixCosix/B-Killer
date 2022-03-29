using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mechanics.MapGeneration;
using Mechanics.Rooms;
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
        public Tilemap wallsTilemap;

        public TileBase pathTile;
        public TileBase lawnTile;
        public TileBase[] treeTile;
        public TileBase[] stoneTile;
        public TileBase[] grassTile;
        public TileBase wallTile;

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            forestTilemap.ClearAllTiles();
            decorationsTilemap.ClearAllTiles();
            obstaclesTilemap.ClearAllTiles();
            wallsTilemap.ClearAllTiles();
        }

        public void PaintDecorations(IEnumerable<Vector2Int> points, IEnumerable<Vector2Int> paths)
        {
            foreach (var point in points)
            {
                var tile = Random.Range(0f, 1f) < 0.2f 
                    ? GetRandomTile(stoneTile) 
                    : GetRandomTile(grassTile);

                PaintTile(point, decorationsTilemap, tile);
            }
        }

        private static TileBase GetRandomTile(TileBase[] tiles)
        {
            var index = Random.Range(0, tiles.Length);
            var tile = tiles[index];
            return tile;
        }

        public void PaintMapWalls()
        {
            FillTileMap(obstaclesTilemap, lawnTile);
        }

        public void PaintRoomWalls(IEnumerable<Vector2Int> points)
        {
            foreach (var point in points)
                PaintTile(point, wallsTilemap, wallTile);
        }

        public void PaintForest(IEnumerable<Vector2Int> points)
        {
            foreach (var point in points)
                PaintTile(point, forestTilemap, GetRandomTile(treeTile));
        }

        private static void FillTileMap(Tilemap tilemap, TileBase tile)
        {
            for (var i = Settings.Instance.MinMapPoint; i < Settings.Instance.MaxMapPoint; ++i)
            for (var j = Settings.Instance.MinMapPoint; j < Settings.Instance.MaxMapPoint; ++j)
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
            Cut(cutSpace, obstaclesTilemap);
            Cut(cutSpace, forestTilemap);
        }

        public void CutWall(IEnumerable<Vector2Int> cutSpace)
        {
            Cut(cutSpace, wallsTilemap);
        }

        private static void Cut(IEnumerable<Vector2Int> cutSpace, Tilemap tilemap)
        {
            PaintTiles(cutSpace, tilemap, null);
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