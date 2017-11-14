using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Bot2048
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    public class GameGrid : ICloneable
    {
        public GameGrid(int width, int height)
        {
            gameArea = new Tile[width, height];

            foreach (var tile in EnumerateTiles())
                this[tile] = new Tile(0);
        }

        private Tile[,] gameArea;

        public Tile this[int i, int j]
        {
            get
            {
                return gameArea[i, j];
            }
            set
            {
                gameArea[i, j] = value;
            }
        }

        public Tile this[Vector2 v]
        {
            get
            {
                return this[(int)v.X, (int)v.Y];
            }
            set
            {
                this[(int)v.X, (int)v.Y] = value;
            }
        }

        public int Width { get { return gameArea.GetLength(0); } }
        public int Height { get { return gameArea.GetLength(1); } }
        public Vector2 Size { get { return new Vector2(Width, Height); } }
        public int Score { get; private set; }
        public bool IsGameOver
        {
            get
            {
                foreach (var d in new Direction[] 
                    { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
                {
                    GameGrid g = this.Clone();
                    if (g.Swipe(d))
                        return false;
                }
                return true;
            }
        }

        public bool Swipe(Direction direction)
        {
            Vector2 v10, v20, v11, v21, dv1, dv2;

            switch (direction)
            {
                case Direction.Up:
                    v10 = new Vector2(0, 0);
                    dv1 = new Vector2(1, 0);
                    v11 = new Vector2(Width, 0);

                    v20 = new Vector2(0, 1);
                    dv2 = new Vector2(0, 1);
                    v21 = new Vector2(0, Height);
                    break;
                case Direction.Down:
                    v10 = new Vector2(0, 0);
                    dv1 = new Vector2(1, 0);
                    v11 = new Vector2(Width, 0);

                    v20 = new Vector2(0, Height - 2);
                    dv2 = new Vector2(0, -1);
                    v21 = new Vector2(0, -1);
                    break;
                case Direction.Left:
                    v10 = new Vector2(0, 0);
                    dv1 = new Vector2(0, 1);
                    v11 = new Vector2(0, Height);

                    v20 = new Vector2(1, 0);
                    dv2 = new Vector2(1, 0);
                    v21 = new Vector2(Width, 0);
                    break;
                case Direction.Right:
                    v10 = new Vector2(0, 0);
                    dv1 = new Vector2(0, 1);
                    v11 = new Vector2(0, Height);

                    v20 = new Vector2(Width - 2, 0);
                    dv2 = new Vector2(-1, 0);
                    v21 = new Vector2(-1, 0);
                    break;
                default:
                    throw new NotImplementedException();
            }

            bool result = false;
            for (Vector2 v1 = v10; v1 != v11; v1 += dv1)
            {
                for (Vector2 v2 = v20; v2 != v21; v2 += dv2)
                {
                    Vector2 v = v1 + v2;
                    if (this[v].IsEmpty)
                        continue;
                    if (this[v - dv2].IsEmpty)
                    {
                        this[v - dv2] = this[v];
                        this[v] = new Tile(0);
                        v2 = v20 - dv2;
                        result = true;
                        continue;
                    }
                    if (this[v - dv2] == this[v] && !(this[v - dv2].WasStacked || this[v].WasStacked))
                    {
                        var b = this[v - dv2];
                        b.Increment();
                        b.WasStacked = true;
                        this[v - dv2] = b;

                        this[v] = new Tile(0);
                        v2 = v20 - dv2;
                        result = true;
                        Score += b.Power;
                        continue;
                    }
                }

            }

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                {
                    var b = this[i, j];
                    b.WasStacked = false;
                    this[i, j] = b;
                }
            return result;
        }

        public IEnumerable<Vector2> EnumerateTiles()
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    yield return new Vector2(i, j);
        }

        public string Dump()
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    sb.AppendFormat("{0:00} ", this[i, j].ToString().PadLeft(4, '.'));
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public GameGrid Clone()
        {
            return new GameGrid(Width, Height)
            {
                gameArea = (Tile[,])gameArea.Clone(),
            };
        }

        object ICloneable.Clone()
        {
            return new GameGrid(Width, Height)
            {
                gameArea = (Tile[,])gameArea.Clone(),
            };
        }
    }
}
