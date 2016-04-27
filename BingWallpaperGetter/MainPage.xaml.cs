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
        private static string TodayPictureUri = "http://appserver.m.bing.net/BackgroundImageService/TodayImageService.svc/GetTodayImage?dateOffset=-0&urlEncodeHeaders=true&osName=windowsphone&osVersion=8.10&prientation=480x800&deviceName=WP8Device&mkt=zh-CN";

        private static string TodayPictureUriBig = "";

        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width <= 500)
            {
                BackgroundImage.UriSource = new Uri(TodayPictureUri);
            }
            else
            {
                BackgroundImage.UriSource=new Uri(TodayPictureUriBig);
            }
        }

        /// <summary>
        /// ?????
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BackgroundImage.UriSource = new Uri(TodayPictureUri);
        }

        private void TodayButton_OnClick(object sender, RoutedEventArgs e)
        {
//            Frame.Navigate(typeof (DayPicturePage), 0);
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
