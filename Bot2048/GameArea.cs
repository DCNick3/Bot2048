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

    public class GameArea
    {
        public GameArea()
        {
            gameArea = new int[4, 4];
        }

        private int[,] gameArea;

        public int this[int i, int j]
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

        public int this[Vector2 v]
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

        public void Swipe(Direction direction)
        {
            Vector2 v10, v20, v11, v21, dv1, dv2;

            switch (direction)
            {
                case Direction.Up:
                    v10 = new Vector2(0, 0);
                    dv1 = new Vector2(1, 0);
                    v11 = new Vector2(gameArea.GetLength(0), 0);

                    v20 = new Vector2(0, 1);
                    dv2 = new Vector2(0, 1);
                    v21 = new Vector2(0, gameArea.GetLength(1));
                    break;
                case Direction.Down:
                    v10 = new Vector2(0, 0);
                    dv1 = new Vector2(1, 0);
                    v11 = new Vector2(gameArea.GetLength(0), 0);

                    v20 = new Vector2(0, gameArea.GetLength(1) - 2);
                    dv2 = new Vector2(0, -1);
                    v21 = new Vector2(0, -1);
                    break;
                case Direction.Left:
                    v10 = new Vector2(0, 0);
                    dv1 = new Vector2(0, 1);
                    v11 = new Vector2(0, gameArea.GetLength(1));

                    v20 = new Vector2(1, 0);
                    dv2 = new Vector2(1, 0);
                    v21 = new Vector2(gameArea.GetLength(0), 0);
                    break;
                case Direction.Right:
                    v10 = new Vector2(0, 0);
                    dv1 = new Vector2(0, 1);
                    v11 = new Vector2(0, gameArea.GetLength(1));

                    v20 = new Vector2(gameArea.GetLength(1) - 2, 0);
                    dv2 = new Vector2(-1, 0);
                    v21 = new Vector2(-1, 0);
                    break;
                default:
                    throw new NotImplementedException();
            }

            for (Vector2 v1 = v10; v1 != v11; v1 += dv1)
            {
                int c = 0;
                for (Vector2 v2 = v20; v2 != v21; v2 += dv2)
                {
                    Vector2 v = v1 + v2;
                    if (this[v] == 0)
                        continue;
                    if (this[v - dv2] == 0)
                    {
                        this[v - dv2] = this[v];
                        this[v] = 0;
                        v2 = v20 - dv2;
                        continue;
                    }
                    if (c < 2 && this[v - dv2] == this[v])
                    {
                        this[v - dv2]++;
                        this[v] = 0;
                        v2 = v20 - dv2;
                        c++;
                        continue;
                    }
                }
            }
        }

        public string Dump()
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < gameArea.GetLength(1); j++)
            {
                for (int i = 0; i < gameArea.GetLength(0); i++)
                {
                    sb.AppendFormat("{0:00} ", this[i, j]);
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
