using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class LanguagePanel : MonoBehaviour
{
    List<string> languages;
    string language_select;
    public Dropdown languagesDropDown;
    public void Start()
    {
        languagesDropDown.onValueChanged.AddListener(OnDropdownValueChanged);
        language_select = Application.systemLanguage.ToString();
        ChangeLanguage();
    }
    private void OnDropdownValueChanged(int index)
    {
        languages = new List<string>() { "Vietnamese", "English", "German", "French", "Japanese", "Korean", "Russian", "Chinese", "Portuguese", "Indonesian", "Spanish", "Thai", "Malay" };
        language_select= languages[index];

        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        LocalizationManager.CurrentLanguage = language_select;
        LocalizationManager.UpdateSources();
    }

}
