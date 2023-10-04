using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class ButtonAudio : MonoBehaviour
{
    private Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            button = GetComponent<ButtonEffectLogic>();
        }
    }

    public void Start()
    {
        button.onClick.AddListener(PlayButtonSound);
    }
    public void PlayButtonSound()
    {
        SoundManager.Instance.PlayButtonSound();
    }
}
