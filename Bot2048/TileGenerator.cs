using System;
using System.Collections.Generic;

namespace Bot2048
{
    public class TileGenerator : ICloneable
    {
        public TileGenerator()
        {
            
        }

        private int _weight_sum;
        private List<TileGeneratorRule> rules = new List<TileGeneratorRule>();

        public int Generate(Random rnd)
        {
            int w = rnd.Next(_weight_sum);
            int i = 0;
            while (i < rules.Count && w > rules[i].Weight)
            {
                w -= rules[i].Weight;
                i++;
            }
            return rules[i].TilePower;
        }

        public TileGenerator AddRule(int tilePower, int weight)
        {
            return AddRule(new TileGeneratorRule(tilePower, weight));
        }

        public TileGenerator AddRule(TileGeneratorRule rule)
        {
            _weight_sum += rule.Weight;
            rules.Add(rule);
            return this;
        }

        public object Clone()
        {
            var x = new TileGenerator();
            rules.ForEach(_ => x.AddRule((TileGeneratorRule)_.Clone()));
            return x;
        }

        public static TileGenerator Default
        {
            get
            {
                return new TileGenerator().AddRule(1, 3).AddRule(2, 1);
            }
        }
    }
}