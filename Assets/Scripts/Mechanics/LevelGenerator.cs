using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics
{
    public class LevelGenerator : MonoBehaviour
    {
        public int mainRoomSize = 100;
        public int splitCount = 5;

        public float minSplitSize = 0.3f;
        private float _maxSplitSize;

        public GameObject sprite;
        public int minRoomSize = 25;
        public float time = 2;

        private readonly List<Rect> _rooms = new List<Rect>();
        private readonly List<GameObject> _sprites = new List<GameObject>();
        private float _time;

        private void Start()
        {
            _time = time;
        }

        private void Update()
        {
            _maxSplitSize = 1 - minSplitSize;
            if (_time < 0)
            {
                _time = time;
                _rooms.Clear();
                foreach (var item in _sprites)
                    Destroy(item);

                _sprites.Clear();
                var rect = new Rect(0,0, mainRoomSize, mainRoomSize);
                SplitRoom(rect, splitCount);
                DrawRooms();
            }
            else
                _time -= Time.deltaTime;
        }

        private void SplitRoom(Rect rect, int parts)
        {
            if (parts == 1)
            {
                _rooms.Add(rect);
                return;
            }

            bool splitByVertical;
            var partsRelation = rect.width / rect.height;

            if (partsRelation > 3)
                splitByVertical = true;
            else if (partsRelation < 0.3)
                splitByVertical = false;
            else
                splitByVertical = Random.Range(0, 2) == 0;

            var splitByHorizontal = !splitByVertical;

            var width1 = splitByVertical
                ? Random.Range(rect.width * minSplitSize, rect.width * _maxSplitSize)
                : rect.width;
            var height1 = splitByHorizontal
                ? Random.Range(rect.height * minSplitSize, rect.height * _maxSplitSize)
                : rect.height;

            // ReSharper disable TailRecursiveCall
            var x1 = rect.x;
            var y1 = rect.y;
            var rect1 = new Rect(x1, y1, width1, height1);
            SplitRoom(rect1, parts - 1);

            var width2 = splitByVertical ? rect.width - rect1.width : rect.width;
            var height2 = splitByHorizontal ? rect.height - rect1.height : rect.height;
            var x2 = splitByVertical ? x1 + width1 : x1;
            var y2 = splitByHorizontal ? y1 + height1 : y1;
            var rect2 = new Rect(x2, y2, width2, height2);
            SplitRoom(rect2, parts - 1);
        }

        private void DrawRooms()
        {
            Debug.Log($"rooms count {_rooms.Count}");
            foreach (var room in _rooms)
            {
                var minLength = Math.Min(room.width, room.height);
                var width = Random.Range(minLength, room.width * _maxSplitSize);
                if (width < minRoomSize)
                    width = minRoomSize;
                var height = Random.Range(minLength, room.height * _maxSplitSize);
                if (height < minRoomSize)
                    height = minRoomSize;
                // var offsetX = Random.Range(minRoomDistance, room.width);
                // var offsetY = Random.Range(minRoomDistance, room.height);
                
                for (var i = room.x; i < room.xMax - 2; ++i)
                for (var j = room.y; j < room.yMax - 2; ++j)
                {
                    var position = new Vector3((int)i, (int)j);
                    var newSprite = Instantiate(sprite, position, Quaternion.identity);
                    _sprites.Add(newSprite);
                }
            }
        }
    }
}