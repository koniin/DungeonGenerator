namespace DungeonGenerator {
    public class DoorBuilder {
        public void AddDoors(Map map) {
            for (int y = 0;y < map.Height;y++) {
                for (int x = 0;x < map.Width;x++) {
                    if (map[x, y].Id != 0) {
                        AddDoors(map, map[x, y]);
                    }
                }
            }

        }

        private void AddDoors(Map map, Room room) {
            if (room.X - 1 >= 0 && map[room.X - 1, room.Y].Id != 0) {
                room.HasDoors = true;
                room.AddDoor(map[room.X - 1, room.Y].Id, DoorPosition.Left);
            }
            if (room.X + 1 < map.Width && map[room.X + 1, room.Y].Id != 0) {
                room.HasDoors = true;
                room.AddDoor(map[room.X + 1, room.Y].Id, DoorPosition.Right);
            }
            if (room.Y - 1 >= 0 && map[room.X, room.Y - 1].Id != 0) {
                room.HasDoors = true;
                room.AddDoor(map[room.X, room.Y - 1].Id, DoorPosition.Top);
            }
            if (room.Y + 1 < map.Height && map[room.X, room.Y + 1].Id != 0) {
                room.HasDoors = true;
                room.AddDoor(map[room.X, room.Y + 1].Id, DoorPosition.Bottom);
            }
        }
    }
}
