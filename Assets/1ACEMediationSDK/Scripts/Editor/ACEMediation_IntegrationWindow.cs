using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _1ACEMediationSDK.Scripts.Editor.SimpleJSON;
using ACEMediation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ACEMediation_IntegrationWindow : EditorWindow
{
    private static Texture2D ace_icon;
    private static Texture2D install_icon;
    private static Texture2D delete_icon;

    private static bool hasNewIronsource;
    private static string currentIronsourceVersion;
    private static string newIronsourceVersion;

    [MenuItem("ACE Mediation/Integration Window")]
    static void Init()
    {
        EditorGUIUtility.labelWidth = 50;
        ACEMediation_IntegrationWindow window = (ACEMediation_IntegrationWindow) EditorWindow.GetWindow(typeof(ACEMediation_IntegrationWindow), true, "ACE Mediation Integration Manager");
        window.Show();

        ACEMediation_EditorCorountine.StartEditorCoroutine(IEGetIronSourceConfig());
        var config = ACEMediation_Config.ValidateConfig();
        enableLogData4Game = config.enableLogData4Game;
    }

    private string ironsource_key_android;
    private string ironsource_key_ios;

    private string MaxSdkKey;
    private string InterstitialAdUnitId;
    private string RewardedAdUnitId;
    private string RewardedInterstitialAdUnitId;
    private string BannerAdUnitId;
    private string MRecAdUnitId;

    private bool existedIronsource;
    private bool existedMax;
    private bool existedMoPub;

    void InitACEIcon()
    {
        ace_icon = new Texture2D(32, 64);
        ace_icon.LoadImage(System.IO.File.ReadAllBytes("Assets/1ACEMediationSDK/Sprites/acegamestudioicon.png"));
        ace_icon.Apply();
        install_icon = new Texture2D(32, 32);
        install_icon.LoadImage(System.IO.File.ReadAllBytes("Assets/1ACEMediationSDK/Sprites/installicon.png"));
        install_icon.Apply();
        delete_icon = new Texture2D(32, 32);
        delete_icon.LoadImage(System.IO.File.ReadAllBytes("Assets/1ACEMediationSDK/Sprites/deleteicon.png"));
        delete_icon.Apply();
        
        
    }

    private static bool enableLogData4Game;
    void RefreshConfig()
    {
        var config = ACEMediation_Config.ValidateConfig();
#if EXISTED_IRON_SOURCE
        existedIronsource = true;
#else
        existedIronsource = false;
#endif

#if EXISTED_MAX
        existedMax = true;
#else
        existedMax = false;
#endif

#if EXISTED_MOPUB
        existedMoPub = true;
#else
        existedMoPub = false;
#endif
        if (existedIronsource && existedMax)
        {
            //Debug.LogError("Existed both Ironsource and Max, please remove one of them");
            EditorGUILayout.HelpBox("You used more than one mediation, please keep one mediation", MessageType.Warning);
        }
        else
        {
            if (existedIronsource)
            {
                var ironsourceConfig = ACEMediation_Config.GetIronsourceConfig();
                if (string.IsNullOrEmpty(ironsource_key_android)) ironsource_key_android = ironsourceConfig.ironsource_key_android;
                if (string.IsNullOrEmpty(ironsource_key_ios)) ironsource_key_ios = ironsourceConfig.ironsource_key_ios;
                using (new EditorGUILayout.HorizontalScope("box"))
                {
                    enableLogData4Game = EditorGUILayout.Toggle("Enable Log Data4Game", enableLogData4Game);
                    if (enableLogData4Game)
                    {
                        GUI.color = Color.green;
                        GUILayout.Label("Enable");
                        GUI.color = Color.white;
                    }
                    else
                    {
                        GUI.color = Color.yellow;
                        GUILayout.Label("Disable");
                        GUI.color = Color.white;
                    }
                }
                using (var ver = new EditorGUILayout.VerticalScope("Verticle"))
                {
                    GUILayout.Label("Ironsource config");
                    GUILayout.Label("Ironsource android key (*)");
                    ironsource_key_android = EditorGUILayout.TextArea(ironsource_key_android);
                    GUILayout.Label("Ironsource ios key (*)");
                    ironsource_key_ios = EditorGUILayout.TextArea(ironsource_key_ios);

                    if (GUILayout.Button("Da chuyen sang fix cung"))//"Save config"))
                    {
                        ironsourceConfig.ironsource_key_android = ironsource_key_android;
                        ironsourceConfig.ironsource_key_ios = ironsource_key_ios;
                        ACEMediation_Config.SetLogData4Game(enableLogData4Game);
                        ACEMediation_Config.SetIronsourceConfig(ironsourceConfig);
                    }
                }
            }
            else if (existedMax)
            {
                var maxConfig = ACEMediation_Config.GetMaxConfig();
                if (string.IsNullOrEmpty(MaxSdkKey)) MaxSdkKey = maxConfig.MaxSdkKey;
                if (string.IsNullOrEmpty(InterstitialAdUnitId)) InterstitialAdUnitId = maxConfig.InterstitialAdUnitId;
                if (string.IsNullOrEmpty(RewardedAdUnitId)) RewardedAdUnitId = maxConfig.RewardedAdUnitId;
                if (string.IsNullOrEmpty(RewardedInterstitialAdUnitId)) RewardedInterstitialAdUnitId = maxConfig.RewardedInterstitialAdUnitId;
                if (string.IsNullOrEmpty(BannerAdUnitId)) BannerAdUnitId = maxConfig.BannerAdUnitId;
                if (string.IsNullOrEmpty(MRecAdUnitId)) MRecAdUnitId = maxConfig.MRecAdUnitId;

                using (new EditorGUILayout.HorizontalScope("box"))
                {
                    enableLogData4Game = EditorGUILayout.Toggle("Enable Log Data4Game", enableLogData4Game);
                    GUILayout.Label(enableLogData4Game? "Enable" : "Disable");
                }
                using (var ver = new EditorGUILayout.VerticalScope("Verticle"))
                {
                    GUILayout.Label("MAX config");
                    GUILayout.Label("Max Sdk Key (*)");
                    MaxSdkKey = EditorGUILayout.TextField(MaxSdkKey);
                    GUILayout.Label("InterstitialAdUnitId (*)");
                    InterstitialAdUnitId = EditorGUILayout.TextField(InterstitialAdUnitId);
                    GUILayout.Label("RewardedAdUnitId (*)");
                    RewardedAdUnitId = EditorGUILayout.TextField(RewardedAdUnitId);
                    // GUILayout.Label("RewardedInterstitialAdUnitId");
                    // RewardedInterstitialAdUnitId = EditorGUILayout.TextField(RewardedInterstitialAdUnitId);
                    GUILayout.Label("BannerAdUnitId (*)");
                    BannerAdUnitId = EditorGUILayout.TextField(BannerAdUnitId);
                    // GUILayout.Label("MRecAdUnitId");
                    // MRecAdUnitId = EditorGUILayout.TextField(MRecAdUnitId);

                    if (GUILayout.Button("---------Da chuyen sang fix cung----------"))//"Save config"))
                    {
                        maxConfig.MaxSdkKey = MaxSdkKey;
                        maxConfig.InterstitialAdUnitId = InterstitialAdUnitId;
                        maxConfig.RewardedAdUnitId = RewardedAdUnitId;
                        maxConfig.RewardedInterstitialAdUnitId = RewardedInterstitialAdUnitId;
                        maxConfig.BannerAdUnitId = BannerAdUnitId;
                        maxConfig.MRecAdUnitId = MRecAdUnitId;
                        ACEMediation_Config.SetLogData4Game(enableLogData4Game);
                        ACEMediation_Config.SetMaxConfig(maxConfig);
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Document tại Assets/1ACEMediationSDK/Document");
        GUILayout.Space(10);
        InitACEIcon();
        using (var ver = new EditorGUILayout.VerticalScope("Ver"))
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Label("Mediations:");
                GUIStyle buttonStyle = new GUIStyle();
                buttonStyle.fixedHeight = 40;
                buttonStyle.fixedWidth = 40;
                GUI.enabled = true;
                using (new EditorGUILayout.HorizontalScope("box"))
                {
                    currentIronsourceVersion = PlayerPrefs.GetString("editor_ironsource_curversion");
                    newIronsourceVersion = PlayerPrefs.GetString("editor_ironsource_newversion");
                    hasNewIronsource = PlayerPrefs.GetInt("editor_ironsource_newversion_istrue") == 1;
                    GUILayout.Label("Ironsource " + newIronsourceVersion);
//#if EXISTED_IRON_SOURCE
                    // if (hasNewIronsource)
                    // {
                    //     if (GUILayout.Button("Upgrade to latest version"))
                    //     {
                    //         if (!CheckExistMediation())
                    //         {
                    //             FMediation_EditorMenuItem.AddIronsourceMediation(true);
                    //         }
                    //     }
                    // }
//#endif
#if EXISTED_IRON_SOURCE
                    GUI.enabled = false;
#else
                    GUI.enabled = true;
#endif
                    if (GUILayout.Button(install_icon, buttonStyle))
                    {
                        if (!string.IsNullOrEmpty(newIronsourceVersion))
                        {
                            if (!CheckExistMediation())
                            {
                                FMediation_EditorMenuItem.AddIronsourceMediation(newIronsourceVersion);
                            }
                        }
                        else
                        {
                            if (EditorUtility.DisplayDialog("Please wait to fetching Ironsource version", "", "OK"))
                            {
                                FMediation_EditorMenuItem.RemoveIronsourceMediation();
                            }
                        }
                    }

#if EXISTED_IRON_SOURCE
                    GUI.enabled = true;
#else
                    GUI.enabled = false;
#endif
                    if (GUILayout.Button(delete_icon, buttonStyle))
                    {
                        if (EditorUtility.DisplayDialog("Remove Ironsource", "", "OK", "Cancel"))
                        {
                            FMediation_EditorMenuItem.RemoveIronsourceMediation();
                        }
                    }
                }

                GUI.enabled = true;
                using (new EditorGUILayout.HorizontalScope("box"))
                {
                    GUILayout.Label("MAX v3.1.18");
#if EXISTED_MAX
                    GUI.enabled = false;
#else
                    GUI.enabled = true;
#endif
                    if (GUILayout.Button(install_icon, buttonStyle))
                    {
                        if (!CheckExistMediation())
                        {
                            FMediation_EditorMenuItem.AddMAXMediation();
                        }
                    }

#if EXISTED_MAX
                    GUI.enabled = true;
#else
                    GUI.enabled = false;
#endif
                    if (GUILayout.Button(delete_icon, buttonStyle))
                    {
                        if (EditorUtility.DisplayDialog("Remove MAX", "", "OK", "Cancel"))
                        {
                            FMediation_EditorMenuItem.RemoveMAXMediation();
                        }
                    }
                }
                GUI.enabled = true;
                GUI.color = Color.yellow;
                GUILayout.Label("Chú ý: download và import SDK quảng cáo xong thì mở bảng tích hợp (Ironsource hoặc MAX) \nvà nâng cấp SDK quảng cáo và các adapter network lên mới nhất");
                GUI.color = Color.white;
            }

            GUILayout.Space(20);

            using (new EditorGUILayout.VerticalScope("box"))
            {
                RefreshConfig();
            }

            var ACESyle = new GUIStyle();
            ACESyle.fixedWidth = 64;
            ACESyle.fixedHeight = 64 * 1.3f;
            GUILayout.Box(ace_icon, ACESyle);
        }
    }

    static IEnumerator IEGetIronSourceConfig()
    {
        //Debug.Log("Get ironsource version config");
        UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://ssa.public.s3.amazonaws.com/Ironsource-Integration-Manager/IronSourceSDKInfo.json");
        var webRequest = unityWebRequest.SendWebRequest();
        while (!webRequest.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (!unityWebRequest.isHttpError && !unityWebRequest.isNetworkError)
        {
            string json = unityWebRequest.downloadHandler.text;
            var N = JSON.Parse(json);
            newIronsourceVersion = N["SDKSInfo"]["IronSource"]["Android"]["version"]["sdk"].Value;
            PlayerPrefs.SetString("editor_ironsource_newversion", newIronsourceVersion);
            //Debug.Log("New ironsource : " + newIronsourceVersion);
        }

        currentIronsourceVersion = ACEMediation_Config.GetIronsourceVersion();
        PlayerPrefs.SetString("editor_ironsource_curversion", currentIronsourceVersion);
        //Debug.Log("Current ironsource : " + currentIronsourceVersion);
        int length = currentIronsourceVersion.Length > newIronsourceVersion.Length ? newIronsourceVersion.Length : currentIronsourceVersion.Length;
        if (length == 0)
        {
            //Debug.Log("not have new ironsource");
            hasNewIronsource = false;
        }
        else //Debug.Log(length);
        for (int i = 0; i < length; i++)
        {
            var newCharacter = ((int) newIronsourceVersion[i]) - 48;
            var curCharacter = ((int) currentIronsourceVersion[i]) - 48;
            // //Debug.Log($"cur {curCharacter} new {newCharacter}");
            if (9 < newCharacter || newCharacter < 0 || 9 < curCharacter || curCharacter < 0)
                continue;
            if (newCharacter == curCharacter)
                continue;
            hasNewIronsource = newCharacter > curCharacter;
            PlayerPrefs.SetInt("editor_ironsource_newversion_istrue", hasNewIronsource? 1 : 0);
            //Debug.Log("new ironsource? " + hasNewIronsource);
            yield break;
        }
        hasNewIronsource = false;
        //Debug.Log("not have new ironsource");
    }

    bool CheckExistMediation()
    {
        if (existedIronsource)
        {
            EditorUtility.DisplayDialog("Warning", "Ironsource already existed. Please remove Ironsource after integration", "OK");
            return true;
        }
        else if (existedMax)
        {
            EditorUtility.DisplayDialog("Warning", "MAX already existed. Please remove MAX after integration", "OK");
            return true;
        }
        else if (existedMoPub)
        {
            EditorUtility.DisplayDialog("Warning", "MoPub already existed. Please remove MoPub after integration", "OK");
            return true;
        }

        return false;
    }
}