using System;

namespace DungeonGenerator {
    public class DungeonGenerator3 {
        private readonly Random _rand;
        private readonly PathGenerator _pathGenerator;
        private DoorBuilder _doorBuilder;

        public DungeonGenerator3() {
            _rand = new Random();
            _pathGenerator = new PathGenerator();
            _doorBuilder = new DoorBuilder();
        }

        public Map CreateDungeon(int width, int height) {
            Map map;
            while (true){
                map = CreateRandomStartDungeon(width, height);
                _pathGenerator.GeneratePathToEnd(map, 60);
                if (map.ReachedEnd)
                    break;
            }

            _pathGenerator.GeneratePathToEnd(map, 60);
            AddMulitpleSideRooms(map, 60);

            _doorBuilder.AddDoors(map);

            return map;
        }

        private void AddMulitpleSideRooms(Map map, int probability) {
            for (int y = 0; y < map.Height; y++){
                for (int x = 0; x < map.Width; x++){
                    if (map[x, y].Id > 1 && map[x, y].Id != 99 && map[x, y].Id != 88) {
                        if(_rand.Next(100) < probability)
                            AddSideRooms(map[x, y], map);
                    }
                }
            }

        }

        private void AddSideRooms(Room room, Map map){
            if (room.X - 1 >= 0 && map[room.X - 1, room.Y].Id == 0){
                map[room.X - 1, room.Y].Id = 88;
            }
            if (room.X + 1 < map.Width && map[room.X + 1, room.Y].Id == 0) {
                map[room.X + 1, room.Y].Id = 88;
            }
            if (room.Y - 1 >= 0 && map[room.X, room.Y - 1].Id == 0){
                map[room.X, room.Y - 1].Id = 88;
            }
            if (room.Y + 1 < map.Height && map[room.X, room.Y + 1].Id == 0){
                map[room.X, room.Y + 1].Id = 88;
            }

        }

        private Map CreateRandomStartDungeon(int width, int height) {
            var map = new Map(width, height);
            map.SetEnd(6, 5);
            map.SetRandomCornerStart(_rand.Next(4));
            return map;
        }
    }
}
