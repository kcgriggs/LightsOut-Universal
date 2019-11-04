using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LightsOut
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        private LightsOutGame game;
        public MainPage()
        {
            this.InitializeComponent();
            game = new LightsOutGame();
            SizeThree.IsChecked = true;
            CreateGrid();
            DrawGrid();
        }

        private void CreateGrid()
        {
            // Remove all previously-existing rectangles
            boardCanvas.Children.Clear();

            int rectSize = (int)boardCanvas.Width / game.GridSize;

            // Turn entire grid on and create rectangles to represent it
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = new Rectangle();
                    SolidColorBrush black = new SolidColorBrush(Windows.UI.Colors.Black);
                    SolidColorBrush white = new SolidColorBrush(Windows.UI.Colors.White);
                    rect.Fill = white;
                    rect.Width = rectSize + 1;
                    rect.Height = rect.Width + 1;
                    rect.Stroke = black;

                    // Store each row and col as a Point
                    rect.Tag = new Point(r, c);
                    rect.Tapped += Rect_Tapped;

                    int x = c * rectSize;
                    int y = r * rectSize;

                    Canvas.SetTop(rect, y);
                    Canvas.SetLeft(rect, x);

                    // Add the new rectangle to the canvas' children
                    boardCanvas.Children.Add(rect);
                }
            }
        }

        private void DrawGrid()
        {
            int index = 0;

            // Set the colors of the rectangles
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = boardCanvas.Children[index] as Rectangle;
                    index++;

                    if (game.GetGridValue(r, c))
                    {
                        // On
                        rect.Fill = Brushes.White;
                        rect.Stroke = Brushes.Black;
                    }
                    else
                    {
                        // Off
                        rect.Fill = Brushes.Black;
                        rect.Stroke = Brushes.White;
                    }
                }
            }
        }

        private void Rect_Tapped(object sender, MouseButtonEventArgs e)
        {
            // Get row and column from Rectangle's Tag
            Rectangle rect = sender as Rectangle;
            var rowCol = (Point)rect.Tag;
            int row = (int)rowCol.X;
            int col = (int)rowCol.Y;

            game.Move(row, col);

            // Redraw the board
            DrawGrid();

            if (game.IsGameOver())
            {
                msgDialog.Show(this, "Congratulations!  You've won!", "Lights Out!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Owner = this;
            about.ShowDialog();
        }

        private void SizeChanged_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            SizeThree.IsChecked = false;
            SizeFive.IsChecked = false;
            SizeSeven.IsChecked = false;
            switch (menuItem.Name)
            {
                case "SizeThree":
                    SizeThree.IsChecked = true;
                    game.GridSize = 3;
                    break;
                case "SizeFive":
                    SizeFive.IsChecked = true;
                    game.GridSize = 5;
                    break;
                case "SizeSeven":
                    SizeSeven.IsChecked = true;
                    game.GridSize = 7;
                    break;
            }

            CreateGrid();
            DrawGrid();
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            game.NewGame();
            DrawGrid();
        }
    }
}
    }
}
