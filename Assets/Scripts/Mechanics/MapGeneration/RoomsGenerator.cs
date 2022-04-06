using System.Linq;
using Mechanics.Rooms;
using UnityEngine;

namespace Mechanics.MapGeneration
{
    public class RoomsGenerator : MonoBehaviour
    {
        public GameObject roomPrefab;
        
        public void Generate()
        {
            var rects = BinarySpacePartitionTree.Split().ToList();
            GameManager.Instance.Rooms = rects.Select((rect, i) 
                => CreateRoom(rect, (uint)i))
                .ToList();
        }

        private Room CreateRoom(Rect rect, uint id)
        {
            var roomGameObject = Instantiate(roomPrefab, gameObject.transform);
            var room = roomGameObject.GetComponent<Room>();
            room.Init(rect, id);
            return room;
        }
    }
}