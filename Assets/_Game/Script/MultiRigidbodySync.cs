using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MultiRigidbodySync : Solid
{
    public static MultiRigidbodySync instance;
    static float angle;
    public static List<ColorC> colorCs => FindObjectsOfType<ColorC>().ToList();

    private void Start()
    {
        instance = this;
        OnInit();
    }
       
    private void FixedUpdate()
    {
        bool shouldStopAll = false; 
        if(isTouch)
        {
            foreach (var c in colorCs)
            {
               
                if (!c.isMove)
                {
                    shouldStopAll = true; // Set the flag to true if any Rigidbody2D has stopped
                    break;
                }
            }

            if (shouldStopAll)
            {
                // If any Rigidbody2D has stopped, set the velocity of all Rigidbody2D components to zero
                foreach (var c in colorCs)
                {
                    c.rb.angularVelocity = 0;
                }
            }
        }
        
    }
    public override void CheckFree()
    {
        foreach (var c in colorCs)
        {
            c.isTouch = false;
            c.rb.bodyType = RigidbodyType2D.Static;          
            c.CheckFree();

        }
    }
    public override void OffSelected()
    {
        isTouch = false;
        foreach (var c in colorCs)
        {           
            c.OffSelected();       
        }
    }
    public override void OnSelected()
    {
        
        foreach (var c in colorCs)
        {
            
            c.OnSelected();
           
        }
    }
    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {
        isTouch = true;
        angle = Vector3.SignedAngle(vectorA, vectorB, Vector3.forward);
        foreach (var c in colorCs)
        {
            c.isTouch = true;
            c.OnSelected();
            //  c.MoveNotSync(angle);
        }

    }









}

