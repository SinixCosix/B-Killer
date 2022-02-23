using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MapGeneration
{
    public static class RoomsGenerator
    {
        public static void Generate()
        {
            var rects = BinarySpacePartitionTree.Split().ToList();
            var rooms = rects.Select((rect, i) 
                => CreateRoom(rect, (uint)i))
                .ToList();

            MapGenerator.Instance.Rooms = rooms;
        }

        private static Room CreateRoom(Rect rect, uint id)
        {
            var room = new Room(rect, id);
            return room;
        }
    }
}