using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bot2048
{
    public class GameController
    {
        public GameController() : this(new GameParameters())
        { }

        public GameController(GameParameters parameters)
        {
            this.parameters = (GameParameters)parameters.Clone();
            rnd = parameters.Random;
            ResetGame();
        }

        private GameParameters parameters;
        private GameGrid _grid;
        private Random rnd;

        public GameGrid GameGrid { get => _grid; }
        public double Score { get => _grid.Score; }
        public bool IsGameOver { get => _grid.IsGameOver; }
        public Vector2 LastPlacedTilePosition { get; private set; } 
        public Tile LastPlacedTile { get; private set; }

        private bool PlaceTile()
        {
            var e = GameGrid.EnumerateTiles().Where(_ => GameGrid[_].IsEmpty);
            if (e.Count() == 0)
                return false;
            LastPlacedTilePosition = e.RandomElement(rnd);
            LastPlacedTile = new Tile(parameters.TileGenerator.Generate(rnd));
            _grid[LastPlacedTilePosition] = LastPlacedTile;
            return true;
        }

        public void ResetGame()
        {
            _grid = new GameGrid(parameters.GridWidth, parameters.GridHeight);
            PlaceTile(); PlaceTile();
        }

        public bool Swipe(Direction direction)
        {
            var wasSwiped = GameGrid.Swipe(direction);
            if (wasSwiped)
                PlaceTile();
            return wasSwiped;
        }
    }
}
