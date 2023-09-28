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
        InCage();
    }
    void ChangeAnim(string anim)
    {
        skeletonAnimation.AnimationName = anim;
    }
    public void OnFree()
    {
        ChangeAnim(Constant.ANIM_SUCCESS);
    }
    public void Cry()
    {
        ChangeAnim(Constant.ANIM_FAILED);
    }    
    public void InCage()
    {
        ChangeAnim(Constant.ANIM_IDLE);

    }


}
