using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IController : MonoBehaviour
{
    public Rigidbody2D hip;

    public Transform Points;
    public bool flipped = false;

    public void TryFlip(float direction)
    {
        if (!Points)
        {
            return;
        }
        if (flipped)
        {
            if (direction > 0)
            {
                FlipAnimation();
            }
        }
        else if (direction < 0)
        {
            FlipAnimation();
        }
    }
    public void FlipAnimation()
    {
        flipped = !flipped;
        var scale = Points.localScale;
        scale.x *= -1;
        Points.localScale = scale;
    }
    public abstract void SetGrounded(bool value);
    public abstract bool IsAlive();
    public abstract void Jump();
    public abstract float GetMoveSpeed();
    public abstract Animator GetAnimator();
    public abstract void Kill();
}

