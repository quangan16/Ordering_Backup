using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour
{
    [SerializeField] Transform checkPosition;
    [SerializeField] LayerMask layer;
    public SpriteRenderer shadow;
    public Solid target;
    Solid parent => GetComponentInParent<Solid>();
    public float time;
    public bool isDead;
    private void Start()
    {      
        isDead = false;
    }
    void Update()
    {
        if(!isDead)
        {
            CheckTarget();

        }
        else
        {
            if(target!= null)
            {
                FreeTarget();
            }
            
        }
       
    }
    void CheckTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPosition.position, Vector2.zero);
        if (hit.collider != null)
        {
            target = Cache.GetSolid(hit.collider);  
            if (target != null)
            {
                time = 0;
                if (!target.ContainTrigger(this))
                { 
                    target.AddTrigger(this); 
                }

                if (parent != null && !parent.ContainLock(this))
                {
                    parent.AddLock(this);
                }
            }
        }
        else
        {
            time += Time.deltaTime;   
            if(time > 0.4f)
            {
                if (target != null)
                {
                   FreeTarget();
                }
                else
                {
                    CheckParent();
                }

            }
            
        }
    } 
    void CheckParent()
    {
        if (parent != null)
        {
            if(parent.RemoveLock(this))           
            {
                parent.SetLastPosition(transform.position);
            }
            parent.CheckFree();
        }
       
    }
    void FreeTarget()
    {
        target.SetLastPosition(transform.position);
        target.RemoveTrigger(this);
        target.CheckFree();
        target = null;
    }

}