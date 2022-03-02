using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.Rooms
{
    public class Room : MonoBehaviour
    {
        public uint Id { get; private set; }
        public Rect Rect { get; private set; }

        private float MinLength { get; set; }
        private float MinBaseLength { get; set; }

        public HashSet<Vector2Int> Points { get; private set; }
        public Walls Walls { get; private set; }

        public int MobsCount
        {
            get => _mobsCount;
            set
            {
                _mobsCount = value;
                if (_mobsCount > 0) return;
                
                _mobsCount = 0;
                MobsAreKilled?.Invoke(this);
            }
        }

        private Rect BaseRect { get; set; }
        private bool _isPlayerEntered;
        private int _mobsCount;

        public delegate void MethodContainer(Room room);

        public event MethodContainer PlayerTriggered;
        public event MethodContainer MobsAreKilled;

        public Room()
        {
            Walls = new Walls();
        }
        public void Init(Rect baseRect, uint id)
        {
            BaseRect = baseRect;
            Id = id;
            Reshape();
            InitMobsCount();
            CreatePoints();
            InitBorder();
            Walls.Init(this);
        }

        private void Reshape()
        {
            MinBaseLength = Math.Min(BaseRect.width, BaseRect.height);
            
            var gameManager = Settings.Instance;
            var width = Random.Range(MinBaseLength * gameManager.splitRatioFrom, BaseRect.width);
            if (width < gameManager.minRoomSize)
                width = gameManager.minRoomSize;
            var height = Random.Range(MinBaseLength * gameManager.splitRatioFrom, BaseRect.height);
            if (height < gameManager.minRoomSize)
                height = gameManager.minRoomSize;
            var x = Random.Range(BaseRect.x, BaseRect.xMax - width);
            var y = Random.Range(BaseRect.y, BaseRect.yMax - height);

            Rect = new Rect(x, y, width, height);
            MinLength = Math.Min(Rect.width, Rect.height);

        }

        private void InitMobsCount()
        {
            MobsCount = (int)MinLength / 3;
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
        }
    }
}