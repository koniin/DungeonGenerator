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
        public void CreateDungeon(){
            Map map;
            while (true){
                map = CreateRandomDungeon();
                if (map.ReachedEnd)
                    break;
            }

            Console.WriteLine("Generated map with " + map.RoomsInPath + " rooms in path");
            MapDrawer.Draw(map);

            map = AddSideRooms(map);

            Console.WriteLine("\nAdded side rooms");
            MapDrawer.Draw(map);

            map = AddDoors(map);
            Console.WriteLine("\nAdded doors");
            MapDrawer.Draw(map);
        }

        private Map CreateRandomDungeon() {
            Random rand = new Random();
            Map map = new Map(13, 11);
            map.SetEnd(6, 5);
            map.SetStart(rand.Next(4));
            map.GeneratePathToEnd(50);
            return map;
        }

        private Map AddSideRooms(Map map){
            map.AddSideRooms(30);
            return map;
        }

        private Map AddDoors(Map map){
            map.AddDoors();
            return map;
        }
    }
}