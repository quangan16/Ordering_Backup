using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : Solid
{
    public bool isVertical;
    Vector3 direct;
    Vector3 startPosition;
    Vector2 target;
    Vector3 initCameraPos;
    float maxDiff;
    private void Start()
    {
        mainCamera = Camera.main;
        initCameraPos = mainCamera.transform.position;
        Vector3 screenCorner = mainCamera.ScreenToWorldPoint(Vector3.right * Screen.width)-mainCamera.ScreenToWorldPoint(Vector3.zero);
        maxDiff = screenCorner.x * 0.2f;
        OnInit();
        startPosition = transform.localPosition;
    }
    public override void OnInit()
    {
        base.OnInit();

    }
    public override void SetUp()
    {
        if (isVertical)
        {
            direct = transform.up;
        }
        else
        {
            direct = transform.right;
        }
        rb.freezeRotation = true;
    }
    private void Update()
    {
        if (!isDead)
        {
            if (isVertical)
            {
                transform.localPosition = new Vector3(startPosition.x, transform.localPosition.y);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, startPosition.y);
            }

        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.otherCollider.GetComponent<Bomb>() && !collision.collider.GetComponent<Bomb>())
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.velocity = -rb.velocity * 0.5f;
            }
            if (GameManager.isVibrate)
            {
                Handheld.Vibrate();
            }
            SoundManager.Instance.Play();
        }


    }
    public override void Move(Vector3 vectorA, Vector3 vectorB)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector3 endPoint = Vector3.Project(vectorB - vectorA, direct);
        target = new Vector2(endPoint.x, endPoint.y);

        rb.velocity = (target / Time.deltaTime).normalized * Mathf.Clamp((target / Time.deltaTime).magnitude, -100, 100);
        //rb.MovePosition(target + rb.position);


    }
    public override void MoveDeath()
    {
        //Instantiate(ps, transform.position, Quaternion.identity);
        //blinkVoice.Play();

        //transform.DOLocalMove((transform.localPosition*2 - startPosition), 1f);
        base.MoveDeath();
    }
    public override void OnSelected()
    {
        base.OnSelected();
        foreach (var sp in spriteShadow)
        {
            //  sp.enabled = true;
        }
    }
    //-----------------------------------------
    private Vector3 offset;
    private Camera mainCamera;
    private float dragThreshold = 10f;
    void OnMouseDown()
    {
        offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    void OnMouseDrag()
    {
        Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
        CheckCameraMovement(cursorPosition);

    }
    private void OnMouseUp()
    {
        mainCamera.transform.position = initCameraPos;
    }
    void CheckCameraMovement(Vector3 objectPosition)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(objectPosition);
        Vector3 viewportPoint2 = mainCamera.WorldToViewportPoint(transform.position);

        float eps = 0.15f;
        if ((viewportPoint.x < eps || viewportPoint.x > 1 - eps || viewportPoint.y < eps || viewportPoint.y > 1 - eps)
           // || (viewportPoint2.x < eps || viewportPoint2.x > 1 - eps || viewportPoint2.y < eps || viewportPoint2.y > 1 - eps)
            )
        {
            Vector3 cameraTargetPosition = new Vector3(
                Mathf.Clamp(objectPosition.x, mainCamera.transform.position.x - dragThreshold, mainCamera.transform.position.x + dragThreshold),
                Mathf.Clamp(objectPosition.y, mainCamera.transform.position.y - dragThreshold, mainCamera.transform.position.y + dragThreshold),
                mainCamera.transform.position.z
            );

            Vector3 lastPosition = Vector3.Project(cameraTargetPosition-mainCamera.transform.position, direct);
            lastPosition.z = 0;
            Vector3 pos = Vector3.Lerp(mainCamera.transform.position, mainCamera.transform.position+lastPosition, Time.deltaTime * 2f);

            mainCamera.transform.position = new Vector3( Mathf.Clamp(pos.x,initCameraPos.x-maxDiff,initCameraPos.x+maxDiff),
                Mathf.Clamp(pos.y, initCameraPos.y - maxDiff, initCameraPos.y + maxDiff),
                mainCamera.transform.position.z

                );
            
        }
    }
    bool CheckCamera(Vector3 objectPosition)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(objectPosition);
        Vector3 viewportPoint2 = mainCamera.WorldToViewportPoint(transform.position);

        float eps = 0.15f;
        if ((viewportPoint.x < eps || viewportPoint.x > 1 - eps || viewportPoint.y < eps || viewportPoint.y > 1 - eps)
            || (viewportPoint2.x < eps || viewportPoint2.x > 1 - eps || viewportPoint2.y < eps || viewportPoint2.y > 1 - eps)
            )
        {
            return true;
        }
        return false;


    }




}
