using UnityEngine;

namespace Mechanics.MapGeneration
{
    public static class PathsGenerator 
    {
        public static void Generate()
        {
            var rooms = MapGenerator.Instance.Rooms;
            var paths = MapGenerator.Instance.Paths;
            
            for (var i = 0; i < rooms.Count - 1; ++i)
            {
                var room = rooms[i].Rect.center;
                var nextRoom = rooms[i + 1].Rect.center;
                var position = room;

                while ((int) position.x != (int) nextRoom.x)
                {
                    if (nextRoom.x > position.x)
                        position += Vector2.right;
                    else if (nextRoom.x < position.x)
                        position += Vector2.left;

                    for (var j = 0; j < 2; ++j)
                    {
                        var y = (int) position.y + j;
                        paths.Add(new Vector2Int((int) position.x, y));
                    }
                }

                while ((int) position.y != (int) nextRoom.y)
                {
                    if (nextRoom.y > position.y)
                        position += Vector2.up;
                    else if (nextRoom.y < position.y)
                        position += Vector2.down;

                    for (var j = 0; j < 2; ++j)
                    {
                        var x = (int) position.x + j;
                        paths.Add(new Vector2Int(x, (int) position.y));
                    }
                }
            }
        }
    }
}