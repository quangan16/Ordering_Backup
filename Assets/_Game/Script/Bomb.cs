using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] AudioClip audioClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block")|| collision.gameObject.CompareTag("Lock"))
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
        if (GameManager.isVibrate)
        {
            Handheld.Vibrate();
        }
        SoundManager.Instance.PlaySound(audioClip);
        Instantiate(ps,transform.position,transform.rotation);
        gameObject.SetActive(false);
    }
}
