using System;
using System.Linq;

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
        private readonly DoorBuilder _doorBuilder;

        public DungeonGenerator(){
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

            AddSideRooms(map);
            _doorBuilder.AddDoors(map);
            RemoveUnconnectedRooms(map);
            return map;
        }

        private Map CreateRandomStartDungeon(int width, int height) {
            var map = new Map(width, height);
            map.SetEnd(6, 5);
            map.SetRandomCornerStart(_rand.Next(4));
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

        private void RemoveUnconnectedRooms(Map map) {
            for (int y = 0; y < map.Height; y++) {
                for (int x = 0; x < map.Width; x++) {
                    if (map.CountNeighbours(map[x, y]) == 0)
                        map[x, y] = new Room { X = x, Y = y };
                    else if (RoomHasOnlySideRoomConnections(map, map[x, y]))
                        map[x, y] = new Room { X = x, Y = y };
                }
            }
        }

        private bool RoomHasOnlySideRoomConnections(Map map, Room room) {
            var neighbours = map.GetNeighbours(room);
            if (room.Id == 88 && neighbours.All(n => n.Id == 88 || n.Id == 0))
                return true;

            return false;
        }
    }
}