using System;

namespace Bot2048
{
    public class TileGeneratorRule : ICloneable
    {
        public TileGeneratorRule(int power, int weight)
        {
            TilePower = power;
            Weight = weight;
        }
        private int tilePower;
        private int weight;
        public int TilePower
        {
            get => tilePower;
            set
            {
                if (value <= 0 || value >= 32)
                    throw new ArgumentOutOfRangeException();
                tilePower = value;
            }
        }
        public int Weight
        {
            get => weight;
            set
            {
                if (value < 0 || value >= 32)
                    throw new ArgumentOutOfRangeException();
                weight = value;
            }
        }

        public object Clone()
        {
            return new TileGeneratorRule(tilePower, weight);
        }
    }
}