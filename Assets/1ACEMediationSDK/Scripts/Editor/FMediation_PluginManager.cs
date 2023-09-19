﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEditor;

#endif

namespace ACEMediation
{
    [InitializeOnLoad]
    public class FMediation_PluginManager
    {
        // This static constructor will automatically run thanks to the InitializeOnLoad attribute.
        static FMediation_PluginManager()
        {
            //Debug.Log("FMediation_PluginManager");
            EditorApplication.update += CheckPlugins;
        }

        static void CheckPlugins()
        {
            if (EditorApplication.isCompiling)
                return;
            bool existedACESDK = AssetDatabase.IsValidFolder($"Assets/1ACESDK");
            bool existedIronsource = AssetDatabase.IsValidFolder($"Assets/Ironsource");
            bool existedMax = AssetDatabase.IsValidFolder($"Assets/MaxSdk");
            bool existedMoPub = AssetDatabase.IsValidFolder($"Assets/MoPub");
#if !EXISTED_ACE_SDK
            if (existedACESDK)
                ACEMediation_Utils.AddDefine("EXISTED_ACE_SDK");
#else
            if (!existedACESDK)
                FMediation_Utils.RemoveDefine("EXISTED_ACE_SDK");
#endif
#if !EXISTED_IRON_SOURCE
            if (existedIronsource)
                FMediation_EditorMenuItem.AddMediationDefine(mediation.IRON_SOURCE);
#else
            if (!existedIronsource)
                FMediation_EditorMenuItem.RemoveMediationDefine(mediation.IRON_SOURCE);
#endif
#if !EXISTED_MAX
            if (existedMax)
                FMediation_EditorMenuItem.AddMediationDefine(mediation.MAX);
#else
            if (!existedMax)
                FMediation_EditorMenuItem.RemoveMediationDefine(mediation.MAX);
#endif
#if !EXISTED_MOPUB
            if (existedMoPub)
                FMediation_EditorMenuItem.AddMediationDefine(mediation.MOPUB);
#else
            if (!existedMoPub)
                FMediation_EditorMenuItem.RemoveMediationDefine(mediation.MOPUB);
#endif
        }
    }
}