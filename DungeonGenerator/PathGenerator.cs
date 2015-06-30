using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator {
    public class PathGenerator {
        private readonly Random _rand;

        public PathGenerator(){
            _rand = new Random();
        }

        public void GeneratePathToEnd(Map map, int maxIterations) {
            int counter = 2;
            int iteration = 0;
            Room room = map[map.StartX, map.StartY];

            while (iteration < maxIterations) {
                IEnumerable<Room> neighbours = map.GetNeighbours(room);
                if (neighbours.Any(n => n.X == map.EndX && n.Y == map.EndY)) {
                    room.Id = 99;
                    map.ReachedEnd = true;
                    break;
                }
                foreach (Room neighbour in neighbours.OrderBy(x => _rand.Next())) {
                    if (neighbour.Id == 0 && map.CountNeighbours(neighbour) == 1) {
                        neighbour.Id = counter;
                        room = neighbour;
                        counter++;
                        break;
                    }
                }
                iteration++;
            }
            map.RoomsInPath = counter - 2;
        }
    }
}
