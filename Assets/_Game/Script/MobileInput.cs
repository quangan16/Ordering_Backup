using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MobileInput : MonoBehaviour
{
    public static Solid target;
    public static Vector3 anchor;
    public static bool isStopped ;
    private void Start()
    {
        Application.targetFrameRate= 60;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GetOnClick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (target != null)
            {
                target.OffSelected();
                target.CheckFree();
            }
            target = null;
        }
    }
    void FixedUpdate()
    {
        
        if (target)
        {

            Vector3 rPosition = (target.transform.position);
            Vector3 ray1 = anchor - rPosition;
            ray1.z = 0;
            Vector3 mouse =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - rPosition;
            mouse.z = 0;
            target.Move(ray1, mouse);
            anchor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }
    public  void GetOnClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            Solid solid = Cache.GetSolid(hit.collider);
            if (solid != null)
            {
                if (UIControl.getHint && (solid is Line || solid is Circle))
                {
                    UIControl.getHint = false;
                    solid.OnDespawn();
                }
                else
                {
                    solid.isTouch = true;
                    solid.OnSelected();
                    solid.SetUp();
                    
                    target = solid;
                    
                    
                   anchor = Camera.main.ScreenToWorldPoint( Input.mousePosition);     
                    //anchor = ( Input.mousePosition);

                }

            }
        }
    }


}
