using System;
using System.Collections.Generic;
using System.Numerics;

namespace Bot2048
{
    public class GameParameters : ICloneable
    {
        public GameParameters()
        {
            TileGenerator = TileGenerator.Default;
            Random = new Random();
            GridHeight = 4;
            GridWidth = 4;
        }

        private int h, w;

        public int GridHeight { get => h; set => h = value; }
        public int GridWidth { get => w; set => w = value; }
        public Random Random { get; set; }

        public object Clone()
        {
            return new GameParameters()
            {
                h = h, w = w,
                TileGenerator = (TileGenerator)TileGenerator.Clone(),
                Random = Random,
            };
        }

        public Vector2 GridSize
        {
            get => new Vector2(w, h);
            set { w = (int)value.X; h = (int)value.Y; }
        }
        
        public TileGenerator TileGenerator { get; set; }
    }
}