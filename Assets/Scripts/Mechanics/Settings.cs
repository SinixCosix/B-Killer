using UnityEngine;

namespace Mechanics
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance;
        
        public float time = 2;
        public bool infinityGeneration;
        public uint mapSize = 100;
        public uint splitCount = 4;

        public float splitRatioFrom = 0.25f;
        public float splitRatioTo;

        public uint minRoomSize = 6;

        public Settings()
        {
            Instance = this;
        }
        
        private void Awake()
        {
            splitRatioTo = 1 - splitRatioFrom;
        }
    }
}