using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bot2048;

namespace Bot2048.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(GameRecord g)
        {
            InitializeComponent();
            record = g;
            recordEnumerator = g.GetEnumerator();
            grid = new GameGrid(4, 4);
        }

        GameGrid grid;
        GameRecord record;
        IEnumerator<GameRecordItem> recordEnumerator;

        private Color[] tileColors = new Color[]
        {
            Color.FromRgb(204, 192, 179),
            Color.FromRgb(238, 228, 218),
            Color.FromRgb(237, 224, 100),
            Color.FromRgb(242, 177, 121),
            Color.FromRgb(245, 149, 99),
            Color.FromRgb(245, 124, 95),
            Color.FromRgb(245, 93, 59),
            Color.FromRgb(237, 206, 113),
            Color.FromRgb(237, 204, 97),
            Color.FromRgb(236, 200, 80),
        };
        private Color[] textColors = new Color[]
        {
            Color.FromRgb(119, 110, 101),
            Color.FromRgb(119, 110, 101),
            Color.FromRgb(249, 247, 243),
        };

        private bool NextStep()
        {
            if (recordEnumerator.MoveNext())
            {
                var i = recordEnumerator.Current;
                grid.Swipe(i.Direction);
                grid[i.SpawnPos] = new Tile(i.SpawnPower);
                DrawGrid(grid);
                return true;
            }
            return false;
        }

        private void DrawGrid(GameGrid grid)
        {
            foreach (var t in grid.EnumerateTiles())
            {
                var tile = grid[t];
                SetTile((int)t.X, (int)t.Y, tile.ToString(),
                    tileColors[Math.Min(tileColors.Length - 1, tile.Power)],
                    textColors[Math.Min(textColors.Length - 1, tile.Power)]);
            }
        }

        private void SetTile(int x, int y, string text, Color backgroundColor, Color textColor)
        {
            GetRect(x, y).Fill = new SolidColorBrush(backgroundColor);
            GetTxt(x, y).Foreground = new SolidColorBrush(textColor);
            GetTxt(x, y).Text = text;
        }

        #region crutch~
        private Rectangle GetBRect(int x, int y)
        {
            switch (y * 100 + x)
            {
                case 000:
                    return brect_1_1;
                case 001:
                    return brect_1_2;
                case 002:
                    return brect_1_3;
                case 003:
                    return brect_1_4;

                case 100:
                    return brect_2_1;
                case 101:
                    return brect_2_2;
                case 102:
                    return brect_2_3;
                case 103:
                    return brect_2_4;

                case 200:
                    return brect_3_1;
                case 201:
                    return brect_3_2;
                case 202:
                    return brect_3_3;
                case 203:
                    return brect_3_4;

                case 300:
                    return brect_4_1;
                case 301:
                    return brect_4_2;
                case 302:
                    return brect_4_3;
                case 303:
                    return brect_4_4;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        private Rectangle GetRect(int x, int y)
        {
            switch (y * 100 + x)
            {
                case 000:
                    return rect_1_1;
                case 001:
                    return rect_1_2;
                case 002:
                    return rect_1_3;
                case 003:
                    return rect_1_4;

                case 100:
                    return rect_2_1;
                case 101:
                    return rect_2_2;
                case 102:
                    return rect_2_3;
                case 103:
                    return rect_2_4;

                case 200:
                    return rect_3_1;
                case 201:
                    return rect_3_2;
                case 202:
                    return rect_3_3;
                case 203:
                    return rect_3_4;

                case 300:
                    return rect_4_1;
                case 301:
                    return rect_4_2;
                case 302:
                    return rect_4_3;
                case 303:
                    return rect_4_4;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        private TextBlock GetTxt(int x, int y)
        {
            switch (y * 100 + x)
            {
                case 000:
                    return txt_1_1;
                case 001:
                    return txt_1_2;
                case 002:
                    return txt_1_3;
                case 003:
                    return txt_1_4;

                case 100:
                    return txt_2_1;
                case 101:
                    return txt_2_2;
                case 102:
                    return txt_2_3;
                case 103:
                    return txt_2_4;

                case 200:
                    return txt_3_1;
                case 201:
                    return txt_3_2;
                case 202:
                    return txt_3_3;
                case 203:
                    return txt_3_4;

                case 300:
                    return txt_4_1;
                case 301:
                    return txt_4_2;
                case 302:
                    return txt_4_3;
                case 303:
                    return txt_4_4;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NextStep();
        }
    }
}
