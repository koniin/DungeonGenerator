﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator {
    class Program {
        static void Main(string[] args) {
            DungeonGenerator d = new DungeonGenerator();
            d.CreateDungeon();
            Console.Read();
        }
    }
}