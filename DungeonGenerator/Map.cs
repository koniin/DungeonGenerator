namespace DungeonGenerator{
    public class Map {
        public int Width { get; set; }
        public int Height { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        private readonly Room[,] _grid;
        public Map(int x, int y) {
            _grid = new Room[x, y];
            Width = x;
            Height = y;
            Initialize();
        }

        private void Initialize() {
            for (int y = 0;y < Height;y++) {
                for (int x = 0;x < Width;x++) {
                    _grid[x, y] = new Room() { X = x, Y = y };
                }
            }
        }

        public Room this[int x, int y] {
            get { return _grid[x, y]; }
            set { _grid[x, y] = value; }
        }

        public void SetEnd(int x, int y) {
            EndX = x;
            EndY = y;
        }

        public void SetStart(int x, int y){
            StartX = x;
            StartY = y;
            _grid[x, y].Id = 1;
        }

        public bool ReachedEnd { get; set; }
        public int RoomsInPath { get; set; }
    }
}