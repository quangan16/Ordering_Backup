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
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Button replay;
    public Mode mode;

    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        EmergeEaseOutBack();
    }

    void EmergeEaseOutBack()
    {
        transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
    }
    public void SetData(int i)
    {
        Mode mode = (Mode)DataManager.Instance.GetLevelMode(i);
        switch (mode)
        {
            case Mode.Locked:
                {
                    playButton.gameObject.SetActive(false);
                    break;
                }
            case Mode.Unlocked:
                {
                    playButton.gameObject.SetActive(false);
                    break;
                }
            case Mode.Bought:
                {
                    playButton.gameObject.SetActive(true);
                    break;
                }
            case Mode.Pass:
                {
                    playButton.gameObject.SetActive(true);
                    break;
                }
            case Mode.Fail:
                {
                    playButton.gameObject.SetActive(true);
                    break;
                }
            default:
                {

                    break;
                }







        }
        //tmp.text = s;
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
