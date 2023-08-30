using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ColorC : Circle
{
    public static bool canMoveLeft = true;
    public static bool canMoveRight = true;
    public List<ColorC> colorCs => FindObjectsOfType<ColorC>().ToList() ;
    public static float angle;
    public float curRot;
    public void Sync(Vector3 vectorA, Vector3 vectorB)
   {
        
        foreach (var c in colorCs)
        {

            c.MoveNotSync(vectorA, vectorB);
        }
   }
    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {
        angle = Vector3.SignedAngle(vectorA, vectorB, Vector3.forward);
        Sync(vectorA, vectorB);
        
    }
    public void MoveNotSync(Vector3 vectorA, Vector3 vectorB)
    {
        base.SetUp();
        rb.bodyType = RigidbodyType2D.Dynamic;
        if((angle > 0 && canMoveLeft) || (angle < 0 && canMoveRight))
        {
            rb.MoveRotation(rb.rotation + angle);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        angle = 0;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        angle = 0;
    }


}
