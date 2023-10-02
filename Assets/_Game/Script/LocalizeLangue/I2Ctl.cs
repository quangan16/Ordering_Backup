using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using I2.Loc;
using UnityEngine;
using Newtonsoft.Json;

public class I2Ctl : MonoBehaviour
{
    public LanguageSourceAsset languageSource;
    
    [Button]
    private void Try()
    {
        // const string folderPath = @"Assets\0Game\Datas\Text_language";
        // var files = Directory.GetFiles(folderPath).ToList();

        string[] paths = new[]
        {
            @"D:\Data\Ordering",

        };

        foreach (string path in paths)
        {
            var files = Directory.GetFiles(path).ToList();
            files.ForEach(AddTerm);
            Debug.Log($"Complete add {files.Count} files");
        }
    }

    void AddTerm(string filePath)
    {
        if (filePath.Contains(".meta")) return;

        Debug.LogError("AddTerm: " + filePath);
        var jsonString = File.ReadAllText(filePath);
        TermData termData;
        try
        {
            var translate = JsonConvert.DeserializeObject<DataModel>(jsonString);
            termData = new TermData
            {
                Term = translate.English,
                Languages = new[]
                {
                    translate.English, translate.German, translate.French, translate.Japanese, translate.Korean,
                    translate.Russian, translate.Chinese, translate.Vietnamese, translate.Portuguese,
                    translate.Indonesian,
                    translate.Spanish, translate.Thai, translate.Malay
                }
            };
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Error: " + filePath + " is null or empty");
            return;
        }

        languageSource.mSource.mTerms.Add(termData);
        languageSource.mSource.Awake();
    }

//    public CharacterTalk characterTalk;

    //[Button]
    //public void Test()
    //{
    //    // CanvasUIManager.Instance.OpenBoxImmediately();
    //}
}

[Serializable]
public class DataModel
{
    public string English;
    public string German;
    public string French;
    public string Japanese;
    public string Korean;
    public string Russian;
    public string Chinese;
    public string Vietnamese;
    public string Portuguese;
    public string Indonesian;
    public string Spanish;
    public string Thai;
    public string Malay;
}