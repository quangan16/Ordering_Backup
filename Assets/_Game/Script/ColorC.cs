using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ColorC : Circle
{
    public List<ColorC> colorCs => FindObjectsOfType<ColorC>().ToList() ;
    public static float angle;
    public static float sign;
    
    public void Sync()
    {       
        foreach (var c in colorCs)
        {
            c.isTouch = true;
            c.OnSelected();
            //c.MoveNotSync(angle);
        }
        
    }
    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {
        MobileInput.target = MultiRigidbodySync.instance;
        MultiRigidbodySync.instance.transform.position = transform.position;
        //angle = Vector3.SignedAngle(vectorA, vectorB, Vector3.forward);
        //Sync();
    }
    public void MoveNotSync(float angle)
    {
        base.SetUp();
        if(angle * sign <=0)
        {
            rb.MoveRotation(rb.rotation + angle);
        }
    }
   

    public void SetOnTrigger()
    {
       // sign = Mathf.Sign(angle);
       // angle = 0;      
    }
    public void SetOffTrigger()
    {
        //sign = -Mathf.Sign(angle);        
    }

   


}
