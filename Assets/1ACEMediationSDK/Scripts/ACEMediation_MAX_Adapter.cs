using System;
using System.Collections.Generic;
#if EXISTED_ACE_SDK
using ACEMediation;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace ACEMediation
{
#if EXISTED_MAX
    public class ACEMediation_MAX_Adapter : ACEMediation_Adapter
    {
        private const string MaxSdkKey = "V3fbyi67QQtWAtUHPG-2SFAsm25n0syesosKHFBnXk-GBsSfS2-dLoAnNiFNIqUCKS86SR_Imyzcc2lDw7ItHm";
        private const string InterstitialAdUnitId = "";
        private const string RewardedAdUnitId = "";
        private const string BannerAdUnitId = "";
        private const string AppOpenAdUnitId = "";

        private int interstitialRetryAttempt;
        private int rewardedRetryAttempt;
        private int rewardedInterstitialRetryAttempt;

        public override void Setup(bool isInterBackupForReward)
        {
            this.isInterBackupForReward = isInterBackupForReward;
            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
            {
                // AppLovin SDK is initialized, configure and start loading ads.
                // Debug.Log("MAX SDK Initialized");

                InitializeInterstitialAds();
                InitializeRewardedAds();
                InitializeBannerAds();

                // Initialize Adjust SDK
            };
            
            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
            {
                MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;
 
                ShowAdIfReady();
            };
            MaxSdk.SetSdkKey(MaxSdkKey);
            MaxSdk.InitializeSdk();
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        }
        
        #region AppOpenAds
        public void ShowAdIfReady()
        {
            #if !UNITY_EDITOR
            if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
            {
                MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
            }
            else
            {
                MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
            }
#endif
        }
        public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        }
 
        private void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                ShowAdIfReady();
            }
        }
        #endregion

        #region Interstitial Ad Methods

        private void InitializeInterstitialAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;

            // Load the first interstitial
            LoadInterstitial();
        }

        void LoadInterstitial()
        {
            inter_status = ads_status.loading;
            MaxSdk.LoadInterstitial(InterstitialAdUnitId);
        }

        public override void ShowInterstitial()
        {
            if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
            {
                inter_status = ads_status.showing;
                MaxSdk.ShowInterstitial(InterstitialAdUnitId);
            }
            else
            {
                inter_status = ads_status.not_ready;
                onVideoFail?.Invoke();
                onVideoComplete = null;
                onVideoFail = null;
            }
        }

        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'

            inter_status = ads_status.loaded;
            Debug.Log("Interstitial loaded");

            // Reset retry attempt
            interstitialRetryAttempt = 0;
        }

        private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            interstitialRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));

            inter_status = ads_status.load_failed;
            Debug.Log("Interstitial failed to load with error code: " + errorInfo.Code);

            Invoke("LoadInterstitial", (float) retryDelay);
        }

        private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo,
            MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad failed to display. We recommend loading the next ad
            Debug.Log("Interstitial failed to display with error code: " + errorInfo.Code);
            LoadInterstitial();
            onVideoFail?.Invoke();
            onVideoComplete = null;
            onVideoFail = null;
        }

        private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is hidden. Pre-load the next ad
            Debug.Log("Interstitial dismissed");
            LoadInterstitial();
            onVideoComplete?.Invoke();
            onVideoComplete = null;
            onVideoFail = null;
        }

        private void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad revenue paid. Use this callback to track user revenue.
            Debug.Log("Interstitial revenue paid");
        }

        #endregion

        #region Rewarded Ad Methods

        private void InitializeRewardedAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;

            // Load the first RewardedAd
            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
            reward_status = ads_status.loading;
            MaxSdk.LoadRewardedAd(RewardedAdUnitId);
        }

        public override void ShowRewardedAd(Action onVideoComplete, Action onVideoFail)
        {
            this.onVideoComplete = onVideoComplete;
            this.onVideoFail = onVideoFail;
            if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
            {
                reward_status = ads_status.showing;
                MaxSdk.ShowRewardedAd(RewardedAdUnitId);
            }
            else
            {
                reward_status = ads_status.not_ready;
                if (isInterBackupForReward)
                {
                    ShowInterstitial();
                }
                else
                {
                    onVideoFail?.Invoke();
                    onVideoComplete = null;
                    onVideoFail = null;
                }
            }
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'

            reward_status = ads_status.loaded;
            Debug.Log("Rewarded ad loaded");

            // Reset retry attempt
            rewardedRetryAttempt = 0;
        }

        private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            rewardedRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));


            reward_status = ads_status.load_failed;
            Debug.Log("Rewarded ad failed to load with error code: " + errorInfo.Code);

            Invoke("LoadRewardedAd", (float) retryDelay);
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo,
            MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad failed to display. We recommend loading the next ad
            Debug.Log("Rewarded ad failed to display with error code: " + errorInfo.Code);
            LoadRewardedAd();
            onVideoFail?.Invoke();
            onVideoComplete = null;
            onVideoFail = null;
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("Rewarded ad displayed");
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("Rewarded ad clicked");
        }

        private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            Debug.Log("Rewarded ad dismissed");
            LoadRewardedAd();
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad was displayed and user should receive the reward
            Debug.Log("Rewarded ad received reward");
            onVideoComplete?.Invoke();
            onVideoComplete = null;
            onVideoFail = null;
        }

        private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad revenue paid. Use this callback to track user revenue.
            Debug.Log("Rewarded ad revenue paid");

            // // Ad revenue
            // double revenue = adInfo.Revenue;
            //
            // // Miscellaneous data
            // string
            //     countryCode =
            //         MaxSdk.GetSdkConfiguration()
            //             .CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
            // string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
            // string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
            // string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
        }

        #endregion

        #region Banner Ad Methods

        private void InitializeBannerAds()
        {
            // Attach Callbacks
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdFailedEvent;
            MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;

            // Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
            // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
            MaxSdk.CreateBanner(BannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);

            // Set background or background color for banners to be fully functional.
            MaxSdk.SetBannerBackgroundColor(BannerAdUnitId, Color.black);
        }


        public override void ShowBanner()
        {
            MaxSdk.ShowBanner(BannerAdUnitId);

            banner_status = ads_status.showing;
        }

        public override void HideBanner()
        {
            MaxSdk.HideBanner(BannerAdUnitId);
            banner_status = ads_status.hide;
        }

        private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Banner ad is ready to be shown.
            // If you have already called MaxSdk.ShowBanner(BannerAdUnitId) it will automatically be shown on the next ad refresh.
            banner_status = ads_status.loaded;
        }

        private void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Banner ad failed to load. MAX will automatically try loading a new ad internally.
        }

        private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
        }

        private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Banner ad revenue paid. Use this callback to track user revenue.

            // Ad revenue
            // double revenue = adInfo.Revenue;
            //
            // // Miscellaneous data
            // string
            //     countryCode =
            //         MaxSdk.GetSdkConfiguration()
            //             .CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
            // string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
            // string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
            // string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
        }

        #endregion

        // Attach callbacks based on the ad format(s) you are using

        private void OnAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
        {
            double revenue = impressionData.Revenue;
            var impressionParameters = new[]
            {
                new Firebase.Analytics.Parameter("ad_platform", "AppLovin"),
                new Firebase.Analytics.Parameter("ad_source", impressionData.NetworkName),
                new Firebase.Analytics.Parameter("ad_unit_name", impressionData.AdUnitIdentifier),
                new Firebase.Analytics.Parameter("ad_format", impressionData.Placement),
                new Firebase.Analytics.Parameter("value", revenue),
                new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
        }
    }
#endif
}