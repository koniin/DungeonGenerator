using System;

namespace DungeonGenerator{
    public class MapDrawer {
        public static void Draw(Map map) {
            Console.WriteLine(
                "--------------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 0;y < map.Height;y++) {
                for (int x = 0;x < map.Width;x++) {
                    if (map[x, y].Id == 1) {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (map[x, y].HasDoors){
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(map[x, y].Id.ToString().PadLeft(4));
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}