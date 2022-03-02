using Mechanics.MapGeneration;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mechanics.Spawners
{
    public class RoomWallSpawner : MonoBehaviour
    {
        private const int Offset = 2;
        private TilemapPainter _painter;

        private int _x;
        private int _xMax;
        private int _y;
        private int _yMax;


        public void Spawn(Room room)
        {
            var rect = room.Rect;
            _painter = GameManager.Instance.painter;

            InitCoordinates(rect);
            BuildLeftWall();
            BuildRightWall();
            BuildTopWall();
            BuildBottomWall();
        }

        private void InitCoordinates(Rect rect)
        {
            _x = (int) rect.x - Offset;
            _y = (int) rect.y - Offset;
            _xMax = (int) rect.xMax + Offset;
            _yMax = (int) rect.yMax + Offset;
        }

        private void BuildLeftWall()
        {
            for (var y = _y; y < _yMax; ++y)
                _painter.PaintRoomWall(new Vector2Int(_x, y));
        }

        private void BuildRightWall()
        {
            for (var y = _y; y < _yMax; ++y)
                _painter.PaintRoomWall(new Vector2Int(_xMax, y));
        }

        private void BuildTopWall()
        {
            for (var x = _x; x < _xMax; ++x)
                _painter.PaintRoomWall(new Vector2Int(x, _y));
        }

        private void BuildBottomWall()
        {
            for (var x = _x; x < _xMax; ++x)
                _painter.PaintRoomWall(new Vector2Int(x, _yMax));
        }
    }
}