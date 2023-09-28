using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject sprite;
    private void Start()
    {
        Vector3 scale = sprite.transform.localScale;
        sprite.transform.DOScale(new Vector3(scale.x * 1.05f, scale.y * 0.95f), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(Constant.TAG_BLOCK)|| collision.gameObject.CompareTag(Constant.TAG_LOCK))
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
