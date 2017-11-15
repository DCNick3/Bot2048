using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot2048.Bots
{
    public class NaiveBot : IGameBot
    {
        public void Deinitialize()
        {
        }

        public Direction GetDirection(GameGrid grid)
        {
            return Enum.GetValues(typeof(Direction)).Cast<Direction>()
                .OrderByDescending(_ =>
                {
                    var c = grid.Clone();
                    if (!c.Swipe(_))
                        return -100.0;
                    return c.EnumerateTiles().Where(__ => c[__].IsEmpty).Count() * 1.5 + c.Score;
                }).First();
        }

        public void Initialize()
        {
        }
    }
}
