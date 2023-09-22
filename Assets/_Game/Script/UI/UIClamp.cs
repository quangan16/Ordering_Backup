using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClamp : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] ElementClampUI elementClamp;
    public static List<Clamp> clamps= new List<Clamp>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnInit()
    {
        foreach(ElementClampUI ecui in GetComponentsInChildren<ElementClampUI>()) 
        { 
            Destroy(ecui.gameObject);
            Destroy(ecui);

        }
        for(int i = 0;i<clamps.Count;i++)
        {
            ElementClampUI eCUI = Instantiate(elementClamp, content.transform) ;
            eCUI.clamp = clamps[i];
            eCUI.OnInit();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
