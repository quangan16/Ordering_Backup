using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallegeItemAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeTxt;

    private float scaleDuration = 0.5f;
    public Button playButton;
    public Level dataLevel;
    public int level;
    public int price;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        EmergeEaseOutBack();
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void OnDestroy()
    {
       transform.DOKill();
    }

    void EmergeEaseOutBack()
    {
        transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
    }
    public void SetData(int time)
    {
        timeTxt.text =  time.ToString();
        price = time;
    }
    public void Play()
    {
        DataManager.Instance.SetLevel(level, Mode.Fail, 0);
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
