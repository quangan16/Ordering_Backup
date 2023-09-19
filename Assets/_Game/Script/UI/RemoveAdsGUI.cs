using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAdsGUI : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void DenyBtn()
    {
        Close();
    }

    public void Accept()
    {
    }
}
