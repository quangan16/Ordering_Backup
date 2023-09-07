using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SettingDropdownAnimation : MonoBehaviour
{

    [SerializeField] private bool isOpen;
    private float scaleDuration = 0.3f;
    void Start()
    {
        isOpen = false;
        transform.localScale = Vector3.zero;
    }

    public void HandleSettingInteraction()
    {
        if (isOpen == false)
        {
            transform.DOScale(Vector3.one, scaleDuration).OnComplete(() => isOpen = true);
        }
        else
        {
            transform.DOScale(Vector3.zero, scaleDuration).OnComplete(() => isOpen = false);
        }
    }

   
}
