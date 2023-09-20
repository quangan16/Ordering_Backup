using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            Solid solid = Cache.GetSolid(collision.collider);
            if (solid != GetComponentInParent<Solid>())
            {
                solid.OnDespawn();
                OnDespawn();
            }
        }
          

    }
    void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}
