using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Bot2048
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var w = new WPF.MainWindow();
            w.ShowDialog();
            return;

            BotGameController botGame = new BotGameController(new Bots.RandomBot(), new GameParameters());
            var rec = botGame.DoGame();

            GameGrid g = new GameGrid(4, 4);
            foreach (var move in rec)
            {
                g.Swipe(move.Direction);
                g[move.SpawnPos] = new Tile(move.SpawnPower);

                Console.WriteLine(g.Dump());
                Console.WriteLine("===");
            }
        }
    }
}
