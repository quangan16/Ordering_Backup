using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour
{
    [SerializeField] Transform checkPosition;
    [SerializeField] LayerMask layer;   
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
                if (!target.triggered.Contains(this))
                { 
                    target.triggered.Add(this); 
                }

                if (parent != null && !parent.locked.Contains(this))
                {
                    parent.locked.Add(this);
                }
            }
        }
        else
        {
            time += Time.deltaTime;   
            if(time > 0.5f)
            {
                if (target != null)
                {
                    target.triggered.Remove(this);
                    target.CheckFree();
                    target = null;
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
            parent.locked.Remove(this);
            parent.CheckFree();
        }
       
    }

}