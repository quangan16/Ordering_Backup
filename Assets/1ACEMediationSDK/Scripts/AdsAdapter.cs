using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;
using ACEMediation;
#if EXISTED_IRON_SOURCE
using GoogleMobileAds.Api;

#endif

public class AdsAdapter : MonoBehaviour
{
    public static AdsAdapter Instance;
    private const bool testAd = false;
    public bool AMInitialized;
    private ACEMediation_Adapter adapter;
    public GameObject canvas_GDPR;

    public int adscount
    {
        get => PlayerPrefs.GetInt("ads_count");
        set => PlayerPrefs.SetInt("ads_count", value);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            Instance = this;
            if (transform.parent)
                DontDestroyOnLoad(transform.parent.gameObject);
#if !UNITY_EDITOR
        if (!Debug.isDebugBuild)
        {
            Debug.unityLogger.logEnabled = false;
        }
#endif
            Init();
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

#if EXISTED_IRON_SOURCE
    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            AMInitialized = true;
            AppOpenAdManager.Instance.LoadAppOpenAd();
        });

        // Listen to application foreground and background events.
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    private void OnDestroy()
    {
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }

    private void OnAppStateChanged(GoogleMobileAds.Common.AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);
        if (AMInitialized && state == GoogleMobileAds.Common.AppState.Foreground)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
#endif

    public void Init()
    {
        //if (!testAd)
        {
#if EXISTED_MAX
            var adapterGo = new GameObject("adapter");
            adapterGo.AddComponent<ACEMediation_MAX_Adapter>();
            adapterGo.transform.SetParent(transform);
            adapter = adapterGo.GetComponent<ACEMediation_Adapter>();
            adapter.Setup(true);
#elif EXISTED_IRON_SOURCE
            InitIronsource();
#endif
        }
    }

#if EXISTED_IRON_SOURCE
    void InitIronsource()
    {
        var adapterGo = new GameObject("adapter");
        adapterGo.AddComponent<ACEMediation_IS_Adapter>();
        adapterGo.transform.SetParent(transform);
        adapter = adapterGo.GetComponent<ACEMediation_Adapter>();

        if (!PopupGDPR.rate_gdpr)
        {
            var canvasGDPR = Instantiate(canvas_GDPR);
            canvasGDPR.GetComponentInChildren<PopupGDPR>().onComplete += () =>
            {
                Debug.Log("init ironsource");
                adapter.Setup(true);
            };
        }
        else
        {
            if (PopupGDPR.rate_gdpr_value)
            {
                IronSource.Agent.setConsent(true);
            }
            else
            {
                IronSource.Agent.setConsent(false);
            }

            Debug.Log("init ironsource");
            adapter.Setup(true);
        }
    }

#endif
    public void ShowBanner()
    {
        //if (!testAd)
        {
            adapter.ShowBanner();
        }
    }

    public void HideBanner()
    {
        //if (!testAd)
        {
            adapter.HideBanner();
        }
    }

    public void ShowInterstitial(int level, where where)
    {
        //if (!testAd)
        {
#if UNITY_ANDROID
            //FirebaseAnalytics.LogEvent("watch_ad", "times", adscount);
#endif
            adapter.ShowInterstitial();
            adscount++;
            if (adscount == 1)
            {
                LogAFAndFB($"unique_user", level.ToString(), level.ToString());
            }

            LogAFAndFB($"level {level} int at {where}", level.ToString(), level.ToString());
            LogAFAndFB($"ads_count {adscount} at level {level} at {where}", level.ToString(), level.ToString());
        }
    }

    public enum where
    {
        // home,
        // buy_skin,
        // get_coin,
        // main_game,
        // main_game_win,
        // main_game_lose,
        // main_game_add_damage,
        // main_game_play_level_bonus,
        // main_game_try_skin,
        // main_game_free_skin,
        // main_game_skip_level,
        // main_game_get_coin_piggy_bank,
        // main_game_try_again_level_challenge,
        // main_game_get_key,
        //
        // tree_game,
        // tree_game_win,
        // tree_game_lose,
        //
        // treasure_game,
        // treasure_game_win,
        // treasure_game_lose,
        //
        // boss_game,
        // boss_game_win,
        // boss_game_lose,
        //
        // area_game,
        // area_game_win,
        // area_game_lose,
        // area_game_add_power,
        // area_game_go_home,
        //
        // merge_game,
        // merge_game_win,
        // merge_game_lose,
        // merge_game_gift_popup,
        // merge_game_daily_reward,
        // merge_game_new_ally,
        // merge_game_buy_animal,
        // merge_game_buy_animal_ingame,
        // merge_game_buy_human,
        // merge_game_home_buy_coin,
        
        get_coins,
        next_level,
        back_to_main,
    }

    public void ShowRewardedVideo(Action onComplete, Action onFail, int level, where where)
    {
        //if (!testAd)
        {
#if UNITY_ANDROID
            //FirebaseAnalytics.LogEvent("watch_ad", "times", adscount);
#endif
            onComplete += () =>
            {
                adscount++;
                if (adscount == 1)
                {
                    LogAFAndFB($"unique_user", level.ToString(), level.ToString());
                }

                LogAFAndFB($"level {level} rw at {where}", level.ToString(), level.ToString());
                LogAFAndFB($"ads_count {adscount} at level {level} at {where}", level.ToString(), level.ToString());
            };

            adapter.ShowRewardedAd(onComplete, onFail);
        }
    }
}