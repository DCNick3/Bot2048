using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot2048
{
    public class BotGameController
    {
        public BotGameController(IGameBot bot, GameParameters parameters)
        {
            this.bot = bot;
            controller = new GameController(parameters);
            this.parameters = parameters;
        }

        private GameParameters parameters;
        private GameController controller;
        private IGameBot bot;

        public GameRecord DoGame()
        {
            GameRecord r = new GameRecord(parameters);
            bot.Initialize();
            controller.ResetGame();
            foreach (var t in controller.GameGrid.EnumerateTiles().Where(_ => !controller.GameGrid[_].IsEmpty))
                r.AddEntry(Direction.Up, t, controller.GameGrid[t].Power);

            while (!controller.IsGameOver)
            {
                var dir = bot.GetDirection(controller.GameGrid);
                if (controller.Swipe(dir))
                    r.AddEntry(dir, controller.LastPlacedTilePosition, controller.LastPlacedTile.Power);
            }
            r.Verify();
            bot.Deinitialize();
            return r;
        }
    }
}
