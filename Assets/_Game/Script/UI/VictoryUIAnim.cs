using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class VictoryUIAnim : MonoBehaviour
{
    [SerializeField] Image titleAlpha ;
    [SerializeField] private GameObject adsButton;

    void OnEnable()
    {
        
        titleAlpha.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        FadeInTitle();
        BoucingButton();
    }

    void FadeInTitle()
    {
        titleAlpha.DOFade(1, 0.7f);
        titleAlpha.transform.DOScale(1, 1.0f).SetEase(Ease.OutBounce).SetDelay(0.5f);

    }
    
    
    
    void BoucingButton()
    {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(adsButton.transform.DOScale(1.1f, 1.5f).SetEase(Ease.OutElastic)).Append(adsButton.transform
            .DOScale(1.0f, 0.2f)).SetDelay(1.0f).SetLoops(-1);


        tweenSequence.Play();
    }
    
    
}
