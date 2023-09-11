using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallegeItemAnimation : MonoBehaviour
{
    private float scaleDuration = 0.5f;
    public Button playButton;
    [SerializeField]TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        EmergeEaseOutBack();
    }

    private void OnDisable()
    {
        DOTween.Clear();
    }

    private void OnDestroy()
    {
        DOTween.Clear();
    }

    void EmergeEaseOutBack()
    {
        transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
    }
    public void SetData(string s)
    {
        //tmp.text = s;
    }
}
