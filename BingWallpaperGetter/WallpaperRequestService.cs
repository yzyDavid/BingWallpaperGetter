using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Windows.UI.Notifications;
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

        private static int HttpTimesToRequest { get; set; }

        private static int HttpTimesTotal { get; set; }

        private static bool IsDownloading { get; set; } = false;

        //a list in dictionary? why?
        private static Dictionary<int, List<PictureInfo>> AllDownloadedPictures { get; set; }

        public static EventHandler<ProgressEventArgs> GetOneDayWallpapersProgressEventHandler { get; set; }

        private static void OnGetOneDayWallpapersProgressEventHandler(ProgressEventArgs progressEventArgs)
        {
            //WARNING not knowing if the sender is null will work.
            GetOneDayWallpapersProgressEventHandler?.Invoke(null, progressEventArgs);
        }

        //staic class CAN have a ctor?
        static WallpaperService()
        {
            AllDownloadedPictures = new Dictionary<int, List<PictureInfo>>();
        }

        public static void GetOneDayWallpapers(int day)
        {
            if (IsDownloading)
            {
                return;
            }
            IsDownloading = true;
            SelectedDay = day;
            if (!AllDownloadedPictures.Keys.Contains(SelectedDay))
            {
                AllDownloadedPictures.Add(SelectedDay, new List<PictureInfo>());
            }

            string format =
                "http://appserver.m.bing.net/BackgroundImageService/TodayImageService.svc/GetTodayImage?dateOffset=-{0}&urlEncodeHeaders=true&osName=windowsPhone&osVersion=8.10&orientation=480x800&deviceName=WP8Device&mkt={1}";
            HttpTimesTotal = HttpTimesToRequest = Countries.Count<string>();

            foreach (string country in Countries)
            {
                //AllDownloadedPictures[SelectedDay].Select(item => item.CountryCode == country).Count() == 0
                if (!AllDownloadedPictures[SelectedDay].Select(item => item.CountryCode == country).Any())
                {
                    var bingUrlFmt = string.Format(format, SelectedDay, country);
                    var request = WebRequest.Create(bingUrlFmt) as HttpWebRequest;
                    Debug.Assert(request != null);
                    request.Method = "GET";
                    request.BeginGetResponse(result => ResponseHandler(result, request, country, bingUrlFmt), null);
                }
                else
                {
                    SetProgress();
                }
            }
        }

        private static void ResponseHandler(IAsyncResult asyncResult, HttpWebRequest request, string myloc,
            string imgUri)
        {
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse) request.EndGetResponse(asyncResult);
            }
            catch (Exception exception)
            {
                IsDownloading = false;

                OnGetOneDayWallpapersProgressEventHandler(new ProgressEventArgs
                {
                    IsException = true,
                    IsComplete = false,
                    ExceptionInfo = exception.Message,
                    ProgressValue = 0,
                    Pictures = null
                });
                return;
            }

            if (request.HaveResponse)
            {
                var hotspotList = new List<string>();
                var imgTitle = "";
                foreach (var str in response.Headers.AllKeys)
                {
                    var str2 = str;
                    var str3 = response.Headers[str];
                    if (str2.Contains("Image-Info-Credit"))
                    {
                        imgTitle = WebUtility.UrlDecode(str3);
                    }
                    else if (str2.Contains("Image-Info-Hotspot-"))
                    {
                        var strArray = WebUtility.UrlDecode(str3).Replace(" ", "").Split(new char[] {';'});
                        hotspotList.AddRange(strArray);
                    }
                }
                var info = new PictureInfo(myloc, imgTitle, imgUri);
                info.Hotspot = hotspotList;
                AllDownloadedPictures[SelectedDay].Add(info);
            }

            Debug.WriteLine("AllDownloadedPictures[SelectedDay].Count:" + AllDownloadedPictures[SelectedDay].Count);

            SetProgress();
        }

        private static void SetProgress()
        {
            HttpTimesToRequest--;
            bool finish = HttpTimesToRequest == 0;
            if (finish)
            {
                IsDownloading = false;
            }
            OnGetOneDayWallpapersProgressEventHandler(new ProgressEventArgs
            {
                IsException = false,
                IsComplete = finish,
                ExceptionInfo = "",
                ProgressValue = (int) (((float) (HttpTimesTotal - HttpTimesToRequest)/(float) HttpTimesTotal)*100),
                Pictures = AllDownloadedPictures[SelectedDay]
            });
        }
    }
}
