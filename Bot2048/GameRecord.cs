using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bot2048
{
    public class GameRecord : IEnumerable<GameRecordItem>
    {
        public GameRecord(GameParameters parameters)
        {
            _record = new List<GameRecordItem>();
            this.parameters = parameters;
        }

        private GameParameters parameters;
        private List<GameRecordItem> _record;
        private double score;
        private bool isDataValid = false;
        private bool isValid;

        public double Score { get { if (!isDataValid) Verify(); return score; } }
        public bool IsValid { get { if (!isDataValid) Verify(); return isValid; } }

        public IEnumerator<GameRecordItem> GetEnumerator()
        {
            return _record.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _record.GetEnumerator();
        }

        public void AddEntry(Direction direction, Vector2 spawnPos, int spawnPower)
        {
            AddEntry(new GameRecordItem(direction, spawnPos, spawnPower));
        }

        public void AddEntry(GameRecordItem recordItem)
        {
            _record.Add(recordItem);
        }

        private bool VerifyInternal()
        {
            score = 0;
            GameGrid gr = new GameGrid(parameters.GridWidth, parameters.GridHeight);
            foreach (var i in _record)
            {
                gr.Swipe(i.Direction);
                if (!gr[i.SpawnPos].IsEmpty)
                    return false;
                gr[i.SpawnPos] = new Tile(i.SpawnPower);
            }
            score = gr.Score;
            return true;
        }

        public bool Verify()
        {
            isValid = VerifyInternal();
            isDataValid = true;
            return IsValid;
        }
    }
}
