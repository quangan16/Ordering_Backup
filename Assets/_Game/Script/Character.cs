using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] SkeletonAnimation skeletonAnimation;
    // Start is called before the first frame update
    void Start()
    {
        ChangeAnim("Idle");
    }
    void ChangeAnim(string anim)
    {


        skeletonAnimation.AnimationName = anim;


    }
    public void OnFree()
    {
        ChangeAnim("Success");
    }


}
