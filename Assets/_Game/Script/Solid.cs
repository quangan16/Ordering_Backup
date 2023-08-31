using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Solid : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D[] colliders => GetComponentsInChildren<Collider2D>();
    public SpriteRenderer[] spriteRenderers => GetComponentsInChildren<SpriteRenderer>();
    //public HashSet<Collider2D> triggered = new HashSet<Collider2D>();
    public List<Clamp> triggered = new List<Clamp>();
    public List<Clamp> locked ;
    public Vector3 lastPosition;
    public bool isTouch = false;
    public bool isDead = false;
    public bool canClick = true;
    Level level => GetComponentInParent<Level>();
    private void Start()
    {
       OnInit();
    }        
    private void OnMouseDown()
    {   if(canClick)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Solid solid = Cache.GetSolid(hit.collider);
                if (solid != null)
                {
                    solid.isTouch = true;
                    MobileInput.target = solid;
                    MobileInput.anchor = Input.mousePosition;

                }
            }


        }
        
    }
    public virtual void OnInit()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.centerOfMass = Vector2.zero;
        isTouch = false;
        canClick = true;
    }
    public void SetLastPosition(Vector3 position)
    { lastPosition = position; }
    public virtual void OnDespawn()
    {
        if (!isDead)
        {
            Clamp[] clamps= GetComponentsInChildren<Clamp>();
            for (int i = 0;i<clamps.Length;i++)
            {
                clamps[i].isDead = true;               
            }
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.None;

            foreach (Collider2D colid in colliders)
            {
                colid.enabled = false;
            }
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                SpriteRenderer sprite = spriteRenderers[i];
                sprite.DOFade(0, 1f);
            }
            MoveDeath();
            
        }
        isDead = true;
       
    

    }
    public virtual void MoveDeath()
    {
        if(CompareTag("Lock"))
        {
            transform.DORotate(Vector3.forward * 720, 1f, RotateMode.FastBeyond360);
            transform.DOScale(0.2f, 1f);

        }
        
        Invoke(nameof(OnDeath), 1.5f);
    }
    public void OnDeath()
    {
       
        Destroy(this);
        gameObject.SetActive(false);
        if(level != null)
        level.solidList.Remove(this);
       
    }
    public virtual void CheckFree()
    {
        if (triggered.Count == 0 && ! isTouch )
        {
            CheckLocked();
        }
    }
    public void CheckLocked()
    {
        if(locked.Count == 0)
        OnDespawn();
    }
    public virtual void SetUp()
    {
    }
    public virtual void Move(Vector3 vectorA, Vector3 vectorB )
    {
       
    }

}
