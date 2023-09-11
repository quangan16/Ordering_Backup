using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPilePrefab;

    [SerializeField] private Vector3[] coinInitPos;

    [SerializeField] private Transform coinDestPos;

    [SerializeField] private TextMeshProUGUI coinAmountTxt;

    void Awake()
    {
        SetUpInitPos();
    }
    
    private void OnEnable()
    {
        Reset();
      
    }
    private void OnDisable()
    {
        
    }

  

    public void GetCoin()
    {
        StartCoroutine(RewardAnim());
    }
    

    void SetUpInitPos()
    {
        for (int i = 0; i < coinPilePrefab.transform.childCount; i++)
        {
            coinInitPos[i] = coinPilePrefab.transform.GetChild(i).position;

        }
    }

    private void Reset()
    {
        for (int i = 0; i < coinPilePrefab.transform.childCount; i++)
        {
            coinPilePrefab.transform.GetChild(i).localPosition = coinInitPos[i];
            coinPilePrefab.transform.GetChild(i).localScale = Vector3.zero;
        }
    }
    
    [Button]
    IEnumerator RewardAnim()
    {
        for (int i = 0; i < coinPilePrefab.transform.childCount; i++)
        {
            coinPilePrefab.transform.GetChild(i).position = coinInitPos[i];
        }

        float intervalDelay = 0.1f;
        coinPilePrefab.SetActive(true);

        for (int i = 0; i < coinPilePrefab.transform.childCount; i++)
        {
            Transform currentChild = coinPilePrefab.transform.GetChild(i);

            Sequence tweenAnim = DOTween.Sequence();
            if (!tweenAnim.IsComplete())
            {
                tweenAnim.Append(currentChild.DOScale(Vector3.one, 0.2f)).AppendInterval(0.5f).Append(currentChild.DOMove(coinDestPos.position, 1.2f).SetEase(Ease.InQuart)).Join(currentChild.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InQuart)).OnComplete(
                    () =>
                    {
                        //GameManager.Instance.AddCoin(2);
                    });
            }

            WinUI win = GetComponentInParent<WinUI>();
           
            yield return new WaitForSeconds(intervalDelay);
            tweenAnim.Play().OnComplete(() => { win.Close(); });
        }
        
        
    }
}
