using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementClampUI : MonoBehaviour
{
    [SerializeField] Image clampImage;
    [SerializeField] Image clampGreen;
    public Clamp clamp;
    void Start()
    {
        
    }

    public void OnInit()
    {
        clampImage.sprite = clamp.GetComponentInParent<SpriteRenderer>().sprite;
        clampGreen.gameObject.SetActive(clamp.green.enabled);
    }
}
