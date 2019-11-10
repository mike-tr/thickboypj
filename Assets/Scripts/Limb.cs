using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    //the list of all the limb parts.
    private List<LimbPart> parts = new List<LimbPart>();

    // the end Root, with means all out position would be relative to this transform,
    // also we would not include root as part of the limb.
    public Transform root;

    // the target pos we wanna rotate to.
    public Vector2 targetPos;

    // force to controll how strong/fast the limb would be (its wierd).
    public float force = 100;
    [Range(0f, 1f)]
    // ratio !Just keep it HIGH!, at ratio 1 you would not be able to set new targetPos,
    // at ratio 0, you would get really unstable limbs.
    public float ratio = .9f;

    // in case the limb reaching the reversed direction
    public bool fliped = false;

    // in case the limb is facing the wrong direction with 90 degree Error.
    public bool sin = false;
    void Start()
    {
        //Get all the Hinges that are connceted to each other,
        //untill there is none, or the limb has no hingeJoint.
        var hinge = GetComponent<HingeJoint2D>();
        while(hinge != null && hinge.transform != root) {
            parts.Add(new LimbPart(hinge.attachedRigidbody, fliped, sin));
            hinge = hinge.connectedBody.GetComponent<HingeJoint2D>();
        }
        //?? not really needed. in this case parts[0] would be the Parent of the limb, the actuall "root".
        parts.Reverse();
    }

    // Update is called once per frame
    void Update()
    {
        // Actually, rotate each part to the target.
        // Add more force to the "parent" parts, because they simply need more force, to ignore the upper parts, swings.
        var index = parts.Count;
        foreach(var part in parts) {
            part.RotateToward(targetPos + (Vector2)root.position, force * (index * (index + 1) * .5f), ratio);
            index--;
        }
    }

    private void OnDrawGizmos() {
        // Draw gizmos for debug.
        foreach(var p in parts) {
            p.OnDrawGizmos();
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos + (Vector2)root.position, .1f);
    }
}
