using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MultiRigidbodySync : Solid
{
    public static MultiRigidbodySync instance;
    public static float sign;
    static float angle;
    public static List<ColorC> colorCs => FindObjectsOfType<ColorC>().ToList();

    private void Start()
    {
        instance = this;
        OnInit();
    }
    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {
        angle = Vector3.SignedAngle(vectorA, vectorB, Vector3.forward);
        if(sign * angle <= 0)
        {
            
        
            foreach (var c in colorCs)
            {
                c.isTouch = true;
                c.MoveNotSync(angle);
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
    public static void SetSign()
    {
        sign =  angle > 0 ? 1 : -1;
        print(sign + ",  " + angle.ToString());
        angle = 0;
    }
    public static void SetNegativeSign()
    {
        sign = 0;
    }








}

