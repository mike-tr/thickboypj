using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimbAnimator {
    public Vector2 TargetPos;
    //dont really wanna change this one.
    //use this mainly when u want to change force to say -1f, or 
    public float ForceMultiplier = 1f;

    //basically this is for most porpuses, its just handy to have slider from 0-200%.
    [Range (0f, 2f)]
    public float SmallForceMultiplier;

    //this adds force to the limb.
    public Vector2 addedLimbVelocity;
    //this gonna dd force to the body.
    public Vector2 addedBodyVelocity;

    //basically adds -addedLimbVelocity * bodyCounterForce force to the body.
    public Vector2 bodyCounterForce;

    public Color gColor = Color.black;

    //basically how many frames for fade out.
    public int Activity = 0;
    public void DrawGizmos (Vector2 rootPos) {
        // Draw gizmos for debug.
        Gizmos.color = gColor;
        Gizmos.DrawSphere (rootPos + TargetPos, .1f);
    }
}