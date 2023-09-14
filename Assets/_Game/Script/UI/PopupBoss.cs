using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBoss : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void PlayBtn()
    {
        Close();
        UIManager.Instance.OpenBoss();
    }

}
