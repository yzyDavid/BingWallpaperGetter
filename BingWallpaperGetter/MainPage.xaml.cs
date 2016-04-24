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
using BingWallpaperGetter;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace BingWallpaperGetter
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TodayButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void YesterdayButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void TwodaysButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ThreedaysBefore_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void EarlierButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void MinusBar_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void PlusBar_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void GoButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
