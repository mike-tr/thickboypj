using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IController : MonoBehaviour {
    const int DeadLayer = 10;
    public Rigidbody2D hip;

    public Transform Points;
    public TargetIdentifier identifier;
    public bool flipped = false;
    public bool isDead = false;

    public void Init () {
        identifier = Points.GetComponent<TargetIdentifier> ();
        if (!identifier)
            identifier = Points.gameObject.AddComponent<TargetIdentifier> ();
    }

    public void TryFlip (float direction) {
        if (!Points) {
            return;
        }
        if (flipped) {
            if (direction > 0) {
                FlipAnimation ();
            }
        } else if (direction < 0) {
            FlipAnimation ();
        }
    }
    public void FlipAnimation () {
        flipped = !flipped;
        var scale = Points.localScale;
        scale.x *= -1;
        Points.localScale = scale;
    }
    public abstract void SetGrounded (bool value);
    public abstract bool IsAlive ();
    public abstract void Jump ();
    public abstract float GetMoveSpeed ();
    public abstract Animator GetAnimator ();
    public virtual void Kill () {
        isDead = true;
        var animator = GetAnimator ();
        var parameters = GetAnimator ().parameters;
        for (int i = 0; i < parameters.Length; i++) {
            if (parameters[i].type == AnimatorControllerParameterType.Bool) {
                animator.SetBool (parameters[i].name, false);
            } else if (parameters[i].type == AnimatorControllerParameterType.Float) {
                animator.SetFloat (parameters[i].name, 0);
            } else if (parameters[i].type == AnimatorControllerParameterType.Int) {
                animator.SetInteger (parameters[i].name, 0);
            }
        }
        animator.SetBool ("dead", true);

        // nothing crazy just take the stickman and change all its layers to dead,
        // maybe there is a better what but hey it works. ( dont need to worry about collisions between alive once and dead )
        changeAllLayers (transform, DeadLayer);
    }

    public void changeAllLayers (Transform transform, int layer) {
        foreach (Transform t in transform) {
            t.gameObject.layer = layer;
            changeAllLayers (t, layer);
        }
    }
}
