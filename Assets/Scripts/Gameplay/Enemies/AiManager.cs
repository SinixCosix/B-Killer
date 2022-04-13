using Mechanics.Rooms;
using UnityEngine;

namespace Gameplay.Enemies
{
    public static class AiManager
    {
        public static void Recalculate(Room room)
        {
            var graph = AstarPath.active.data.gridGraph;
            Debug.Log($"{graph.width}, {room.Rect.width}");
            graph.center = room.Rect.center;
            graph.SetDimensions((int) room.Rect.width,
                (int) room.Rect.height,
                1);

            Debug.Log($"{graph.width}, {room.Rect.width}");
            AstarPath.active.graphs[0] = graph;
            graph.Scan();
            // AstarPath.active.Scan();
        }
    }
}