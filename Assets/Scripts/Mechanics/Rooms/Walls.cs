using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Rooms
{
    public class Walls
    {
        private const int Offset = 2;

        private int _x;
        private int _xMax;
        private int _y;
        private int _yMax;

        public Walls()
        {
            Points = new HashSet<Vector2Int>();
        }

        public HashSet<Vector2Int> Points { get; }

        public void Init(Room room)
        {
            InitCoordinates(room.Rect);
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
                Points.Add(new Vector2Int(_x, y));
        }

        private void BuildRightWall()
        {
            for (var y = _y; y < _yMax; ++y)
                Points.Add(new Vector2Int(_xMax, y));
        }

        private void BuildTopWall()
        {
            for (var x = _x; x < _xMax; ++x)
                Points.Add(new Vector2Int(x, _y));
        }

        private void BuildBottomWall()
        {
            for (var x = _x; x < _xMax; ++x)
                Points.Add(new Vector2Int(x, _yMax));
        }

        public void Clear()
        {
            Points.Clear();
        }
    }
}