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
}
