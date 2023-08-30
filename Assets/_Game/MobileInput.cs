using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MobileInput : MonoBehaviour
{
    Solid target;
    Vector3 anchor;
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Solid solid = hit.collider.GetComponent<Solid>();
                if (solid != null)
                {
                    target = solid;
                    anchor = Input.mousePosition;
                    return;
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            target = null;
        }
        if (target != null)
        {
            
            if (target.canMove)
            {
                
                Vector3 vector = Camera.main.WorldToScreenPoint(target.transform.position);
                Vector3 mouse = Input.mousePosition;
         
                float angle = Mathf.Atan2(mouse.y-anchor.y-vector.y,mouse.x- anchor.x-vector.x) * Mathf.Rad2Deg;
                target.transform.DORotate(Vector3.forward*angle,0.1f);
                


            }


        }
    }
}
