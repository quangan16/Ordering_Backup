using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour
{
    [SerializeField] Transform checkPosition;
    [SerializeField] LayerMask layer;
    [SerializeField] SpriteRenderer green;
    public SpriteRenderer shadow;
    public SpriteRenderer currentSkin;

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
        RaycastHit2D hit = Physics2D.Raycast(checkPosition.position, Vector2.zero,Mathf.Infinity,layer);
        if (hit.collider != null )
        {
            target = Cache.GetSolid(hit.collider);  
            if (target != null)
            {
                time = 0;
                ChangeGreen(false);
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
            ChangeGreen(true);
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
    public bool ChangeGreen(bool isGreen)
    {
        green.enabled = isGreen;
        return isGreen;
    } 
    public void ChangeSkin(Sprite sprite)
    {
        currentSkin.sprite = sprite;

    }

}