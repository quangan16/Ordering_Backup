using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : Solid
{
    [SerializeField] Character character;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] List<SpriteRenderer> spritePieces = new List<SpriteRenderer>();
    public override void OnDespawn()
    {
        base.OnDespawn();
        
        FreeCharacter();
    }

    void FreeCharacter()
    {
        character.OnFree();
    }    
    void BreakDown()
    {
        sprite.enabled = false;
        foreach(var spr in spritePieces)
        {
            //spr.transform.Do
            spr.enabled = true;

            float random = Random.Range(-90, 90);
            Vector3 pos = new Vector3(Mathf.Sin(random), Mathf.Cos(random), 0);
            spr.transform.DOLocalRotate(Vector3.forward * random + spr.transform.rotation.y * Vector3.up + spr.transform.rotation.x * Vector3.right, 0.5f, RotateMode.LocalAxisAdd);
            Vector3 scale = spr.transform.localScale;
            spr.transform.DOScale(scale * 9 / 8f, 0.5f);
            spr.transform.DOJump(spr.transform.position + pos, 1f, 1, 0.8f);


           
        }
        foreach(var spr in GetComponentsInChildren<SpriteRenderer>())
        {
            spr.DOFade(0f, 1f).OnComplete(() => OnDeath());
        }



    }
    public override void MoveDeath()
    {
        BreakDown();
    }
}
