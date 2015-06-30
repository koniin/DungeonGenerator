using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator {
    /*
     * Here's one possible approach not using all available rooms.

        Start with a random room, this is your entry point. Add this room to a list open_ends.
        Repeat the following steps till your dungeon is "complete" (e.g. target number of rooms achieved).
        Pick a random open end and a random direction.
        Remove the picked room from open ends.
        If the room you're "moving" to doesn't exist, create it and add it to open ends.
        Mark both rooms to have a connection between them, either unlocked or locked using one of the available keys.
        If you've picked a key and keys are one time use only, remove the key from available keys.
        If the room you're "moving" to doesn't have treasure, do a random roll to determine whether there is some.
        If there is treasure and it's a key (e.g. red key, blue key, etc.), add that key to available keys.
        Now your dungeon is almost finished. You're still looking to get some stairs down or a boss room? Easy!

        Pick a random room from open ends.
        If there isn't one available, loop through all rooms and try to add a new one.
        Mark the room to be the boss room and remove it from open ends.
        In a similar fashion, you can add stuff like treasure rooms, shops, libraries, temples, etc. as long as there's still room in your dungeon.
     * */

    public class DungeonGenerator2 {
        private readonly Random _rand;
        private List<Room> _openEnds;

        public DungeonGenerator2(){
            _rand = new Random();
        }

         
        public Map CreateDungeon(int targetRoomCount){
            _openEnds = new List<Room>();
            Map map = CreateRandomStartDungeon(targetRoomCount / 2, targetRoomCount / 2);
            _openEnds.Add(map[map.StartX, map.StartY]);
            int roomCount = 0;
            while (roomCount < targetRoomCount){
                Room currentRoom = _openEnds.ElementAt(_rand.Next(_openEnds.Count));
                // Check so that we dont visit a visited room again
                if (currentRoom.HasDoors)
                    continue;

                currentRoom.HasDoors = true;
                _openEnds.Remove(currentRoom);
                
                // add all the rooms open neighbours to open ends
                var neighbours = map.GetNeighbours(currentRoom);
                _openEnds.AddRange(neighbours);

                // create connections to all the rooms

                // TODO: CREATE CONNECTIONS / DOORS

                // repeat
                
                roomCount++;
            }
            return map;
        }

        private Map CreateRandomStartDungeon(int width, int height) {
            var map = new Map(width, height);
            map.SetEnd(6, 5);
            map.SetStart(_rand.Next(width), _rand.Next(height));
            return map;
        }
    }
}
