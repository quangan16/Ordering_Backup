using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;


public class PopupGDPR : MonoBehaviour
{
    public Transform main;
    public GameObject btn_no;
#if EXISTED_IRON_SOURCE
    public RectTransform notify;
    public bool show_notify_thanks;

    public static bool rate_gdpr
    {
        get { return PlayerPrefs.GetInt("rate_gdpr") == 1; }
        set { PlayerPrefs.SetInt("rate_gdpr", value ? 1 : 0); }
    }

    public static bool rate_gdpr_value
    {
        get { return PlayerPrefs.GetInt("rate_gdpr_value") == 1; }
        set { PlayerPrefs.SetInt("rate_gdpr_value", value ? 1 : 0); }
    }

    public Action onComplete;

    private void Awake()
    {
        main.localScale = Vector3.zero;
        main.DOScale(1, 0.2f);
        btn_no.SetActive(false);
        StartCoroutine(IEShow());
    }

    IEnumerator IEShow()
    {
        yield return new WaitForSeconds(10);
        btn_no.SetActive(true);
    }

    public void OnYes()
    {
        IronSource.Agent.setConsent(true);
        rate_gdpr_value = true;
        NotifyThanks();
    }

    public void OnNo()
    {
        IronSource.Agent.setConsent(false);
        rate_gdpr_value = false;
        NotifyThanks();
    }

    void NotifyThanks()
    {
        rate_gdpr = true;
        if (show_notify_thanks)
        {
            notify.gameObject.SetActive(true);
            notify.transform.localScale = Vector3.one;
            notify.transform.DOScale(1, 0.2f);
            notify.GetComponentInChildren<Text>().text = "Thank you";
            notify.anchoredPosition = new Vector2(0, -100);
            notify.DOAnchorPos(Vector2.zero, 0.5f);
            main.gameObject.SetActive(false);
            StartCoroutine(IEComplete1());
        }
        else
        {
            main.DOScale(0, 0.2f);
            StartCoroutine(IEComplete2());
        }
    }

    IEnumerator IEComplete1()
    {
        yield return new WaitForSeconds(1);
        notify.transform.DOScale(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        onComplete?.Invoke();
        Destroy(transform.gameObject);
    }

    IEnumerator IEComplete2()
    {
        yield return new WaitForSeconds(0.2f);
        onComplete?.Invoke();
        Destroy(transform.gameObject);
    }
#endif
}