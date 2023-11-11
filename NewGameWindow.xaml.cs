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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class NewGameWindow : Window
    {
        private Tile[,] tiles = new Tile[4, 4];
        private int score = 0;
        private Random random = new Random();
        private bool isGameOver = false;
        private bool canMove = true;

        public NewGameWindow()
        {
            InitializeComponent();
            InitializeGame();
            this.KeyDown += NewGameWindow_KeyDown;
            this.Focus();
        }

        private void InitializeGame()
        {
            gameGrid.Children.Clear();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Tile tile = new Tile { Value = 0, Row = row, Column = col };
                    tiles[row, col] = tile;
                    gameGrid.Children.Add(CreateTileElement(tile));
                }
            }

            GenerateNewTile();
        }

        private void GenerateNewTile()
        {
            List<Tile> availableTiles = new List<Tile>();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (tiles[row, col].Value == 0)
                    {
                        availableTiles.Add(tiles[row, col]);
                    }
                }
            }

            if (availableTiles.Count > 0)
            {
                Tile randomTile = availableTiles[random.Next(availableTiles.Count)];
                int value = random.Next(10) == 0 ? 4 : 2;

                randomTile.Value = value;
                UpdateTileDisplay(randomTile);
            }
            else
            {
                isGameOver = true;
                canMove = false;
            }
        }

        private void UpdateTileDisplay(Tile tile)
        {
            if (tile.Border != null)
            {
                TextBlock textBlock = tile.Border.Child as TextBlock;
                if (textBlock != null)
                {
                    textBlock.Text = tile.Value > 0 ? tile.Value.ToString() : "";
                }

                tile.Border.Background = GetTileColor(tile.Value);
            }
        }

        private Thickness GetTilePosition(Tile tile)
        {
            double left = 10 + tile.Column * 100;
            double top = 10 + tile.Row * 100;
            return new Thickness(left, top, 0, 0);
        }
        private UIElement CreateTileElement(Tile tile)
        {
            Border border = new Border
            {
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10),
            };

            TextBlock textBlock = new TextBlock
            {
                Text = tile.Value > 0 ? tile.Value.ToString() : "",
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#787065")),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            border.Child = textBlock;

            tile.Border = border;

            return border;
        }

        private SolidColorBrush GetTileColor(int value)
        {
            switch (value)
            {
                case 0:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#CDC1B5"));
                case 2:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEE4DA"));
                case 4:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#ECE0CA"));
                case 8:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#F2B179"));
                case 16:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#EC8D53"));
                case 32:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#008000"));
                case 64:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
                case 128:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#800080"));
                case 256:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FFFF"));
                case 512:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF00FF"));
                case 1024:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#A52A2A"));
                case 2048:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF4500"));
                default:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
            }
        }
        private void NewGameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                MoveTilesUp();
                GenerateNewTile();
            }
            else if (e.Key == Key.Down)
            {
                MoveTilesDown();
                GenerateNewTile();
            }
            else if (e.Key == Key.Left)
            {
                MoveTilesLeft();
                GenerateNewTile();
            }
            else if (e.Key == Key.Right)
            {
                MoveTilesRight();
                GenerateNewTile();
            }
        }

        private void MoveTilesUp()
        {
            bool moved = false;

            for (int col = 0; col < 4; col++)
            {
                for (int row = 1; row < 4; row++)
                {
                    if (tiles[row, col].Value != 0)
                    {
                        int newRow = row;
                        while (newRow > 0 && tiles[newRow - 1, col].Value == 0)
                        {
                            AnimateTileMoveUp(tiles[newRow, col], newRow - 1);

                            tiles[newRow - 1, col].Value = tiles[newRow, col].Value;
                            tiles[newRow, col].Value = 0;
                            newRow--;
                            moved = true;
                        }
                        if (newRow > 0 && tiles[newRow - 1, col].Value == tiles[newRow, col].Value)
                        {
                            AnimateTileMerge(tiles[newRow - 1, col]);

                            tiles[newRow - 1, col].Value *= 2;
                            tiles[newRow, col].Value = 0;

                            UpdateScore(tiles[newRow - 1, col].Value);
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                GenerateNewTile();
            }
        }

        private void MoveTilesDown()
        {
            bool moved = false;

            for (int col = 0; col < 4; col++)
            {
                for (int row = 2; row >= 0; row--)
                {
                    if (tiles[row, col].Value != 0)
                    {
                        int newRow = row;
                        while (newRow < 3 && tiles[newRow + 1, col].Value == 0)
                        {
                            AnimateTileMoveDown(tiles[newRow, col], newRow + 1);
                            tiles[newRow + 1, col].Value = tiles[newRow, col].Value;
                            tiles[newRow, col].Value = 0;
                            newRow++;
                            moved = true;
                        }
                        if (newRow < 3 && tiles[newRow + 1, col].Value == tiles[newRow, col].Value)
                        {
                            AnimateTileMerge(tiles[newRow + 1, col]);
                            tiles[newRow + 1, col].Value *= 2;
                            tiles[newRow, col].Value = 0;
                            UpdateScore(tiles[newRow + 1, col].Value);
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                GenerateNewTile();
            }
        }

        private void MoveTilesLeft()
        {
            bool moved = false;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 1; col < 4; col++)
                {
                    if (tiles[row, col].Value != 0)
                    {
                        int newCol = col;
                        while (newCol > 0 && tiles[row, newCol - 1].Value == 0)
                        {
                            AnimateTileMoveLeft(tiles[row, newCol], newCol - 1);
                            tiles[row, newCol - 1].Value = tiles[row, newCol].Value;
                            tiles[row, newCol].Value = 0;
                            newCol--;
                            moved = true;
                        }
                        if (newCol > 0 && tiles[row, newCol - 1].Value == tiles[row, newCol].Value)
                        {
                            AnimateTileMerge(tiles[row, newCol - 1]);
                            tiles[row, newCol - 1].Value *= 2;
                            tiles[row, newCol].Value = 0;
                            UpdateScore(tiles[row, newCol - 1].Value);
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                GenerateNewTile();
            }
        }

        private void MoveTilesRight()
        {
            bool moved = false;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 2; col >= 0; col--)
                {
                    if (tiles[row, col].Value != 0)
                    {
                        int newCol = col;
                        while (newCol < 3 && tiles[row, newCol + 1].Value == 0)
                        {
                            AnimateTileMoveRight(tiles[row, newCol], newCol + 1);
                            tiles[row, newCol + 1].Value = tiles[row, newCol].Value;
                            tiles[row, newCol].Value = 0;
                            newCol++;
                            moved = true;
                        }
                        if (newCol < 3 && tiles[row, newCol + 1].Value == tiles[row, newCol].Value)
                        {
                            AnimateTileMerge(tiles[row, newCol + 1]);
                            tiles[row, newCol + 1].Value *= 2;
                            tiles[row, newCol].Value = 0;
                            UpdateScore(tiles[row, newCol + 1].Value);
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                GenerateNewTile();
            }
        }

        private void AnimateTileMoveUp(Tile tile, int newRow)
        {
            double yOffset = (newRow - tile.Row) * 100;
            tile.Row = newRow;

            if (tile.RenderTransform == null)
            {
                tile.RenderTransform = new TranslateTransform();
            }

            TranslateTransform translation = (TranslateTransform)tile.RenderTransform;

            DoubleAnimation anim = new DoubleAnimation
            {
                To = -yOffset,
                Duration = TimeSpan.FromMilliseconds(200),
            };

            translation.BeginAnimation(TranslateTransform.YProperty, anim);
        }

        private void AnimateTileMoveDown(Tile tile, int newRow)
        {
            double yOffset = (newRow - tile.Row) * 100;
            tile.Row = newRow;

            if (tile.RenderTransform == null)
            {
                tile.RenderTransform = new TranslateTransform();
            }

            TranslateTransform translation = (TranslateTransform)tile.RenderTransform;

            DoubleAnimation anim = new DoubleAnimation
            {
                To = yOffset,
                Duration = TimeSpan.FromMilliseconds(200),
            };

            translation.BeginAnimation(TranslateTransform.YProperty, anim);
        }

        private void AnimateTileMoveLeft(Tile tile, int newCol)
        {
            double xOffset = (newCol - tile.Column) * 100;
            tile.Column = newCol;

            if (tile.RenderTransform == null)
            {
                tile.RenderTransform = new TranslateTransform();
            }

            TranslateTransform translation = (TranslateTransform)tile.RenderTransform;

            DoubleAnimation anim = new DoubleAnimation
            {
                To = -xOffset,
                Duration = TimeSpan.FromMilliseconds(200),
            };

            translation.BeginAnimation(TranslateTransform.XProperty, anim);
        }

        private void AnimateTileMoveRight(Tile tile, int newCol)
        {
            double xOffset = (newCol - tile.Column) * 100;
            tile.Column = newCol;

            if (tile.RenderTransform == null)
            {
                tile.RenderTransform = new TranslateTransform();
            }

            TranslateTransform translation = (TranslateTransform)tile.RenderTransform;

            DoubleAnimation anim = new DoubleAnimation
            {
                To = xOffset,
                Duration = TimeSpan.FromMilliseconds(200),
            };

            translation.BeginAnimation(TranslateTransform.XProperty, anim);
        }

        private void AnimateTileMerge(Tile tile)
        {
            TranslateTransform translation = tile.RenderTransform as TranslateTransform;

            if (translation == null)
            {
                translation = new TranslateTransform();
                tile.RenderTransform = translation;
            }

            DoubleAnimation animX = new DoubleAnimation
            {
                To = 1.2,
                Duration = TimeSpan.FromMilliseconds(100),
            };

            DoubleAnimation animY = new DoubleAnimation
            {
                To = 1.2,
                Duration = TimeSpan.FromMilliseconds(100),
            };

            animX.Completed += (s, e) =>
            {
                tile.RenderTransform = null;
            };

            translation.BeginAnimation(TranslateTransform.XProperty, animX);
            translation.BeginAnimation(TranslateTransform.YProperty, animY);
        }

        private void UpdateScore(int points)
        {
            score += points;
            scoreTextBlock.Text = score.ToString();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }

    public class Tile
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Border Border { get; set; }
        public TranslateTransform RenderTransform { get; set; }
    }
}
