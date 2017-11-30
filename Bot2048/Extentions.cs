using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot2048
{
    public static class Extentions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable, Random rand)
        {
            int index = rand.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }

        public static IEnumerable<Direction> EnumDirections()
        {
            return Enum.GetValues(typeof(Direction)).Cast<Direction>();
        }
    }
}
