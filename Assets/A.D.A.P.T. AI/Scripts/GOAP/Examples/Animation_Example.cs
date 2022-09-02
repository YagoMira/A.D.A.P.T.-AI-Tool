using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Example : MonoBehaviour
{
    public AnimationClip anim;
    public AnimationClip anim_2;
    public Animator animator;
    AnimatorOverrideController aoc;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //anim.Play("Idle");
        aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = aoc;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (anim)
            {

                //var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                aoc["T-Pose"] = anim;
                animator.CrossFade("runnableaction", 0.1f, 0);
                /*foreach (var a in aoc.animationClips)
                {
                    Debug.Log("ANIMATION NAME:" + a.name);
                    anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, anim));
                }*/

                //aoc.ApplyOverrides(anims);
                //animator.runtimeAnimatorController = aoc;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (anim)
            {

                //var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                aoc["T-Pose"] = anim_2;
                animator.CrossFade("runnableaction", 0.1f, 0);
                /*foreach (var a in aoc.animationClips)
                {
                    Debug.Log("ANIMATION NAME:" + a.name);
                    anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, anim));
                }*/

                //aoc.ApplyOverrides(anims);
                //animator.runtimeAnimatorController = aoc;
            }
        }


        //animator.CrossFade("Idle", 2f, 0);

    }


}
