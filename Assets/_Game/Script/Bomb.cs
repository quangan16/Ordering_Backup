using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
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
        Instantiate(ps,transform.position,transform.rotation);
        gameObject.SetActive(false);
    }
}
