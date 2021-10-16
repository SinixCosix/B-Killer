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
        public Vector2 startPoint;
        public Vector2 endPoint;
        public int splitCount = 5;
        
        public float minSplitSize = 0.2f;
        private float _maxSplitSize = 0.8f;

        public GameObject sprite;
        public int minRoomSize = 25;
        public int minRoomDistance = 5;
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
                foreach (var sprite in _sprites)
                {
                    Destroy(sprite);
                }

                _sprites.Clear();
                var width = Math.Abs(endPoint.x) - Math.Abs(startPoint.x);
                var height = Math.Abs(endPoint.y) - Math.Abs(startPoint.y);
                var rect = new Rect(startPoint, new Vector2(width, height));
                SplitRoom(rect, splitCount);
                DrawRooms();
            }
            else
            {
                _time -= Time.deltaTime;
            }
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

            var width = splitByVertical
                ? Random.Range(rect.width * minSplitSize, rect.width * _maxSplitSize)
                : rect.width;
            var height = splitByHorizontal
                ? Random.Range(rect.height * minSplitSize, rect.height * _maxSplitSize)
                : rect.height;

            // ReSharper disable TailRecursiveCall
            var x1 = rect.x;
            var y1 = rect.y;
            var rect1 = new Rect(x1, y1, width, height);
            SplitRoom(rect1, parts - 1);

            var x2 = splitByVertical ? x1 + width : x1;
            var y2 = splitByHorizontal ? y1 + height : y1;
            var rect2 = new Rect(x2, y2, width, height);
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
                var offsetX = Random.Range(minRoomDistance, room.width);
                var offsetY = Random.Range(minRoomDistance, room.height);
                var position = new Vector2(room.x + offsetX, room.y + offsetY);

                var newSprite = Instantiate(sprite, position, Quaternion.identity);
                newSprite.transform.localScale = new Vector3(width, height);
                _sprites.Add(newSprite);
            }
        }
    }
}