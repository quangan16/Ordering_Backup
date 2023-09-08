using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ColorC : Circle
{
    public ColorC[] colorCs => FindObjectsOfType<ColorC>() ;
    public static float angle;
    public bool isMove = true;
    float torque;

    public void OnCollisionStay2D(Collision2D collision)
    {
        //// print(collision.relativeVelocity);
        //ContactPoint2D contact =  collision.GetContact(0);
        //Vector2 offset = contact.point - rb.position;

        //foreach(var c in colorCs)
        //{
        //    //  c.rb.AddForceAtPosition(offset+c.rb.position,-contact.normal*contact.normalImpulse);
        //    c.rb.AddTorque(contact.normalImpulse * -160);
        //}
        
       
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //ContactPoint2D contact = collision.GetContact(0);
        //Vector2 offset = contact.point - rb.position;

        //foreach (var c in colorCs)
        //{
        //    //c.rb.AddForceAtPosition(offset + c.rb.position, -contact.normal * contact.normalImpulse);
        //    c.rb.AddTorque(contact.normalImpulse*-160);
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    public override void OnInit()
    {
        base.OnInit();        
    }
    public void Sync()
    {       
        foreach (var c in colorCs)
        {
            c.isTouch = true;
            c.OnSelected();                    
            c.MoveNotSync(angle);
        }



    }
    

    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {

        //MobileInput.target = MultiRigidbodySync.instance;
        //MultiRigidbodySync.instance.transform.position = transform.position;
        //MultiRigidbodySync.instance.isTouch = true;
        angle = Vector3.SignedAngle(vectorA, vectorB, Vector3.forward);
        Sync();
    }
    public void MoveNotSync(float angle)
    {
        base.SetUp();
        rb.angularVelocity = angle/Time.fixedDeltaTime;
    }
    public override void CheckFree()
    {
        foreach (var c in colorCs)
        {
            c.OffSelected();
        }
        base.CheckFree();

    }


}
