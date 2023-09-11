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
    public int level;
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
    public void SetData(int i)
    {
        Mode mode = DataManager.Instance.GetLevelMode(i);       
    }
    public void BuyLevel()
    {
        
        DataManager.Instance.AddCoin(-200);
    }





}
public enum Mode
{
    Locked, //haven't reached enough level   => show lock
    Unlocked, // reached enough level, didn't buy => show price
    Bought, //Bought, didn't play => show play
    Pass, //Play and passed => show replay 
    Fail // Play and fail => show replay

}
