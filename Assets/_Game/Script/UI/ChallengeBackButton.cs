using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeBackButton : MonoBehaviour
{
    [SerializeField] private FadePopup challengePopup;
    // Start is called before the first frame update
    public void Back()
    {
        challengePopup.Hide();
    }
}
