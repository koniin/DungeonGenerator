using System.Collections.Generic;

namespace DungeonGenerator {
    public static class MapExtensions {
        public static int CountNeighbours(this Map map, Room room) {
            int neighbourCount = 0;
            // left
            if (room.X - 1 >= 0 && map[room.X - 1, room.Y].Id != 0)
                neighbourCount++;
            // right
            if (room.X + 1 < map.Width && map[room.X + 1, room.Y].Id != 0)
                neighbourCount++;
            // top
            if (room.Y - 1 >= 0 && map[room.X, room.Y - 1].Id != 0)
                neighbourCount++;
            // bottom
            if (room.Y + 1 < map.Height && map[room.X, room.Y + 1].Id != 0)
                neighbourCount++;
            return neighbourCount;
        }

        public static IEnumerable<Room> GetNeighbours(this Map map, Room room) {
            List<Room> neighbours = new List<Room>();
            // left
            if (room.X - 1 >= 0)
                neighbours.Add(map[room.X - 1, room.Y]);
            // right
            if (room.X + 1 < map.Width)
                neighbours.Add(map[room.X + 1, room.Y]);
            // top
            if (room.Y - 1 >= 0)
                neighbours.Add(map[room.X, room.Y - 1]);
            // bottom
            if (room.Y + 1 < map.Height)
                neighbours.Add(map[room.X, room.Y + 1]);
            return neighbours;
        }

        public static void SetRandomCornerStart(this Map map, int corner) {
            switch (corner) {
                case 0:
                    map.StartX = 0;
                    map.StartY = 0;
                    break;
                case 1:
                    map.StartX = map.Width - 1;
                    map.StartY = 0;
                    break;
                case 2:
                    map.StartX = 0;
                    map.StartY = map.Height - 1;
                    break;
                case 3:
                    map.StartX = map.Width - 1;
                    map.StartY = map.Height - 1;
                    break;
            }
            map[map.StartX, map.StartY].Id = 1;
        }
    }
}
