using Mechanics.MapGeneration;
using Mechanics.Rooms;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mechanics.Spawners
{
    public class RoomWallSpawner : MonoBehaviour
    {
        public void Spawn(Room room)
        {
            GameManager.Instance.painter.PaintRoomWalls(room.Walls.Points);
        }

        public void Despawn(Room room)
        {
            Debug.Log("despawn walls");
            GameManager.Instance.painter.CutWall(room.Walls.Points);
        }
    }
}