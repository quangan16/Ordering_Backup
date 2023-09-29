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

    [SerializeField] private LoadingUI loading;

    [SerializeField] private IndicatorController indi;
    [SerializeField] private WinUI winUI => GetComponentInParent<WinUI>();

    private int rewardAmount;

    [SerializeField] private AudioSource audioSrc;

    [SerializeField] private AudioClip spawnCoinSfx;

    [SerializeField] private AudioClip collectCoinSfx;
    

    private float volume = 1.0f;

    void Awake()
    {
        SetUpInitPos();
    }
    
    private void OnEnable()
    {
        Reset();
        coinAmountTxt.text = DataManager.Instance.GetCoin().ToString();


    }
    private void OnDisable()
    {
        
    }

    public void GetAdsCoin()
    {
        rewardAmount = int.Parse(indi.adsCoinTxt.text);
        StartCoroutine(RewardAnim());
    }
    public void GetDefaultCoin()
    {
        rewardAmount = int.Parse(indi.defaultCoinTxt.text);
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
        
        audioSrc.PlayOneShot(spawnCoinSfx, volume);
        
        for (int i = 0; i < coinPilePrefab.transform.childCount; i++)
        {
            Transform currentChild = coinPilePrefab.transform.GetChild(i);

            // Sequence tweenAnim = DOTween.Sequence();
           
                // currentChild.DOScale(Vector3.one, 0.2f)).AppendInterval(0.5f).Append(currentChild.DOMove(coinDestPos.position, 1.2f).SetEase(Ease.InQuart)).Join(currentChild.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InQuart))
                currentChild.DOScale(Vector3.one, 0.2f);
                yield return new WaitForSeconds(intervalDelay);
                currentChild.DOMove(coinDestPos.position, 1.2f).SetEase(Ease.InQuart).OnComplete(() => currentChild.DOScale(Vector3.zero, 0.5f));
                // currentChild.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InQuart);
        }

        
        yield return new WaitForSeconds(0.5f);
        audioSrc.PlayOneShot(collectCoinSfx, volume);
        int targetCoinAmount = int.Parse(coinAmountTxt.text) + rewardAmount;
        coinAmountTxt = UIManager.Instance.CoinText();
        while (int.Parse(coinAmountTxt.text) < targetCoinAmount -1)
        {
            yield return new WaitForSeconds(0.05f);
            coinAmountTxt.text = (int.Parse(coinAmountTxt.text) + rewardAmount/7).ToString();            
            yield return null;

        }
        UIManager.Instance.SetCoin();

        yield return new WaitForSeconds(0.5f);
       
        winUI.Close();
        //loading.Open();

    }
}
