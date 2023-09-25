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

    private Tween itemTweener;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        EmergeEaseOutBack();
    }
    
    private void OnDisable()
    {
        Hide();
        transform.DOKill();
    }

    private void OnDestroy()
    {
       transform.DOKill();
    }

    void EmergeEaseOutBack()
    {
        itemTweener = transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
    }

    void Hide()
    {
        itemTweener.Rewind();
    }
    public virtual void SetData(int data)
    {
        timeTxt.text =  data.ToString();
    }
    public void Play()
    {
        if(GetComponentInParent<ChallengeUI>().heart>0)
        {
            DataManager.Instance.SetLevel(level, Mode.Fail, 0);

        }
        else
        {

        }
        
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
