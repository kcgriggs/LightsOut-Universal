﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightsOut
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("gridSize"))
            {
                int gridSize = Convert.ToInt32(ApplicationData.Current.LocalSettings.Values["gridSize"]);
                switch (gridSize)
                {
                    case 3:
                        x3RadioButton.IsChecked = true;
                        break;
                    case 4:
                        x4RadioButton.IsChecked = true; 
                        break;
                    case 5:
                        x5RadioButton.IsChecked = true; 
                        break;
                }
            }
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("tileColor"))
            {
                string hexColor = ApplicationData.Current.LocalSettings.Values["tileColor"] as String;
                TileColorPicker.Color = ConvertHexToBrush(hexColor).Color;
            }

        }

        private void X3RadioButton_Click(object sender, RoutedEventArgs e)
        {
                ApplicationData.Current.LocalSettings.Values["gridSize"] = 3;
                this.x4RadioButton.IsChecked = false;
                this.x5RadioButton.IsChecked = false;
        }

        private void X4RadioButton_Click(object sender, RoutedEventArgs e)
        {
                ApplicationData.Current.LocalSettings.Values["gridSize"] = 4;
                this.x3RadioButton.IsChecked = false;
                this.x5RadioButton.IsChecked = false;
        }

        private void X5RadioButton_Click(object sender, RoutedEventArgs e)
        {
                ApplicationData.Current.LocalSettings.Values["gridSize"] = 5;
                this.x3RadioButton.IsChecked = false;
                this.x4RadioButton.IsChecked = false;
        }

        private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            ApplicationData.Current.LocalSettings.Values["tileColor"] = TileColorPicker.Color.ToString();
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
