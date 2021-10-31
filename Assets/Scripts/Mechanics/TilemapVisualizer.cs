using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mechanics
{
    public class TilemapVisualizer : MonoBehaviour
    {
        public Tilemap floorTilemap;
        public TileBase lawnTile;
        public TileBase pathTile;
        
        private readonly HashSet<TileBase> _tiles= new HashSet<TileBase>();

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
        }

        public void PaintLawns(IEnumerable<Rect> lawns)
        {
            foreach (var room in lawns)
            {
                for (var i = room.x; i < room.xMax; ++i)
                for (var j = room.y; j < room.yMax; ++j)
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

        private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var position in positions)
                PaintTile(position, tilemap, tile);
        }

        private void PaintTile(Vector2Int position, Tilemap tilemap, TileBase tile)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
            _tiles.Add(tile);
        }
    }
}