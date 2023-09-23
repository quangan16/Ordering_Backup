using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AppOpenAdManager : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/5662855259";
#else
  private string _adUnitId = "unused";
#endif
    private DateTime expireTime;

    public bool IsAdAvailable
    {
        get
        {
            return appOpenAd != null
                   && appOpenAd.CanShowAd()
                   && DateTime.Now < expireTime;
        }
    }

    private AppOpenAd appOpenAd;

    private void Awake()
    {
        LoadAppOpenAd();
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }


    private void OnAppStateChanged(GoogleMobileAds.Common.AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);
        if (state == GoogleMobileAds.Common.AppState.Foreground)
        {
            ShowAdIfAvailable();
        }
    }
    
    /// <summary>
    /// Loads the app open ad.
    /// </summary>
    public void LoadAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        AppOpenAd.Load(_adUnitId, ScreenOrientation.AutoRotation, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());

                appOpenAd = ad;
                expireTime = DateTime.Now + TimeSpan.FromHours(4);
                RegisterEventHandlers(ad);
                RegisterReloadHandler(ad);
            });
    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () => { Debug.Log("App open ad recorded an impression."); };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () => { Debug.Log("App open ad was clicked."); };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () => { Debug.Log("App open ad full screen content opened."); };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadAppOpenAd();
            Debug.Log("App open ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadAppOpenAd();
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    private void RegisterReloadHandler(AppOpenAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App open ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadAppOpenAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadAppOpenAd();
        };
    }

    public void ShowAdIfAvailable()
    {
        if (!IsAdAvailable)
        {
            LoadAppOpenAd();
            //return;
        }

        appOpenAd.Show();
    }
}