using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;
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
    [SerializeField] private AudioSource hintVoice;
    public Collider2D[] colliders => GetComponentsInChildren<Collider2D>();
    public SpriteRenderer[] spriteRenderers => GetComponentsInChildren<SpriteRenderer>();
    //public HashSet<Collider2D> triggered = new HashSet<Collider2D>();
    public SpriteRenderer currentSkin;
    public bool isTouch = false;
    public bool isDead = false;
    public static bool canClick = true;
    Clamp[] clamps;
    Level level;
    private void Awake()
    {
        OnInit();
    }
    private void OnMouseUp()
    {
        canClick = true;
        GameManager.Instance.SubtractMove();
        //level.CheckFree();
        GameManager.Instance.StopAllCoroutines();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.otherCollider.GetComponent<Bomb>() && !collision.collider.GetComponent<Bomb>())
        {
            if (GameManager.isVibrate)
            {
                Handheld.Vibrate();
            }
            SoundManager.Instance.Play();
        }

    }
    public virtual void OnInit()
    {
        clamps = GetComponentsInChildren<Clamp>();
        level = GetComponentInParent<Level>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.centerOfMass = Vector2.zero;
        isTouch = false;
        canClick = true;
        spriteShadow = clamps.ToList().Select(x => x.shadow).ToList();
        spriteShadow.Add(shadow);
        lastPosition = transform.position;
    }
    public void ChangeSkin(SkinItem skinItem)
    {
        currentSkin.color = skinItem.ChangeColorbyID(GetComponent<SpriteRenderer>().sprite.name);

        if (this is Circle)
        {
            currentSkin.sprite = skinItem.spriteC;
            foreach (Clamp clamp in clamps)
            {
                clamp.ChangeSkin(skinItem.spriteL);
                clamp.currentSkin.color = currentSkin.color;
            }

        }
        else if(this is Line || CompareTag(Constant.TAG_BLOCK))
        {
            currentSkin.sprite = skinItem.spriteL;
            foreach (Clamp clamp in clamps)
            {
                clamp.ChangeSkin(skinItem.spriteL);
                clamp.currentSkin.color = currentSkin.color;
            }
        }    
        

    }
    Color ChangeColorbyID(SkinType type)
    {
        Color color = Color.white;
        byte alpha = 80;
        string s = GetComponent<SpriteRenderer>().sprite.name;
        string numberString = new string(s.Where(char.IsDigit).ToArray());
        int i;
        if (!string.IsNullOrEmpty(numberString))
        {
            if (int.TryParse(numberString, out i) && i >= 1 && i <= 10)
            {
                if (type == SkinType.LeoPattern )
                {
                    switch (i)
                    {
                        case 1:
                            {
                                color = new Color32(95, 27, 18,alpha);
                                break;
                            }
                        case 2:
                            {
                                color = new Color32(121, 27, 20,alpha);
                                break;
                            }

                        case 3:
                            {
                                color = new Color32(117, 15, 15,alpha);
                                break;
                            }
                        case 4:
                            {
                                color = new Color32(87, 29, 116,alpha);
                                break;
                            }
                        case 5:
                            {
                                color = new Color32(90, 116, 44,alpha);
                                break;
                            }
                        case 6:
                            {
                                color = new Color32(113, 0, 0,alpha);
                                break;
                            }
                        case 7:
                            {
                                color = new Color32(130, 43, 43,alpha);
                                break;
                            }
                        case 8:
                            {
                                color = new Color32(140, 53, 68,alpha);
                                break;
                            }
                        case 9:
                            {
                                color = new Color32(57, 125, 35,alpha);
                                break;
                            }
                        case 10:
                            {
                                color = new Color32(99, 27, 71,alpha);
                                break;
                            }

                    }
                }
                else if (type == SkinType.Star  || type == SkinType.Wavy)
                {
                    alpha = 200;
                    switch (i)
                    {
                        case 1:
                            {
                                color = new Color32(69, 219, 233,alpha);
                                break;
                            }
                        case 2:
                            {
                                color = new Color32(227, 117, 0,alpha);
                                break;
                            }

                        case 3:
                            {
                                color = new Color32(249, 200, 222,alpha);
                                break;
                            }
                        case 4:
                            {
                                color = new Color32(254, 229, 255,alpha);
                                break;
                            }
                        case 5:
                            {
                                color = new Color32(192, 241, 197,alpha);
                                break;
                            }
                        case 6:
                            {
                                color = new Color32(246, 216, 139,alpha);
                                break;
                            }
                        case 7:
                            {
                                color = new Color32(252, 196, 246,alpha);
                                break;
                            }
                        case 8:
                            {
                                color = new Color32(196, 252, 247,alpha);
                                break;
                            }
                        case 9:
                            {
                                color = new Color32(252, 247, 196,alpha);
                                break;
                            }
                        case 10:
                            {
                                color = new Color32(252, 196, 246,alpha);
                                break;
                            }
                    }
                }
            }
                

        }
        else
        {
        }
        return color;
    }
    public void SetLastPosition(Vector3 position)
    { lastPosition = position; }
    public void OnDespawnHint()
    {
        foreach (var sp in spriteShadow)
        {
            sp.enabled = true;
        }
        Invoke(nameof(OnDespawn), 0.5f);
    }
    public virtual void OnDespawn()
    {
        if (level != null)
        {

            level.solidList.Remove(this);
            if (level.isWin)
            {
                canClick = false;
                UIManager.Instance.DeactiveButtons();
                DOVirtual.DelayedCall(1.0f, () =>
                {
                    UIManager.Instance.OnWin();
                });

            }
        }

        if (!isDead)
        {
            
            for (int i = 0; i < clamps.Length; i++)
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
        OnClampChange();
        UIManager.Instance.ShowClamp();

    }
    public void OnClampChange()
    {
        UIClamp.clamps = ListOfClamp();
        UIManager.Instance.OnClampChange();

    }
    public List<Clamp> ListOfClamp()
    {
        return triggered.Concat(locked).ToList();
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
        UIManager.Instance.OffClamp();
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
        float random = Random.Range(-60, 60);
        Vector3 pos = new Vector3(Mathf.Sin(random), Mathf.Cos(random), 0);
        rb.gravityScale = 1f;
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            SpriteRenderer sprite = spriteRenderers[i];
            sprite.DOFade(0.2f, 1.3f);
        }

        transform.DOLocalRotate(Vector3.forward * random + transform.rotation.y * Vector3.up + transform.rotation.x * Vector3.right, 0.5f, RotateMode.LocalAxisAdd);
        Vector3 scale = transform.localScale;
        transform.DOScale(scale * 9 / 8f, 0.5f);
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
        if (triggered.Count == 0 && !isTouch)
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
    public virtual void Move(Vector3 vectorA, Vector3 vectorB)
    {

    }
}
