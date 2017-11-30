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
            List<GameRecord> recs = new List<GameRecord>();
            while (true)
            {
                BotGameController botGame = new BotGameController(new Bots.NaiveBot(), 
                    new GameParameters() { GridSize = new System.Numerics.Vector2(4, 4) });
                var rec = botGame.DoGame();
                if (rec.Score == 0)
                    continue;
                recs.Add(rec);

                Console.WriteLine(rec.Score);
                var avg = recs.Select(_ => _.Score).Sum() / recs.Count;
                Console.WriteLine("AVG/MIN/MAX/DEV: {0:0.0}/{1:0.0}/{2:0.0}/{3:0.0}", 
                    avg,
                    recs.Select(_ => _.Score).Min(),
                    recs.Select(_ => _.Score).Max(),
                    Math.Sqrt(recs.Select(_ => Math.Pow(_.Score - avg, 2)).Sum() 
                    / (recs.Count - 1) / recs.Count));
            }
            //var w = new WPF.MainWindow(rec);
            //w.ShowDialog();
            /*
            GameGrid g = new GameGrid(4, 4);
            foreach (var move in rec)
            {
                g.Swipe(move.Direction);
                g[move.SpawnPos] = new Tile(move.SpawnPower);

                var w = new WPF.MainWindow(g);
                w.ShowDialog();
            }*/
        }
    }
}
