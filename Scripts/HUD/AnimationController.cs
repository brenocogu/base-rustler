using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    public Animator anim;
    int i = 1;

    public void ToggleAnimation() {
        anim.SetInteger("flow", i);
        i = -i;
    }
}
