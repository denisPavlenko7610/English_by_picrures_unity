using UnityEngine;
using GoogleMobileAds.Api;

namespace EnglishByPictures.Ads
{
    public class Banner : MonoBehaviour
    {
        private BannerView bannerView;

        public void Start()
        {
            MobileAds.Initialize(initStatus => { });
            RequestBanner();
        }

        private void RequestBanner()
        {
#if UNITY_EDITOR
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_ANDROID
        string adUnitId = Secret.BannerID;
#endif
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
        }
    }
}