using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator{
    public class Map {
        public int Width { get; set; }
        public int Height { get; set; }
        private Room[,] _grid;
        private Random _rand;
        public Map(int x, int y) {
            _grid = new Room[x, y];
            Width = x;
            Height = y;
            _rand = new Random();
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

        private int endX;
        private int endY;
        public void SetEnd(int x, int y) {
            endX = x;
            endY = y;
            //_grid[x,y] = new Cell() { Type = 1, X = x, Y = y };
        }

        public void SetStart(int corner) {
            switch (corner) {
                case 0:
                    startX = 0;
                    startY = 0;
                    break;
                case 1:
                    startX = Width - 1;
                    startY = 0;
                    break;
                case 2:
                    startX = 0;
                    startY = Height - 1;
                    break;
                case 3:
                    startX = Width - 1;
                    startY = Height - 1;
                    break;
            }
            _grid[startX, startY].Id = 2;
        }

        public bool ReachedEnd { get; set; }
        public int RoomsInPath { get; set; }

        private int startX;
        private int startY;

        public void GeneratePathToEnd(int maxIterations) {
            int counter = 3;
            int iteration = 0;
            Room room = _grid[startX, startY];
            
            while (iteration < maxIterations) {
                IEnumerable<Room> neighbours = GetNeighbours(room);
                if (neighbours.Any(n => n.X == endX && n.Y == endY)){
                    room.Id = 99;
                    ReachedEnd = true;
                    break;
                }
                foreach (Room neighbour in neighbours.OrderBy(x => _rand.Next())) {
                    if (neighbour.Id == 0 && EmptyNeighbours(neighbour) == 3) {
                        neighbour.Id = counter;
                        room = neighbour;
                        counter++;
                        break;
                    }
                }
                iteration++;
            }
            RoomsInPath = counter - 4;
        }

        private IEnumerable<Room> GetNeighbours(Room room) {
            List<Room> neighbours = new List<Room>();
            // left
            if (room.X - 1 >= 0)
                neighbours.Add(_grid[room.X - 1, room.Y]);
            // right
            if (room.X + 1 < Width)
                neighbours.Add(_grid[room.X + 1, room.Y]);
            // top
            if (room.Y - 1 >= 0)
                neighbours.Add(_grid[room.X, room.Y - 1]);
            // bottom
            if (room.Y + 1 < Height)
                neighbours.Add(_grid[room.X, room.Y + 1]);
            return neighbours;
        }

        private int EmptyNeighbours(Room room) {
            int neighbourCount = 0;
            // left
            if ((room.X - 1 >= 0 && _grid[room.X - 1, room.Y].Id == 0) || room.X - 1 < 0)
                neighbourCount++;
            // right
            if ((room.X + 1 < Width && _grid[room.X + 1, room.Y].Id == 0) || room.X + 1 >= Width)
                neighbourCount++;
            // top
            if ((room.Y - 1 >= 0 && _grid[room.X, room.Y - 1].Id == 0) || room.Y - 1 < 0)
                neighbourCount++;
            // bottom
            if ((room.Y + 1 < Height && _grid[room.X, room.Y + 1].Id == 0) || room.Y + 1 >= Height)
                neighbourCount++;
            return neighbourCount;
        }

        public void AddSideRooms(int probability){
            for (int y = 0;y < Height;y++) {
                for (int x = 0;x < Width;x++){
                    if (_grid[x, y].Id == 0 && _rand.Next(100) < probability)
                        _grid[x, y].Id = 88;
                }
            }
        }

        public void AddDoors(){
            for (int y = 0;y < Height;y++) {
                for (int x = 0;x < Width;x++) {
                    if (_grid[x, y].Id != 0){
                        AddDoors(_grid[x, y]);
                    }
                }
            }

        }

        private void AddDoors(Room room){
            if (room.X - 1 >= 0 && _grid[room.X - 1, room.Y].Id != 0){
                room.HasDoors = true;
            }
            if (room.X + 1 < Width && _grid[room.X + 1, room.Y].Id != 0) {
                room.HasDoors = true;
            }
            if (room.Y - 1 >= 0 && _grid[room.X, room.Y - 1].Id != 0) {
                room.HasDoors = true;
            }
            if (room.Y + 1 < Height && _grid[room.X, room.Y + 1].Id != 0){
                room.HasDoors = true;
            }
        }
    }
}