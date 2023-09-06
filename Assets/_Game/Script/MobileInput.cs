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
        Screen.SetResolution(1080, 1920, true);
    }
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
           if(target!=null)
            {
                target.isTouch = false;
                target.OffSelected();
                target.rb.bodyType = RigidbodyType2D.Static;
                target.CheckFree();
            }
            target = null;
            ColorC.sign = 0;
        }
       
    }
    private void FixedUpdate()
    {
        
        if (target != null)
        {

            target.SetUp();

            Vector3 rPosition = Camera.main.WorldToScreenPoint(target.transform.position);
            Vector3 ray1 = anchor - rPosition;
            Vector3 mouse = Input.mousePosition - rPosition;
            target.Move(ray1/3,mouse/3);
            anchor = Input.mousePosition;

        }
        
    }


}
