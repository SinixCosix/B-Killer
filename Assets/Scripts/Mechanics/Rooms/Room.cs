using System;
using System.Collections.Generic;
using Mechanics.Rooms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MapGeneration
{
    public class Room : MonoBehaviour
    {
        public uint Id { get; private set; }
        public Rect Rect { get; private set; }
        public float MinLength { get; private set; }
        public HashSet<Vector2Int> Points { get; private set; }

        private Rect BaseRect { get; set; }
        private bool _isPlayerEntered = false;

        public delegate void MethodContainer(Room room);

        public event MethodContainer PlayerTriggered;


        public void Init(Rect baseRect, uint id)
        {
            BaseRect = baseRect;
            Id = id;
            Reshape();
            CreatePoints();
            InitBorder();
        }

        private void InitBorder()
        {
            var roomBorder = gameObject.GetComponent<RoomBorder>();
            roomBorder.PlayerTriggered += InvokePlayerTriggered;

            var transform1 = roomBorder.transform;
            transform1.position = Rect.center;
            transform1.localScale = new Vector3(Rect.width, Rect.height);
        }

        private void InvokePlayerTriggered()
        {
            if (_isPlayerEntered 
                || PlayerTriggered == null
                || Id == 0)
                return;

            _isPlayerEntered = true;
            PlayerTriggered.Invoke(this);
            Debug.Log("Player has entered in room");
        }

        private void Reshape()
        {
            var gameManager = Settings.Instance;
            var width = Random.Range(MinLength * gameManager.splitRatioFrom, BaseRect.width);
            if (width < gameManager.minRoomSize)
                width = gameManager.minRoomSize;
            var height = Random.Range(MinLength * gameManager.splitRatioFrom, BaseRect.height);
            if (height < gameManager.minRoomSize)
                height = gameManager.minRoomSize;
            var x = Random.Range(BaseRect.x, BaseRect.xMax - width);
            var y = Random.Range(BaseRect.y, BaseRect.yMax - height);

            Rect = new Rect(x, y, width, height);
            MinLength = Math.Min(Rect.width, Rect.height);
        }

        private void CreatePoints()
        {
            Points = GenerateEllipse(Rect);
        }

        private static HashSet<Vector2Int> GenerateEllipse(Rect rect)
        {
            var points = new HashSet<Vector2Int>();
            for (var x = rect.x; x < rect.xMax; ++x)
            for (var y = rect.y; y < rect.yMax; ++y)
            {
                var result = CalculateEllipsePoint(
                    x, y,
                    rect.center.x, rect.center.y,
                    rect.width / 2, rect.height / 2);

                if (result < 1)
                    points.Add(new Vector2Int((int) x, (int) y));
            }

            return points;
        }

        private static float CalculateEllipsePoint(
            float x, float y,
            float x0, float y0,
            float a, float b)
        {
            var term1 = Mathf.Pow(x - x0, 2) / (a * a);
            var term2 = Mathf.Pow(y - y0, 2) / (b * b);

            return term1 + term2;
        }
    }
}