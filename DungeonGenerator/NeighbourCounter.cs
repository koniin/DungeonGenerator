namespace DungeonGenerator {
    public static class NeighbourCounter {
        public static int Count(Map map, Room room) {
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
    }
}
