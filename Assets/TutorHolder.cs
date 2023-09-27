using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorHolder : MonoBehaviour
{
    private Button backgroundBtn;
    void OnEnable()
    {
        backgroundBtn.GetComponent<Button>();
    }

    void Start()
    {
        backgroundBtn.onClick.AddListener(Hide);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
