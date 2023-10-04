using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTMPToText : MonoBehaviour
{

    public Font font;
    [Button]
    void Change()
    {
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     var child = transform.GetChild(i).GetComponent<TextMeshProUGUI>()
        //     while (!child)
        //     {
        //         var 
        //     }
        // }

        var list_TMP = transform.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var var_text_mesh in list_TMP)
        {
            Debug.Log("Name: " + var_text_mesh.transform.name);
            ChangeChild(var_text_mesh.transform);
        }
        //EditorUtility.SetDirty(gameObject.transform.root.gameObject);
    }

    void ChangeChild(Transform child)
    {
        var tmp = child.GetComponent<TextMeshProUGUI>();
        if (!tmp)
        {
            return;
        }
        var txt = tmp.text;
        var fontStyle = tmp.fontStyle;
        var fontSize = tmp.fontSize;
        var isAutoSize = tmp.autoSizeTextContainer;
        var minSize = tmp.fontSizeMin;
        var maxSize = tmp.fontSizeMax;
        var color = tmp.color;
        var lineSpacing = tmp.lineSpacing;
        var horizontalAlignment = tmp.horizontalAlignment;
        var verticalAlignment = tmp.verticalAlignment;
        
        DestroyImmediate(tmp);

        var text = child.AddComponent<Text>();
        text.text = txt;
        SetFontStyle(fontStyle, text);
        text.fontSize = (int)fontSize;
        if (isAutoSize)
        {
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = (int)minSize;
            text.resizeTextMaxSize = (int)maxSize;
        }

        text.font = font;
        text.color = color;
        text.raycastTarget = false;
        // text.lineSpacing = lineSpacing;
        SetAligment(horizontalAlignment, verticalAlignment, text);

    }

    void SetFontStyle(TMPro.FontStyles tmpStyle, Text text)
    {
        switch (tmpStyle)
        {
            case FontStyles.Bold:
                text.fontStyle = FontStyle.Bold;
                break;
            case FontStyles.Italic:
                text.fontStyle = FontStyle.Italic;
                break;
        }
    }

    void SetAligment(HorizontalAlignmentOptions hor, VerticalAlignmentOptions ver, Text text)
    {
            int horValue = 0;
            int verValue = 0;
        switch (hor)
        {
            case HorizontalAlignmentOptions.Center:
                horValue = 1;
                break;
            case HorizontalAlignmentOptions.Left:
                horValue = 0;
                break;
            case HorizontalAlignmentOptions.Right:
                horValue = 2;
                break;
        }
        switch (ver)
        {
            case VerticalAlignmentOptions.Bottom:
                verValue = 2;
                break;
            case VerticalAlignmentOptions.Top:
                verValue = 0;
                break;
            case VerticalAlignmentOptions.Middle:
                verValue = 1;
                break;
        }

        text.alignment = (TextAnchor)(verValue * 3 + horValue);
    }
}
