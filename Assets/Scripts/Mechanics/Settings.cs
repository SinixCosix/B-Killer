using System;
using UnityEngine;

namespace Mechanics
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance;

        public float time = 2;
        public bool infinityGeneration;

        public uint mapSize = 150;
        public uint mapBorder = 25;
        public uint splitCount = 4;
        [NonSerialized] public int MinMapPoint;
        [NonSerialized] public int MaxMapPoint;

        public float splitRatioFrom = 0.25f;
        [NonSerialized] public float SplitRatioTo;

        public uint minRoomSize = 6;

        public Settings()
        {
            Instance = this;
        }

        private void Awake()
        {
            SplitRatioTo = 1 - splitRatioFrom;
            MinMapPoint = -(int) mapBorder;
            MaxMapPoint = (int) (mapSize + mapBorder);
        }
    }
}