using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class LanguagePanel : MonoBehaviour
{
    List<string> languages ;
    string language_select;
    public Dropdown languagesDropDown;
    public void Awake()
    {
        languages = new List<string>()
        {
            "Vietnamese", "English", "German", "French", "Japanese", "Korean", "Russian", "Chinese", "Portuguese",
            "Indonesian", "Spanish", "Thai", "Malay"
        };
        language_select = DataManager.Instance.GetLanguage();
        for (int i = 0; i < languages.Count; i++)
        {
            if (language_select == languages[i])
            {
                languagesDropDown.value = i;
                break;
            }
        } 
        languagesDropDown.onValueChanged.AddListener(OnDropdownValueChanged);
        
        ChangeLanguage();
    }
    private void OnDropdownValueChanged(int index)
    {
        language_select= languages[index];
        DataManager.Instance.SetLanguage(language_select);
        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        LocalizationManager.CurrentLanguage = language_select;
        LocalizationManager.UpdateSources();
    }

}
