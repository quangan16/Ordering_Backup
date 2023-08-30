using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solid : MonoBehaviour
{
    public bool canMove = true;
    public bool isHit;
    public Transform shadow;
    public Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        shadow = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHit)
        {
            shadow = transform;
            canMove = false;
        }
        else
        {

            
            BounceBack();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            isHit = true;
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            isHit = false;
           
        }
    }
    public void BounceBack()
    {
        canMove = true;
        transform.DOSmoothRewind();
        transform.DOPlayBackwards();
        transform.DOKill();
        transform.rotation = shadow.rotation;
        transform.position = shadow.position;
    }
   public void PerformCircularRotation()
    {
        // where is our center on screen?
        Vector3 center = Camera.main.WorldToScreenPoint(transform.position);

        // angle to previous finger
        float anglePrevious = Mathf.Atan2(center.x - lastPosition.x, lastPosition.y - center.y);

        Vector3 currPosition = Input.mousePosition;

        // angle to current finger
        float angleNow = Mathf.Atan2(center.x - currPosition.x, currPosition.y - center.y);

        lastPosition = currPosition;

        // how different are those angles?
        float angleDelta = angleNow - anglePrevious;

        // rotate by that much
        transform.Rotate(new Vector3(0, 0, angleDelta * Mathf.Rad2Deg));
    }
}
