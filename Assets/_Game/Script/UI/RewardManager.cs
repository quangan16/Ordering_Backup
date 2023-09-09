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
        // Reset();
      
    }

    // Start is called before the first frame update

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

    void Reset()
    {
        for (int i = 0; i < coinPilePrefab.transform.childCount-1; i++)
        {
            coinPilePrefab.transform.GetChild(i).position = coinInitPos[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Button]
    IEnumerator RewardAnim()
    {
        Reset();
        
        float intervalDelay = 0.1f;
        coinPilePrefab.SetActive(true);
        
        for (int i = 0; i < coinPilePrefab.transform.childCount; i++)
        {
            Transform currentChild = coinPilePrefab.transform.GetChild(i);
           
            Sequence tweenAnim = DOTween.Sequence();
            if (tweenAnim.IsPlaying())
            {
                tweenAnim.Append(currentChild.DOScale(Vector3.one, 0.3f)).AppendInterval(0.7f).Append(currentChild.DOMove(coinDestPos.position, 1.2f).SetEase(Ease.InQuart)).Join(currentChild.DOScale(Vector3.zero, 1.0f).SetEase(Ease.InQuart)).OnComplete(
                    () =>
                    {
                        GameManager.Instance.AddCoin(2);
                    });
            }
            
               
            tweenAnim.Play();
            yield return new WaitForSeconds(intervalDelay);
        }
        
        
    }
}
