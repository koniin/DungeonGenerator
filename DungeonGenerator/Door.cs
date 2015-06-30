namespace DungeonGenerator {
    public enum DoorPosition{
        Top,
        Bottom,
        Left,
        Right
    }

    public class Door {
        public DoorPosition DoorPosition { get; set; }
        public int NextRoomId { get; set; }
        
        public Door(DoorPosition doorPosition, int nextRoomId){
            DoorPosition = doorPosition;
            NextRoomId = nextRoomId;
        }
    }
}
