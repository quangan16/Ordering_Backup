using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class PopupManager : MonoBehaviour
{
    [SerializeField] protected RectTransform mainPanel;
    [SerializeField] protected Button backGround;
    protected float scaleDuration = 0.5f;
    protected void OnEnable()
    {
        backGround.onClick.AddListener(OnClose);
        OnOpen();
    }

    protected void OnDisable()
    {
        transform.DOKill();
        mainPanel.DOKill();
    }

    protected void OnDestroy()
    {
        transform.DOKill();
        mainPanel.DOKill();
    }

    public void OnOpen()
    {
        Reset();
        mainPanel.DOScale(1.0f, scaleDuration).SetEase(Ease.OutBack);
       
    }

    public void OnClose()
    {
        mainPanel.DOScale(0.0f, scaleDuration).SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));
    }

    public void Reset()
    {
        mainPanel.localScale = Vector3.zero;
    }
}
