using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Circle : Solid
{
    float x;
    float y => transform.localPosition.y;
    private void Start()
    {
        OnInit();
        x = transform.localPosition.x;
    }
    public override void SetUp()
    {
        base.SetUp();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;

    }
    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {
        base.Move(vectorA, vectorB);
        if(locked.Count == 0)
        {
            float angle = Vector3.SignedAngle(vectorA, vectorB, Vector3.forward);
            rb.MoveRotation(rb.rotation + angle);
        }
        else
        {
            ShakeOff();
        }
    }
    public override void MoveDeath()
    {
        transform.DOMove((transform.position*2 - lastPosition), 1f);
        Invoke(nameof(OnDeath), 1.5f);
    }
    void ShakeOff()
    {       
        StopCollision();       
        transform.DOLocalMoveX(x+0.015f, 0.2f).OnComplete(()=> transform.DOLocalMoveX(x - 0.03f, 0.2f).OnComplete(() => StartCollision()   ));
        
    }
    void StopCollision()
    {
        canClick= false;
        isTouch = true;      
    }
    private void StartCollision()
    {
        transform.DOLocalMoveX(x, 0.2f);
        canClick = true;      
        isTouch = false;
    }
}
