using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ButtonEffect : MonoBehaviour
{
    private float jiggleDuration = 1f;
    private float jiggleAngle = 10.0f;
    public float scaleFactor = 1.1f;
    private Vector3 initialScale;
    private Sequence tweenSequence;
    
    void OnEnable()
    {
        tweenSequence = DOTween.Sequence();
      
        initialScale = transform.localScale;
        StartCoroutine(PlayEffect());
    }

    void OnDisable()
    {
        tweenSequence.Kill();
    }

    public void Reset()
    {
        transform.localScale = initialScale;
        transform.localEulerAngles = Vector3.zero;
    }

    public IEnumerator PlayEffect()
    {
        Reset();
        tweenSequence.SetEase(Ease.InOutQuad);
        tweenSequence.Append(transform.DORotate(new Vector3(0, 0, -jiggleAngle), jiggleDuration/2))
            .Join(transform.DOScale(initialScale * scaleFactor, jiggleDuration / 2)) // Scale up
            .Append(transform.DORotate(new Vector3(0, 0, jiggleAngle), jiggleDuration))
            .Append(transform.DORotate(new Vector3(0, 0, 0), jiggleDuration/2))
            
            .Join(transform.DOScale(initialScale, jiggleDuration / 2)).OnComplete(() => { tweenSequence.Kill(); }); // Scale down
        // tweenSequence.Append(transform.DOScale(1.1f, 1.5f).SetEase(Ease.OutElastic)).Join(transform.).Append(
        //     transform.DOScale(1.0f, 0.2f)).SetDelay(1.0f).SetLoops(-1).OnComplete(() => { tweenSequence.Kill(); });

        tweenSequence.SetDelay(2.0f).SetLoops(-1, LoopType.Restart);
        tweenSequence.Play();
        yield return null;
    }
}
