using System;
using System.Collections.Generic;
#if EXISTED_ACE_SDK
using ACEMediation;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace ACEMediation
{
    public enum ads_status
    {
        loading,
        loaded,
        load_failed,
        showing,
        hide,
        not_ready
    }
    
    public abstract class ACEMediation_Adapter : MonoBehaviour
    {
        public ads_status banner_status, inter_status, reward_status;
        protected Action onVideoComplete, onVideoFail;
        protected bool isInterBackupForReward;

        public abstract void Setup(bool isInterBackupForReward);
        public abstract void ShowInterstitial();
        public abstract void ShowRewardedAd(Action onVideoComplete, Action onVideoFail);
        public abstract void ShowBanner();
        public abstract void HideBanner();
    }
}