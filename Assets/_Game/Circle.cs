using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Solid
{
    // Start is called before the first frame update

    
    void Start()
    {
       // transform.DORotate(Vector3.forward*360, 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
      if(!canMove)
        {
           // transform.DOPause();
        }
    }
   
}
