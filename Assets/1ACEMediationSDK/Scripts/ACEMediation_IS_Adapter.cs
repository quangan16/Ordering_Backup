using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if EXISTED_IRON_SOURCE
using AppsFlyerSDK;
#endif

namespace ACEMediation
{
#if EXISTED_IRON_SOURCE
    public class ACEMediation_IS_Adapter : ACEMediation_Adapter
    {
#if UNITY_ANDROID
        public const string appKey = "16ba6c6cd";
#elif UNITY_IPHONE
        public const string appKey = "16ba6c6cd";
#else
        public const string appKey = "unexpected_platform";
#endif
        public override void Setup(bool isInterBackupForReward)
        {
            IronSource.Agent.setMetaData("is_child_directed", "false");
            IronSource.Agent.shouldTrackNetworkState(true);
            this.isInterBackupForReward = isInterBackupForReward;
            //Dynamic config example
            IronSourceConfig.Instance.setClientSideCallbacks(true);

            string id = IronSource.Agent.getAdvertiserId();
            Debug.Log("unity-script: IronSource.Agent.getAdvertiserId : " + id);

            Debug.Log("unity-script: IronSource.Agent.validateIntegration");
            IronSource.Agent.validateIntegration();

            Debug.Log("unity-script: unity version" + IronSource.unityVersion());

            // Add Banner Events
            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
            IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
            IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
            IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
            IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

            // Add Interstitial Events
            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

            IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;
            IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;

            // SDK init
            Debug.Log("unity-script: IronSource.Agent.init");
            IronSource.Agent.init(appKey);
            IronSourceAdQuality.Initialize(appKey);
            IronSourceEvents.onSdkInitializationCompletedEvent += OnSDKInitCompleted;
            //IronSource.Agent.init (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
            //IronSource.Agent.initISDemandOnly (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);

            //Set User ID For Server To Server Integration
            //// IronSource.Agent.setUserId ("UserId");

            // Load Banner example
            //IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
            //LoadInterstitialButtonClicked();
        }

        private void OnSDKInitCompleted()
        {
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
            LoadInterstitialButtonClicked();
        }
        private void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
        {
        }

        private void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
        {
            Debug.Log("unity-script:  ImpressionSuccessEvent impressionData = " + impressionData);
            if (impressionData != null)
            {
                var revenue = impressionData.revenue.HasValue ? impressionData.revenue.Value : 0;
                
                var impressionParameters = new[] {
                    new Firebase.Analytics.Parameter("ad_platform", "ironSource"),
                    new Firebase.Analytics.Parameter("ad_source", impressionData.adNetwork),
                    new Firebase.Analytics.Parameter("ad_unit_name", impressionData.adUnit),
                    new Firebase.Analytics.Parameter("ad_format", impressionData.instanceName),
                    new Firebase.Analytics.Parameter("value", revenue),
                    new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
                };
                Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
                
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ad_platform", "ironSource");
                dic.Add("ad_source", impressionData.adNetwork);
                dic.Add("ad_unit_name", impressionData.adUnit);
                dic.Add("ad_format", impressionData.instanceName);
                dic.Add("value", revenue.ToString());
                dic.Add("currency", "USD"); // All AppLovin revenue is sent in USD
                AppsFlyerAdRevenue.logAdRevenue("ironSource",
                    AppsFlyerAdRevenueMediationNetworkType.AppsFlyerAdRevenueMediationNetworkTypeIronSource, revenue,
                    "USD", dic);
                
                ISAdQualityCustomMediationRevenue customMediationRevenue = new ISAdQualityCustomMediationRevenue();
                customMediationRevenue.MediationNetwork = ISAdQualityMediationNetwork.LEVEL_PLAY;
                ISAdQualityAdType adType;
                var unit = impressionData.adUnit.ToLower();
                if (unit.Contains("banner"))
                {
                    adType = ISAdQualityAdType.BANNER;
                }
                else if (unit.Contains("interstitial"))
                {
                    adType = ISAdQualityAdType.INTERSTITIAL;
                }
                else adType = ISAdQualityAdType.REWARDED;
                customMediationRevenue.AdType = adType;
                customMediationRevenue.Revenue = revenue;
                IronSourceAdQuality.SendCustomMediationRevenue(customMediationRevenue);
            }
        }

        void OnApplicationPause(bool isPaused)
        {
            Debug.Log("unity-script: OnApplicationPause = " + isPaused);
            IronSource.Agent.onApplicationPause(isPaused);
        }

        void BannerAdLoadedEvent()
        {
            Debug.Log("unity-script: I got BannerAdLoadedEvent");
            banner_status = ads_status.loaded;
        }

        void BannerAdLoadFailedEvent(IronSourceError error)
        {
            Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " +
                      error.getDescription());
            banner_status = ads_status.load_failed;
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        }

        void BannerAdClickedEvent()
        {
            Debug.Log("unity-script: I got BannerAdClickedEvent");
        }

        void BannerAdScreenPresentedEvent()
        {
            Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
            banner_status = ads_status.showing;
        }

        void BannerAdScreenDismissedEvent()
        {
            Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
            banner_status = ads_status.hide;
        }

        void BannerAdLeftApplicationEvent()
        {
            Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
        }

        /************* Interstitial API *************/
        public void LoadInterstitialButtonClicked()
        {
            Debug.Log("unity-script: LoadInterstitialButtonClicked");
            IronSource.Agent.loadInterstitial();

            //DemandOnly
            // LoadDemandOnlyInterstitial ();
            inter_status = ads_status.loading;
        }

        /************* Interstitial Delegates *************/
        void InterstitialAdReadyEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdReadyEvent");
            inter_status = ads_status.loaded;
        }

        void InterstitialAdLoadFailedEvent(IronSourceError error)
        {
            Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() +
                      ", description : " + error.getDescription());
            inter_status = ads_status.not_ready;
            LoadInterstitialButtonClicked();
        }

        void InterstitialAdShowSucceededEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
        }

        void InterstitialAdShowFailedEvent(IronSourceError error)
        {
            Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() +
                      ", description : " + error.getDescription());
            LoadInterstitialButtonClicked();
            onVideoFail?.Invoke();
            onVideoComplete = null;
            onVideoFail = null;
        }

        void InterstitialAdClickedEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdClickedEvent");
        }

        void InterstitialAdOpenedEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
        }

        void InterstitialAdClosedEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdClosedEvent");
            LoadInterstitialButtonClicked();
            onVideoComplete?.Invoke();
            onVideoComplete = null;
            onVideoFail = null;
        }

        /************* RewardedVideo API *************/
        /************* RewardedVideo Delegates *************/
        void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
        {
            Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
            if (canShowAd)
            {
                reward_status = ads_status.loaded;
            }
            else
            {
                reward_status = ads_status.loading;
            }
        }

        void RewardedVideoAdOpenedEvent()
        {
            Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");
        }

        void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
        {
            Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() +
                      " name = " + ssp.getRewardName());
            onVideoComplete?.Invoke();
            onVideoComplete = null;
            onVideoFail = null;
        }

        void RewardedVideoAdClosedEvent()
        {
            Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
        }

        void RewardedVideoAdStartedEvent()
        {
            Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
        }

        void RewardedVideoAdEndedEvent()
        {
            Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
        }

        void RewardedVideoAdShowFailedEvent(IronSourceError error)
        {
            Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() +
                      ", description : " + error.getDescription());
            onVideoFail?.Invoke();
            onVideoComplete = null;
            onVideoFail = null;
        }

        void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
        {
            Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
        }


        public override void ShowInterstitial()
        {
            Debug.Log("unity-script: ShowInterstitialButtonClicked");
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
            }
            else
            {
                Debug.Log("unity-script: IronSource.Agent.isInterstitialReady - False");
            }
        }

        public override void ShowRewardedAd(Action onVideoComplete, Action onVideoFail)
        {
            this.onVideoComplete = onVideoComplete;
            this.onVideoFail = onVideoFail;
            Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
            }
            else
            {
                if (isInterBackupForReward && IronSource.Agent.isInterstitialReady())
                {
                    ShowInterstitial();
                }
                else
                {
                    onVideoFail?.Invoke();
                    onVideoComplete = null;
                    onVideoFail = null;
                }

                Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
            }
        }

        public override void ShowBanner()
        {
            IronSource.Agent.displayBanner();
        }

        public override void HideBanner()
        {
            IronSource.Agent.hideBanner();
        }
    }
#endif
}