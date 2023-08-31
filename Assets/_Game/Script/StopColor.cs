using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopColor : MonoBehaviour
{
    ColorC colorC => GetComponentInParent<ColorC>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MultiRigidbodySync.SetSign();
        //colorC.SetOnTrigger();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MultiRigidbodySync.SetNegativeSign();
       // colorC.SetOffTrigger();

    }

}
