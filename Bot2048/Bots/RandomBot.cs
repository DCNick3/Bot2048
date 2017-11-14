using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot2048.Bots
{
    class RandomBot : IGameBot
    {
        Random rnd = new Random();
        public void Deinitialize()
        {
        }

        public Direction GetDirection(GameGrid grid)
        {
            return Enum.GetValues(typeof(Direction)).Cast<Direction>().RandomElement(rnd);
        }

        public void Initialize()
        {
            rnd = new Random();
        }
    }
}
