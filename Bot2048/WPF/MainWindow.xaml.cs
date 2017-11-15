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
using System.Windows.Media.Animation;
using System.Numerics;
using System.Windows.Threading;

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
            Autoplay = false;
            NextStep();
        }

        GameGrid grid;
        GameRecord record;
        IEnumerator<GameRecordItem> recordEnumerator;
        List<DoubleAnimation> translateAnimations = new List<DoubleAnimation>();
        List<TranslateTransform> translateTransforms = new List<TranslateTransform>();
        DispatcherTimer stepTimer;
        private int animationCount;

        public bool Autoplay { get; set; }
        public bool IsAnimationActive { get; private set; }

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
            //if (IsAnimationActive)
              //  return false;
            if (recordEnumerator.MoveNext())
            {
                var i = recordEnumerator.Current;
                grid.Swipe(i.Direction);
                grid[i.SpawnPos] = new Tile(i.SpawnPower);

                IsAnimationActive = true;
                var translations = grid.LastSwipeTranslations;
                

                foreach (var translation in translations)
                {
                    Vector2 d = translation.Item1 + translation.Item2;
                    TranslateTile((int)translation.Item1.X, (int)translation.Item1.Y, (int)d.X, (int)d.Y);
                }
                if (translations.Count == 0)
                    TranslateFinished();
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
            scoreText.Text = string.Format("Score: {0}", grid.Score);
        }

        private void SetTile(int x, int y, string text, Color backgroundColor, Color textColor)
        {
            var rect = GetRect(x, y);
            rect.Fill = new SolidColorBrush(backgroundColor);
            rect.RenderTransform = Transform.Identity;
            rect.SetValue(Panel.ZIndexProperty, 0);
            var txt = GetTxt(x, y);
            txt.Foreground = new SolidColorBrush(textColor);
            txt.Text = text;
            var txtBox = ((Viewbox)txt.Parent);
            txtBox.SetValue(Panel.ZIndexProperty, 0);
            txtBox.RenderTransform = Transform.Identity;
        }

        private void TranslateTile(int x1, int y1, int x2, int y2)
        {
            var rectTransform = new TranslateTransform();
            var rect = GetRect(x1, y1);
            rect.RenderTransform = rectTransform;
            rect.SetValue(Grid.ZIndexProperty, 2);
            var toRect = GetRect(x2, y2);

            var txtBox = (Viewbox)GetTxt(x1, y1).Parent;
            txtBox.RenderTransform = rectTransform;
            txtBox.SetValue(Grid.ZIndexProperty, 3);

            var time = TimeSpan.FromSeconds(0.2);

            var xRectAnimation = new DoubleAnimation()
            {
                From = 0,
                To = toRect.TranslatePoint(new Point(0, 0), rect).X,
                Duration = new Duration(time),
                DecelerationRatio = 0.5,
                AccelerationRatio = 0.5,
            };

            var yRectAnimation = new DoubleAnimation()
            {
                From = 0,
                To = toRect.TranslatePoint(new Point(0, 0), rect).Y,
                Duration = new Duration(time),
                DecelerationRatio = 0.5,
                AccelerationRatio = 0.5,
            };

            if (xRectAnimation.To != 0)
            {
                xRectAnimation.Completed += rectTranslateAnimation_Completed;
                rectTransform.BeginAnimation(TranslateTransform.XProperty, xRectAnimation);

                animationCount++;
            }

            if (yRectAnimation.To != 0)
            {
                yRectAnimation.Completed += rectTranslateAnimation_Completed;
                rectTransform.BeginAnimation(TranslateTransform.YProperty, yRectAnimation);

                animationCount++;
            }
            

            
            /*var toTxt = GetTxt(x2, y2);

            var xTxtAnimation = new DoubleAnimation()
            {
                From = 0,
                To = toTxt.TranslatePoint(new Point(0, 0), txt).X,
                Duration = new Duration(TimeSpan.FromSeconds(0.8)),
                DecelerationRatio = 0.5,
                AccelerationRatio = 0.5,
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true,
            };

            var yTxtAnimation = new DoubleAnimation()
            {
                From = 0,
                To = toTxt.TranslatePoint(new Point(0, 0), txt).Y,
                Duration = new Duration(TimeSpan.FromSeconds(0.8)),
                DecelerationRatio = 0.5,
                AccelerationRatio = 0.5,
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true,
            };

            txtTransform.BeginAnimation(TranslateTransform.XProperty, xTxtAnimation);
            txtTransform.BeginAnimation(TranslateTransform.YProperty, yTxtAnimation);*/
        }

        private void rectTranslateAnimation_Completed(object sender, EventArgs e)
        {
            TranslateFinished();
        }

        private void TranslateFinished()
        {
            IsAnimationActive = false;
            DrawGrid(grid);
            if (Autoplay)
            {
                //NextStep();
                
            }
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
            if (stepTimer == null)
            stepTimer = new DispatcherTimer(TimeSpan.FromSeconds(0.3), DispatcherPriority.Send, (s, a) =>
            { if (grid.IsGameOver) stepTimer.Stop(); else NextStep(); }, Dispatcher);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Autoplay)
                NextStep();
        }
    }
}
