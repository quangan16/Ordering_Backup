using DG.Tweening;
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
    float[] rotationDiffernce;
 
    public void OnCollisionStay2D(Collision2D collision)
    {
        Keep();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Keep();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       
    }

    public override void OnInit()
    {
        base.OnInit();
        rotationDiffernce = new float[colorCs.Length];
        for (int i = 0; i< colorCs.Length; i++)
        {
            rotationDiffernce[i] = colorCs[i].rb.rotation - rb.rotation;
        }
    }
    void Keep()
    {
        for (int i = 0; i < colorCs.Length; i++)
        {
             colorCs[i].rb.rotation =  rb.rotation + rotationDiffernce[i];
        }

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
        rb.angularVelocity = angle / Time.fixedDeltaTime;

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
