using GoogleMobileAds.Api;
using UnityEngine;

namespace EnglishByPictures.Ads
{
    public static class Interstitial
    {
        public static int CountToShowAd { get; set; }
        private static InterstitialAd _interstitial;
        
        public static void RequestInterstitial()
        {
#if UNITY_EDITOR
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_ANDROID
            string adUnitId = Secret.BannerID;
#else
            string adUnitId = "unexpected_platform";
#endif
            
            _interstitial = new InterstitialAd(adUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            _interstitial.LoadAd(request);
            
            if (_interstitial.IsLoaded()) {
                _interstitial.Show();
            }
        }
    }
}