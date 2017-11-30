using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot2048.Bots
{
    public class ProvisionBot
    {

        public void Deinitialize()
        {
        }

        private double RateOpportunity(GameGrid g, Direction direction, int recursionLevel)
        {
            var gg = g.Clone();
            if (!gg.Swipe(direction))
                return 0.0;
            else
                return RateSituation(gg, recursionLevel + 1);
        }

        private double RateSituation(GameGrid g, int recursionLevel)
        {
            if (recursionLevel >= 4)
                return 0.0;

            double rating = 0.0;
            if (g.IsGameOver)
                return -100.0;

            rating += g.EnumerateTiles().Select(_ => -g[_].Power).Sum() * 0.8;

            rating += g.EnumerateTiles().Where(_ => g[_].IsEmpty).Count() * 5;

            rating += 0.75 * Enum.GetValues(typeof(Direction)).Cast<Direction>().Where(_ => g.CanSwipe(_))
                .Select(_ => RateOpportunity(g, _, recursionLevel)).Sum();
            
            return rating;
        }

        public Direction GetDirection(GameGrid grid)
        {
            return Extentions.EnumDirections().Where(_ => grid.CanSwipe(_))
                .OrderByDescending(_ => RateOpportunity(grid, _, 0)).First();
        }

        public void Initialize()
        {
        }
    }
}
