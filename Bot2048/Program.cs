using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bot2048
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new GameArea(8, 8);

            g[0, 0] = new Tile(1);
            g[1, 0] = new Tile(1);
            g[2, 0] = new Tile(1);
            g[3, 0] = new Tile(1);
            g[4, 0] = new Tile(1);
            g[5, 0] = new Tile(1);
            g[6, 0] = new Tile(1);
            g[7, 0] = new Tile(1);
            g[4, 1] = new Tile(1);
            g[5, 1] = new Tile(1);
            g[6, 1] = new Tile(1);
            g[7, 1] = new Tile(1);
            Console.WriteLine(g.Dump());

            g.Swipe(Direction.Right);
            Console.WriteLine("====");
            Console.WriteLine(g.Dump());

            g.Swipe(Direction.Down);
            Console.WriteLine("====");
            Console.WriteLine(g.Dump());

            g.Swipe(Direction.Left);
            Console.WriteLine("====");
            Console.WriteLine(g.Dump());
        }
    }
}
