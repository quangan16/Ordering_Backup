using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeButtonController : MonoBehaviour
{

    [SerializeField] private FadePopup fadePopup;

    public void Show()
    {
        fadePopup.gameObject.SetActive(true);
        fadePopup.Show(null);
    }
}
