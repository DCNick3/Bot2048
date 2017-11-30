using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot2048.Bots
{
    public class FBot : IGameBot
    {
        /* 
         *     _______
         *     |=   =|
         *     \ ___ /
         *      ''''' 
         *      --|--
         *       / \
         *   
         *   
         */
        public void Deinitialize()
        {
        }

        public Direction GetDirection(GameGrid grid)
        {
            const int depth = 5;
            int best = 0;
            double howGood = double.NegativeInfinity;
            int count = (int)Math.Pow(4,depth);
            var directions = Extentions.EnumDirections().ToArray();
            GameGrid[] gridsnow = new GameGrid[depth + 1];
            for (int i = 0; i < count; i++)
            {
                for (int x = gridsnow.Length - Getmod(i) - 1;x < gridsnow.Length; x++)
                {
                    gridsnow[x] = gridsnow[x - 1].Clone();
                    gridsnow[x].Swipe();
                }
            }
            // drunk, fix later
        }
        static int Getmod(int i)
        {
            int n = 0;
            while (i % 4 == 0)
            {
                n++;
                i /= 4;
            }
            return n;
        }

        public void Initialize()
        {
        }
    }
}
