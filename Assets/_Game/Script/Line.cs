using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Line : Solid
{
    public bool isVertical;
    Vector2 target;
    Vector3 direct;
    Vector3 startPosition;
    private void Start()
    {
        OnInit();
        startPosition = transform.localPosition;
    }
    public override void OnInit()
    {
        base.OnInit();

    }
    public override void SetUp()
    {
        if(isVertical)
        {
            direct = transform.up;
        }
        else 
        {
            direct = transform.right;
        }
        rb.freezeRotation = true;
    }
    private void Update()
    {
        if (isVertical)
        {
            transform.localPosition = new Vector3(startPosition.x, transform.localPosition.y);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, startPosition.y);
        }
       
    }

 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {           
             rb.velocity = -rb.velocity*0.5f;
        }

    }
    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {
        base.Move(vectorA, vectorB);
        rb.bodyType = RigidbodyType2D.Dynamic;
       
        Vector3 endPoint = Vector3.Project(vectorB - vectorA, direct);
        target = new Vector2(endPoint.x, endPoint.y);
        rb.velocity = target;

    }
    public override void MoveDeath()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        blinkVoice.Play();
        transform.DOLocalMove((transform.localPosition*2 - startPosition), 1f);
        Invoke(nameof(OnDeath), 1.5f);
    }



}
