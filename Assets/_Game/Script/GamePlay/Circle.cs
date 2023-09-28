using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Circle : Solid
{
    [SerializeField] AudioClip stuckAu;
    float x;
    Quaternion z;
   
    private void Start()
    {
        OnInit();
        x = transform.localPosition.x;
        z = transform.rotation;
    }
    private void LateUpdate()
    {
        if(isTouch)
        {
            Stretch(GameManager.Instance.sprite, MobileInput.anchor);
        }
    }
    private void Update()
    {
        if (locked.Count==0)
        {
            z = transform.rotation;
        }
    }
    public override void SetUp()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;

    }
    public override void Move(Vector3 vectorA , Vector3 vectorB )
    {
        if (locked.Count == 0)
        {
            
            float angle = Vector3.SignedAngle(vectorA, vectorB, Vector3.forward);    
            //rb.MoveRotation(rb.rotation + angle);
            rb.angularVelocity = Mathf.Clamp(angle/Time.deltaTime,-1000,1000);
            GameManager.Instance.sprite.transform.position = transform.position;
            GameManager.Instance.sprite.gameObject.SetActive(true);

        }
        else
        {
           
            ShakeOff();
        }
    }
    public void Stretch(GameObject _sprite, Vector3 _finalPosition)
    {
        

        Vector3 direction = _sprite.transform.position- _finalPosition;
        direction.z = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _sprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); 

        Vector3 scale = Vector3.one;
        scale.y = Vector3.Magnitude(direction);
        _sprite.transform.localScale = scale;
    }
    public override void MoveDeath()
    {
        //Instantiate(ps, transform.position, Quaternion.identity);
        //blinkVoice.Play();

        //transform.DOMove((transform.position*2 - lastPosition), 1f);
        base.MoveDeath();
    }
    void ShakeOff()
    {
        if(GameManager.isVibrate)
        {
            Handheld.Vibrate();
        }
        StopCollision();
        blinkVoice.PlayOneShot(stuckAu);
        GameManager.Instance.sprite.gameObject.SetActive(false);
        transform.DOLocalMoveX(x+0.015f, 0.15f).OnComplete(()=> transform.DOLocalMoveX(x - 0.03f, 0.15f).OnComplete(() => StartCollision()   ));
        
    }
    public override void OffSelected()
    {
        base.OffSelected();

    }
    void StopCollision()
    {
        MobileInput.target = null;
        Collider2D[] collider = GetComponentsInChildren<Collider2D>();
        foreach(var col in collider)
        {
            col.isTrigger = true;
        }
        rb.angularVelocity = 0;
        canClick= false;
        isTouch = true;      
    }
    void StartCollision()
    {
        Collider2D[] collider = GetComponentsInChildren<Collider2D>();
        OffSelected();

        foreach (var col in collider)
        {
            if(col.gameObject.CompareTag("Block"))
            {
                col.isTrigger = false;

            }
        }
        transform.DOLocalMoveX(x, 0.2f);
        transform.rotation = z;
        canClick = true;
    }
    public override void OnSelected()
    {
       foreach (var sp in spriteShadow)
       {
           //sp.enabled = true;
       }
        if (!(this is ColorC))
        { OnClampChange(); }
        UIManager.Instance.ShowClamp();


    }

}
