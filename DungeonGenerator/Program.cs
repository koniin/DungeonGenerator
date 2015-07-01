using System;

namespace DungeonGenerator {
    class Program {
        static void Main(string[] args) {
            var d = new DungeonGenerator();
            var map = d.CreateDungeon(13, 11);
            Console.WriteLine(map.RoomsInPath);
            MapDrawer.Draw(map);

            var d2 = new DungeonGenerator2();
            map = d2.CreateDungeon(15);
            MapDrawer.Draw(map);

            var d3 = new DungeonGenerator3();
            map = d3.CreateDungeon(13, 11);
            MapDrawer.Draw(map);
            Console.Read();
        }
    }
}
