using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class LanguagePanel :MonoBehaviour
{

    public List<string> languages;
    public string language_select;
    //public int language_index;

     public List<string> language_labels;
     public List<Text> txt_langguage;

    //public ScrollRect scrollRect;
    //public RectTransform contentPanel;
    //public List<RectTransform> button_list;

    //public List<Image> btn_images;
    public void Start()
    {
        LoadLanguageData();
        language_labels = new List<string>() {"Tiếng Việt", "English", "Deutsch", "Français", "日本语", "한국어", "Русский", "中文", "Português", "Indonesia", "Español", "ไทย", "Melayu"};
        for (int i = 0; i < txt_langguage.Count; i++)
        {
            txt_langguage[i].text = language_labels[i];
        }
        // Hide();
    }

    //public override void Show()
    //{
    //    SnapTo(button_list[language_index-1]);
    //    base.Show();
    //}
    //public void SnapTo(RectTransform target)
    //{
    //    Canvas.ForceUpdateCanvases();
    //    // Debug.Log("contentPanel.position: "+(contentPanel.anchoredPosition)+", target.position: "+(target.anchoredPosition));
    //    contentPanel.anchoredPosition = new Vector2(0, 0-target.anchoredPosition.y);
            
    //    // Debug.Log("LanguagePanel-SnapTo:"+target.name+", pos: "+contentPanel.anchoredPosition);
    //}

    private void LoadLanguageData()
    {
        languages = new List<string>() { "Vietnamese", "English", "German", "French", "Japanese", "Korean", "Russian", "Chinese", "Portuguese", "Indonesian", "Spanish", "Thai", "Malay" };
        
        //language_index = PlayPrefDataManager.LanguageIndex;
        
        //if (language_index <= 0) // Not select language AKA first play on device
        //{
        //    language_select = Application.systemLanguage.ToString();
        //    language_index = languages.IndexOf(language_select) + 1;

        //    PlayPrefDataManager.LanguageIndex = language_index;
        //}
        //else
        //{
        //    language_select = languages[language_index - 1];
        //}
        
        //btn_images[language_index - 1].sprite = selected_bg;
        
        ChangeLanguage();
    }

    public void LanguageClick(int index)
    {
        // Debug.Log("LanguageClick()");
        

        ChangeLanguage();
        
      //  Hide();
    }

    private void ChangeLanguage()
    {
        LocalizationManager.CurrentLanguage = language_select;
        LocalizationManager.UpdateSources();

       // CanvasUIManager.Instance.ReloadUIByLanguage();
    }

}
