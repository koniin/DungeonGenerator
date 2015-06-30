using System.Collections.Generic;

namespace DungeonGenerator{
    public class Room {
        public Room(){
            Doors = new List<Door>(4);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Id { get; set; }
        public bool HasDoors { get; set; }

        public List<Door> Doors { get; set; } 

        public void AddDoor(int roomId, DoorPosition doorPosition){
            Doors.Add(new Door(doorPosition, roomId));
        }
    }
}