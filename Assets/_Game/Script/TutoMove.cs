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
            transform.DOLocalMoveY(transform.localPosition.y + 1f,1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        if(horizontal)
        {
            transform.DOLocalMoveX(transform.localPosition.x + 1f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        }
        else
        if (rotate)
        {
            Vector3[] path = new Vector3[] {transform.position+Vector3.right*2, transform.position + Vector3.up * 1.5f, transform.position + Vector3.right * 2 + Vector3.up * 1.5f };
            transform.DORotate(Vector3.forward*-300,3f,RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            GetComponentInChildren<Renderer>().transform.DOLocalRotate(Vector3.forward * 300, 3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        {
            //transform.DOMove(transform.position+Vector3.down*0.5f+Vector3.left*0.5f,1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
           
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            GameManager.Instance.StartCountDown();
            OnDeath();
        }
    }
    public void OnDeath()
    {
       
        transform.DOKill();
        Destroy(gameObject);
        Destroy(this);
    }    

}
