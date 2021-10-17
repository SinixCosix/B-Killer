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
        public uint mainRoomSize = 100;
        public uint splitCount = 5;

        public float splitRatio = 0.25f;
        private float _splitRatio;

        public GameObject sprite;
        public uint minRoomSize = 6;
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
            _splitRatio = 1 - splitRatio;
            if (_time < 0)
            {
                _time = time;
                _rooms.Clear();
                foreach (var item in _sprites)
                    Destroy(item);

                _sprites.Clear();
                var rect = new Rect(0, 0, mainRoomSize, mainRoomSize);
                SplitRoom(rect, splitCount);
                DrawRooms();
            }
            else
                _time -= Time.deltaTime;
        }

        private void SplitRoom(Rect rect, uint parts)
        {
            if (parts == 0)
            {
                _rooms.Add(rect);
                return;
            }

            bool splitByVertical;
            if (rect.width / rect.height >= 1.25)
                splitByVertical = true;
            else if (rect.height / rect.width >= 1.25)
                splitByVertical = false;
            else
                splitByVertical = Random.Range(0f, 1f) > 0.5f;

            var splitByHorizontal = !splitByVertical;

            var width1 = splitByVertical
                ? Random.Range(rect.width * splitRatio, rect.width * _splitRatio)
                : rect.width;
            var height1 = splitByHorizontal
                ? Random.Range(rect.height * splitRatio, rect.height * _splitRatio)
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
                var width = Random.Range(minLength * splitRatio, room.width);
                if (width < minRoomSize)
                    width = minRoomSize;
                var height = Random.Range(minLength * splitRatio, room.height);
                if (height < minRoomSize)
                    height = minRoomSize;
                var x = Random.Range(room.x, room.xMax - width);
                var y = Random.Range(room.y, room.yMax - height);

                var rect = new Rect(x, y, width, height);
                for (var i = rect.x; i < rect.xMax; ++i)
                for (var j = rect.y; j < rect.yMax; ++j)
                {
                    var position = new Vector3((int) i, (int) j);
                    var newSprite = Instantiate(sprite, position, Quaternion.identity);
                    _sprites.Add(newSprite);
                }
            }
        }
    }
}