using System.Numerics;

namespace Bot2048
{
    public class GameRecordItem
    {
        private Direction direction;
        private Vector2 spawnPos;
        private int spawnPower;

        public GameRecordItem(Direction direction, Vector2 spawnPos, int spawnPower)
        {
            this.direction = direction;
            this.spawnPos = spawnPos;
            this.spawnPower = spawnPower;
        }

        public Direction Direction { get => direction; set => direction = value; }
        public Vector2 SpawnPos { get => spawnPos; set => spawnPos = value; }
        public int SpawnPower { get => spawnPower; set => spawnPower = value; }
    }
}