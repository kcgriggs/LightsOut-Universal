using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Popups;
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
        SettingsPage settingsPage;
        private string json;
        private SolidColorBrush tileColor;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            game = new LightsOutGame();
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("json"))
            {
                json = ApplicationData.Current.LocalSettings.Values["json"] as String;
                game = JsonConvert.DeserializeObject<LightsOutGame>(json);
            }
            tileColor = new SolidColorBrush(Windows.UI.Colors.White);
            CreateGrid();
            DrawGrid();
        }

        private void CreateGrid()
        {
            // Remove all previously-existing rectangles
            boardCanvas.Children.Clear();

            int rectSize = Convert.ToInt32(boardCanvas.Width / game.GridSize);

            // Turn entire grid on and create rectangles to represent it
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = new Rectangle();
                    SolidColorBrush black = new SolidColorBrush(Windows.UI.Colors.Black);
                    rect.Fill = tileColor;
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
            SolidColorBrush black = new SolidColorBrush(Windows.UI.Colors.Black);
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
                        rect.Fill = tileColor;
                        rect.Stroke = black;
                    }
                    else
                    {
                        // Off
                        rect.Fill = black;
                        rect.Stroke = tileColor;
                    }
                }
            }
        }

        async private void Rect_Tapped(object sender, TappedRoutedEventArgs e)
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
                MessageDialog msgDialog = new MessageDialog("Congratulations! You Won!", "Lights Out");
                msgDialog.Commands.Add(new UICommand("OK"));
                await msgDialog.ShowAsync();
            }
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            game.NewGame();
            DrawGrid();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["gridSize"] = game.GridSize;
            json = JsonConvert.SerializeObject(game);
            ApplicationData.Current.LocalSettings.Values["json"] = json;
            ApplicationData.Current.LocalSettings.Values["tileColor"] = tileColor.Color.ToString();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (game.GridSize != Convert.ToInt32(ApplicationData.Current.LocalSettings.Values["gridSize"]))
            {
                game.GridSize = Convert.ToInt32(ApplicationData.Current.LocalSettings.Values["gridSize"]);
                CreateGrid();
                DrawGrid();
                json = null;
            }
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("tileColor"))
            {
                string hexColor = ApplicationData.Current.LocalSettings.Values["tileColor"] as String;
                if (tileColor.Color != ConvertHexToBrush(hexColor).Color)
                {
                    tileColor.Color = ConvertHexToBrush(hexColor).Color;
                    DrawGrid();
                }
            }
            if (json != null)
            {
                game = JsonConvert.DeserializeObject<LightsOutGame>(json);
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private SolidColorBrush ConvertHexToBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush newBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return newBrush;

        }
    }
}
