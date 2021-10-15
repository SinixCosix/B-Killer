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
        public int roomsCount = 10;
        public int minRoomDistance = 5;
        public GameObject sprite;
        public float time = 10;

        private List<Rect> _rooms;
        private List<GameObject> _sprites;
        private float _time;

        public LevelGenerator()
        {
            _rooms = new List<Rect>();
            _sprites = new List<GameObject>();
        }

        private void Start()
        {
            _time = time;
        }
private void Update()
        {
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
                SplitRoom(rect, roomsCount);
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

            var splitByVertical = Random.Range(0, 2) == 0;
            var splitByHorizontal = Random.Range(0, 2) == 0;
            
            var width = splitByVertical ? rect.width / 2 : rect.width;
            var height = splitByHorizontal ? rect.height / 2 : rect.height;

            var x1 = splitByVertical ? rect.x + width : rect.x;
            var y1 = splitByHorizontal ? rect.y + height : rect.y;
            var rect1 = new Rect(x1, y1, width, height);
            var parts1 = Random.Range(1, parts);

            var x2 = splitByVertical ? x1 - width : x1 + width;
            var y2 = splitByHorizontal ? y1 - height : y1 + height;
            var rect2 = new Rect(x2, y2, width, height);
            var parts2 = parts - parts1;

            // ReSharper disable once TailRecursiveCall
            SplitRoom(rect1, parts1);
            // ReSharper disable once TailRecursiveCall
            SplitRoom(rect2, parts2);
        }

        private void DrawRooms()
        {
            foreach (var room in _rooms)
            {
                var width = Random.Range(room.width / 2 + minRoomDistance, room.width - minRoomDistance);
                var height = Random.Range(room.height / 2+ minRoomDistance, room.height - minRoomDistance);
                var position = new Vector2(room.x, room.y);
                
                var newSprite = Instantiate(sprite, position, Quaternion.identity);
                newSprite.transform.localScale = new Vector3(width, height);
                _sprites.Add(newSprite);
            }
        }

        
    }
}