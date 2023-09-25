using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Line : Solid
{
    public bool isVertical;
    Vector3 direct;
    Vector3 startPosition;
    Vector2 target;
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
        if(!isDead)
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
       
       
    }

 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.otherCollider.GetComponent<Bomb>())
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.velocity = -rb.velocity * 0.5f;
            }
            if (GameManager.isVibrate)
            {
                Handheld.Vibrate();
            }
            SoundManager.Instance.Play();
        }    


    }
    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector3 endPoint = Vector3.Project(vectorB - vectorA, direct);
        target = new Vector2(endPoint.x, endPoint.y);
      
        rb.velocity = (target/Time.deltaTime).normalized* Mathf.Clamp((target / Time.deltaTime).magnitude,-100,100);
        //rb.MovePosition(target + rb.position);
        

    }
    public override void MoveDeath()
    {
        //Instantiate(ps, transform.position, Quaternion.identity);
        //blinkVoice.Play();

        //transform.DOLocalMove((transform.localPosition*2 - startPosition), 1f);
        base.MoveDeath();
    }
    public override void OnSelected()
    {
        base.OnSelected();
        foreach (var sp in spriteShadow)
        {
          //  sp.enabled = true;
        }
    }



}
