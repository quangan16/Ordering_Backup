using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadePopup : MonoBehaviour
{
    public Transform main;
    private DOTweenAnimation tweenAnimation;
    [SerializeField] private CanvasGroup challengeCanvas;
    private void Awake()
    {
        tweenAnimation = GetComponent<DOTweenAnimation>();
        
       
    }

    public void Show(object data)
    {
        challengeCanvas.transform.localPosition = new Vector3(0, 30.0f);
        challengeCanvas.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
        // Use DOTween to fade in the popup over a specified duration
        challengeCanvas.DOFade(1f, 0.5f);
    }

    public void Hide()
    {

        challengeCanvas.DOFade(0f, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

}
