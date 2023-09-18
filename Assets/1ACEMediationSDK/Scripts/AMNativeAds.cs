using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Yêu cầu Admob 8.3 or higher
/// </summary>
public class AMNativeAds : MonoBehaviour
{
    private const string AD_UNIT_ID = "ca-app-pub-3926209354441959/7823251449";

    private NativeAd nativeAd;
    public RawImage AdIconTexture;
    public Text AdHeadline;
    public Text AdDescription;
    public GameObject AdLoaded;
    public GameObject AdLoading;


    private IEnumerator Start()
    {
        yield return new WaitUntil(() => AdsAdapter.Instance.AMInitialized);
        RequestNativeAd();
    }

    private void RequestNativeAd() {
        if (this.nativeAd != null)
        {
            this.nativeAd.Destroy();
            this.nativeAd = null;
        }
        AdLoader adLoader = new AdLoader.Builder(AD_UNIT_ID)
            .ForNativeAd()
            .Build();
        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
        adLoader.LoadAd(new AdRequest.Builder().Build());
    }

    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        Debug.Log("Native ad failed to load: " + args.LoadAdError.ToString());
        RequestNativeAd();
    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args) {
        Debug.Log("Native ad loaded.");
        this.nativeAd = args.nativeAd;

        AdIconTexture.texture = nativeAd.GetIconTexture();
        AdHeadline.text = nativeAd.GetHeadlineText();
        AdDescription.text = nativeAd.GetBodyText();

        if (!nativeAd.RegisterIconImageGameObject(AdIconTexture.gameObject))
        {
            Debug.Log("error registering icon");
        }
        if (!nativeAd.RegisterHeadlineTextGameObject(AdHeadline.gameObject))
        {
            Debug.Log("error registering headline");
        }
        if (!nativeAd.RegisterBodyTextGameObject(AdDescription.gameObject))
        {
            Debug.Log("error registering description");
        }
        
        AdLoaded.gameObject.SetActive(true);
        AdLoading.gameObject.SetActive(false);
    }
}