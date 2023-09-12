using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoMove : MonoBehaviour
{
    public bool vertical;
    public bool horizontal;
    public bool rotate;
    void Start()
    {
        if(vertical)
        {
            transform.DOLocalMoveY(transform.localPosition.y + 0.5f,1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        if(horizontal)
        {
            transform.DOLocalMoveX(transform.localPosition.x + 0.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        }
        else
        if (rotate)
        {
            Vector3[] path = new Vector3[] {transform.position+Vector3.right*2, transform.position + Vector3.up * 1.5f, transform.position + Vector3.right * 2 + Vector3.up * 1.5f };
            transform.DOPath(path,1.5f,PathType.CubicBezier).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        {
            transform.DOMove(transform.position+Vector3.down*0.5f+Vector3.left*0.5f,1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.StopAllCoroutines();
            GameManager.Instance.StartCountDown();
            transform.DOKill();
            Destroy(gameObject);
            Destroy(this);
        }
    }

}
