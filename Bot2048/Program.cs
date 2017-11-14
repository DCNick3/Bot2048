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
            var g = new GameArea();

            g[0, 0] = 1;
            g[1, 0] = 1;
            g[2, 0] = 1;
            g[3, 0] = 1;
            Console.WriteLine(g.Dump());

            g.Swipe(Direction.Right);

            Console.WriteLine("====");

            Console.WriteLine(g.Dump());
        }
    }
}
