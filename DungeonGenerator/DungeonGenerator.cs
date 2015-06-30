using System;

namespace DungeonGenerator {
    /*
     * 
     * Generera kartor
http://stackoverflow.com/questions/15314602/create-a-path-for-a-maze-in-a-two-dimensional-array-any-algorihtm-ideas
algorithm gen-maze(pos):
set pos to 1
 
build a list of neighboring positions
randomly shuffle this list
for each neighbor n of pos in random order:
    if n is 0 and setting it to 1 doesn't create a square:
        gen-maze(n)
 
https://sv.wikipedia.org/wiki/Prims_algoritm
 
     * DUNGEON GENERATOR
         - Maybe you could always have 4 paths? 
            (you could take another exit than what you came from? but then what about closed doors?)
        1. Create a grid
        2. Generate a path from a corner to the middle
        3. Fill X% (maybe 40%) of the tiles with “occupied” if the tile not occupied
        4. Then set doors between any neighbours
            loop each row of the grid
                if tile has a neighbor in any of the foor directions set a door pointing to that tile
        5. Last step is to remove rooms with no connection to the path (so we can place treasure and keys on the path)
            - rooms that only connects through the middle are not ok (unless more paths)

     * */

    public class DungeonGenerator {
        private readonly Random _rand;
        private readonly PathGenerator _pathGenerator;

        public DungeonGenerator(){
            _rand = new Random();
            _pathGenerator = new PathGenerator();
        }

        public Map CreateDungeon(int width, int height) {
            Map map;
            while (true){
                map = CreateRandomStartDungeon(width, height);
                _pathGenerator.GeneratePathToEnd(map, 50);
                if (map.ReachedEnd)
                    break;
            }

            AddSideRooms(map);
            AddDoors(map);
            RemoveUnconnectedRooms(map);
            return map;
        }

        private Map CreateRandomStartDungeon(int width, int height) {
            var map = new Map(width, height);
            map.SetEnd(6, 5);
            SetRandomCornerStart(map, _rand.Next(4));
            return map;
        }

        private void AddSideRooms(Map map){
            AddSideRooms(map, 30);
        }

        private void AddSideRooms(Map map, int probability) {
            for (int y = 0;y < map.Height;y++) {
                for (int x = 0;x < map.Width;x++) {
                    if (map[x, y].Id == 0 && _rand.Next(100) < probability)
                        map[x, y].Id = 88;
                }
            }
        }

        private void AddDoors(Map map) {
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

        private void RemoveUnconnectedRooms(Map map) {
            for (int y = 0;y < map.Height;y++) {
                for (int x = 0;x < map.Width;x++) {
                    if (MapExtensions.CountNeighbours(map, map[x, y]) == 0)
                        map[x, y] = new Room { X = x, Y = y };
                }
            }
        }

        private void SetRandomCornerStart(Map map, int corner) {
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