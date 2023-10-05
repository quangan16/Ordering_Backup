using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] SkeletonAnimation skeletonAnimation;

    [SerializeField] ParticleSystem helpParticle;

    private float intervalPartileTime = 6.0f;

    private float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        InCage();
    }

    void Update()
    {
        LoopHelpParticle();
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

    public void LoopHelpParticle()
    {
        if (skeletonAnimation.AnimationName == Constant.ANIM_IDLE)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= intervalPartileTime)
            {
                elapsedTime = 0;
                helpParticle.Play();
            }
        }
        else
        {
            helpParticle.Stop();
            return;
        }
    }


}
