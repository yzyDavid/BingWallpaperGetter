using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace BingWallpaperGetter
{
    /// <summary>
    /// This class contains informations of a single picture.
    /// </summary>
    public class PictureInfo
    {
        public List<string> Hotspot { get; set; }

        public string ImgTitle { get; private set; }

        public Uri ImageUri { get; private set; }

        public BitmapImage Image { get; set; }

        public string CountryCode { get; private set; }

        public PictureInfo(string countryCode, string imgTitle, string imgUri)
        {
            CountryCode = countryCode;
            ImgTitle = imgTitle;
            ImageUri = new Uri(imgUri);
        }

        private PictureInfo()
        {
        }
    }

    public class ProgressEventArgs : EventArgs
    {
        public int ProgressValue { get; set; }

        public bool IsComplete { get; set; }

        public bool IsException { get; set; }

        public string ExceptionInfo { get; set; }

        public List<PictureInfo> Pictures { get; set; } 
    }

    /// <summary>
    /// a tool class for get wallpaper info.
    /// it is designed as singleton in the book.
    /// </summary>
    public static class WallpaperService
    {
        private static int SelectedDay { get; set; }

        private static List<string> Countries { get; set; }
        = new List<string>(new string[]
        {
            "zh-CN",
            "fr-FR",
            "de-DE",
            "en-US",
            "ja-JP",
            "en-GB"
        });

        private static uint HttpTimesToRequest { get; set; }

        private static uint HttpTimesTotal { get; set; }

        private static bool IsDownloading { get; set; } = false;

        //a list in dictionary? why?
        private static Dictionary<uint, List<PictureInfo>> AllDownloadedPictures { get; set; }

        public static EventHandler<ProgressEventArgs> GetOneDayWallpapersProgressEventHandler { get; set; }

        private static void OnGetOneDayWallpapersProgressEvent(ProgressEventArgs progressEventArgs)
        {
            //WARNING not knowing if the sender is null will work.
            GetOneDayWallpapersProgressEventHandler?.Invoke(null ,progressEventArgs);
        }

        //staic class CAN have a ctor?
        static WallpaperService()
        {
            AllDownloadedPictures = new Dictionary<uint, List<PictureInfo>>();
        }
    }
}
