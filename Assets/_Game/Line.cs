using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : Solid
{
    Vector2 direct;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Block"))
        {
            
            Circle circle= GetComponentInParent<Circle>();
            {
                if(circle != null)
                {
                    circle.canMove = false;
                }
            }
        }
    }
}
