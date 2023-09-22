using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Solid : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem ps;
    public SpriteRenderer shadow;
    public List<SpriteRenderer> spriteShadow = new List<SpriteRenderer>();
    public List<Clamp> triggered = new List<Clamp>();
    public List<Clamp> locked;
    public Vector3 lastPosition;
    public AudioSource blinkVoice => GetComponent<AudioSource>();
    public Collider2D[] colliders => GetComponentsInChildren<Collider2D>();
    public SpriteRenderer[] spriteRenderers => GetComponentsInChildren<SpriteRenderer>();
    //public HashSet<Collider2D> triggered = new HashSet<Collider2D>();
    public SpriteRenderer currentSkin;
    public bool isTouch = false;
    public bool isDead = false;
    public static bool canClick = true;
    Level level => GetComponentInParent<Level>();
    private void Awake()
    {
       OnInit();
    }
    private void OnMouseUp()
    {
        canClick = true;
        GameManager.Instance.SubtractMove();
        GameManager.Instance.StopAllCoroutines();
    }

    public virtual void OnInit()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.centerOfMass = Vector2.zero;
        isTouch = false;
        canClick = true;
        spriteShadow = GetComponentsInChildren<Clamp>().ToList().Select(x => x.shadow).ToList();
        spriteShadow.Add(shadow);
        lastPosition = transform.position;
    }
    public void ChangeSkin(Sprite sprite)
    {
        currentSkin.sprite = sprite;
        foreach(Clamp clamp in GetComponentsInChildren<Clamp>())
        {
            clamp.ChangeSkin(sprite);
        }

    }
    public void SetLastPosition(Vector3 position)
    { lastPosition = position; }
    public virtual void OnDespawn()
    {
        if (level != null)
        {
            
            level.solidList.Remove(this);
            if (level.isWin)
            {
                canClick= false;
                UIManager.Instance.DeactiveButtons();
                DOVirtual.DelayedCall(1.0f, () =>
                {
                    UIManager.Instance.OnWin();
                });

            }
        }
            
        if (!isDead)
        {
            Clamp[] clamps= GetComponentsInChildren<Clamp>();
            for (int i = 0;i<clamps.Length;i++)
            {
                clamps[i].isDead = true;
                clamps[i].ChangeGreen(false);
            }
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.None;

            foreach (Collider2D colid in colliders)
            {
                colid.enabled = false;
            }

            MoveDeath();
            Instantiate(ps, transform.position, Quaternion.identity);
            blinkVoice.Play();

        }
        isDead = true;




    }
    public virtual void OnSelected()
    {
        if(this is Circle || this is Line)
        foreach(var sp in spriteShadow)
        {
            sp.enabled = true;
        }
   
    }
    public virtual void OffSelected()
    {
        if (this is Circle || this is Line)
        {
            foreach (var sp in spriteShadow)
            {
                sp.enabled = false;
            }
  
        }
        
        rb.bodyType = RigidbodyType2D.Static;
        isTouch = false;
    }
    public virtual void MoveDeath()
    {

        //if (CompareTag("Lock"))
        //{
        //    transform.DORotate(Vector3.forward * 720, 1f, RotateMode.FastBeyond360);
        //    transform.DOScale(0.2f, 1f);
        //}
        float random = Random.Range(-60,60);
        Vector3 pos = new Vector3( Mathf.Sin(random), Mathf.Cos(random),0);
        rb.gravityScale = 1f;
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            SpriteRenderer sprite = spriteRenderers[i];
            sprite.DOFade(0.2f, 1.3f);
        }
      
        transform.DOLocalRotate( Vector3.forward*random+transform.rotation.y*Vector3.up+transform.rotation.x*Vector3.right, 0.5f,RotateMode.LocalAxisAdd);
        float x = transform.localScale.x;
        transform.DOScale(x*9/8f, 0.5f);     
        transform.DOJump(transform.position + pos, 1f, 1, 0.8f).OnComplete(() =>
        {
            OnDeath();
        }
        );





    }
    private void OnDisable()
    {
        transform.DOKill();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            SpriteRenderer sprite = spriteRenderers[i];
            sprite.DOKill();
        }

    }
    public void OnDeath()
    {
        
        Destroy(this);
        gameObject.SetActive(false);
        
        
    }
    public bool RemoveTrigger(Clamp clamp)
    {
       return triggered.Remove(clamp);
    }
    public void AddTrigger(Clamp clamp)
    {
        triggered.Add(clamp);
    }
    public bool ContainTrigger(Clamp clamp)
    {
        return triggered.Contains(clamp);
    }
    public void AddLock(Clamp clamp)
    {
        locked.Add(clamp);
    }
    public bool ContainLock(Clamp clamp)
    { return locked.Contains(clamp); }
    public bool RemoveLock(Clamp clamp)
    { return locked.Remove(clamp); }
    public virtual void CheckFree()
    {
        if (triggered.Count == 0 && ! isTouch )
        {
            CheckLocked();
        }
    }
    public void CheckLocked()
    {
        if (locked.Count == 0)
        {
            if (GameManager.Instance.gameMode == GameMode.Normal)
            {
                OnDespawn();
            }
            else
            {
                if (GameManager.timer > 0)
                {
                    OnDespawn();
                }
            }
        }
        
    }
    public virtual void SetUp()
    {
    }
    public virtual void Move(Vector3 vectorA, Vector3 vectorB )
    {
       
    }
}
