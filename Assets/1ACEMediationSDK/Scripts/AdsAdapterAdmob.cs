using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsAdapterAdmob : MonoBehaviour
{
    public static AdsAdapterAdmob Instance;
    [HideInInspector] public bool AMInitialized;
    private string banner_id = "ca-app-pub-2399819186335414/6993071800";
    private string inter_id = "ca-app-pub-2399819186335414/6120378800";
    private string rw_id = "ca-app-pub-2399819186335414/5244875863";
    private bool banner_loaded;

    public int adscount
    {
        get => PlayerPrefs.GetInt("ads_count");
        set => PlayerPrefs.SetInt("ads_count", value);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(transform.parent.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.parent.gameObject);
#if !UNITY_EDITOR
        if (!Debug.isDebugBuild)
        {
            Debug.unityLogger.logEnabled = false;
        }
#endif
    }

    public void Start()
    {
        // When true all events raised by GoogleMobileAds will be raised
        // on the Unity main thread. The default value is false.
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        // Initialize the Mobile Ads SDK.
        MobileAds.Initialize((initStatus) =>
        {
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        Debug.Log("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        Debug.Log("Adapter: " + className + " is initialized.");
                        break;
                }
            }
            AMInitialized = true;
            // This callback is called once the MobileAds SDK is initialized.
            LoadBanner();
            LoadInterstitialAd();
            LoadRewardedAd();
        });

        AppOpenAdManager.Instance.LoadAppOpenAd();
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    private void OnAppStateChanged(GoogleMobileAds.Common.AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);
        if (state == GoogleMobileAds.Common.AppState.Foreground)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }

    public static void LogAFAndFB(string eventName, string key, string value)
    {
#if !UNITY_EDITOR
        try
        {
            if (FireBaseRemote.initialized)
            {
                FirebaseAnalytics.LogEvent(eventName, key, value);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace);
        }
#endif
    }

    public enum where
    {
        next_level,
        back_to_main,
        get_hint,
        get_live,
        skip_level,
        get_coin,
        multiply_reward_coin,
        
    }

    private float no_touch_duration;

    // private void Update()
    // {
    //     if (Input.GetMouseButton(0))
    //     {
    //         no_touch_duration = 0;
    //         return;
    //     }
    //
    //     no_touch_duration += Time.deltaTime;
    //     if (no_touch_duration > 15)
    //     {
    //         no_touch_duration = 0;
    //         ShowInterstitial(UserData.CurrentLevel, where.no_touch);
    //     }
    // }


    #region banner

    BannerView _bannerView;

    /// <summary>
    /// Creates a 320x50 banner at top of the screen.
    /// </summary>
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(banner_id, AdSize.Banner, AdPosition.Bottom);
        ListenToBannerEvents();
    }

    /// <summary>
    /// Creates the banner view and loads a banner ad.
    /// </summary>
    public void LoadBanner()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    /// <summary>
    /// listen to events the banner may raise.
    /// </summary>
    private void ListenToBannerEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            banner_loaded = true;
            Debug.Log("Banner view loaded an ad with response : "
                      + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                           + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () => { Debug.Log("Banner view recorded an impression."); };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () => { Debug.Log("Banner view was clicked."); };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () => { Debug.Log("Banner view full screen content opened."); };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () => { Debug.Log("Banner view full screen content closed."); };
    }

    /// <summary>
    /// Destroys the ad.
    /// </summary>
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
            banner_loaded = false;
        }
    }

    #endregion

    public void ShowBanner()
    {
        if (banner_loaded)
        {
            _bannerView.Show();
        }
    }

    public void HideBanner()
    {
  
        if (banner_loaded)
        {
            _bannerView.Hide();
        }
    }

    #region interstitial

    private InterstitialAd interstitialAd;
    private int inter_fail_time;

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        // send the request to load the ad.
        InterstitialAd.Load(inter_id, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    inter_fail_time++;
                    Invoke("LoadInterstitialAd", Mathf.Pow(2, inter_fail_time));
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());
                inter_fail_time = 0;
                interstitialAd = ad;
                RegisterInterEventHandlers(ad);
                RegisterInterReloadHandler(ad);
            });
    }

    private void RegisterInterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () => { Debug.Log("Interstitial ad recorded an impression."); };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () => { Debug.Log("Interstitial ad was clicked."); };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () => { Debug.Log("Interstitial ad full screen content opened."); };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () => { Debug.Log("Interstitial ad full screen content closed."); };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    private void RegisterInterReloadHandler(InterstitialAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }

    #endregion

    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void ShowInterstitial(int level, where where)
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
            adscount++;
            if (adscount == 1)
            {
                LogAFAndFB($"unique_user", level.ToString(), level.ToString());
            }

            LogAFAndFB($"level {level} int at {where}", level.ToString(), level.ToString());
            LogAFAndFB($"ads_count {adscount} at {where}", level.ToString(), level.ToString());
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    #region rw

    private RewardedAd rewardedAd;

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(rw_id, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterRewardEventHandlers(ad);
                RegisterRewardReloadHandler(ad);
            });
    }

    private void RegisterRewardEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () => { Debug.Log("Rewarded ad recorded an impression."); };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () => { Debug.Log("Rewarded ad was clicked."); };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () => { Debug.Log("Rewarded ad full screen content opened."); };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () => { Debug.Log("Rewarded ad full screen content closed."); };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    private void RegisterRewardReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }

    #endregion

    public void ShowRewardedVideo(Action onComplete, Action onFail, int level, where where)
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                onComplete?.Invoke();
                adscount++;
                if (adscount == 1)
                {
                    LogAFAndFB($"unique_user", level.ToString(), level.ToString());
                }

                LogAFAndFB($"level {level} rw at {where}", level.ToString(), level.ToString());
                LogAFAndFB($"ads_count {adscount} at {where}", level.ToString(), level.ToString());
            });
        }
        else
        {
            onFail?.Invoke();
        }
    }
}