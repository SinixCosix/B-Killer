using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mechanics
{
    public class TilemapVisualizer : MonoBehaviour
    {
        public Tilemap floorTilemap;
        public TileBase floorTile;
        
        private readonly HashSet<TileBase> _tiles= new HashSet<TileBase>();

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
        }
        public void PaintFloor(IEnumerable<Vector2> positions)
        {
            PaintTiles(positions, floorTilemap, floorTile);
        }

        private void PaintTiles(IEnumerable<Vector2> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var position in positions)
            {
                PaintTile(position, tilemap, tile);
            }
        }

        private void PaintTile(Vector2 position, Tilemap tilemap, TileBase tile)
        {
            var tilePosition = tilemap.WorldToCell(position);
            tilemap.SetTile(tilePosition, tile);
            _tiles.Add(tile);
        }
    }
}