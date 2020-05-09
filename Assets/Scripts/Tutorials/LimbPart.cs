using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbPart {
    private Rigidbody2D rigidbody;
    private Transform transform;

    public Vector2 RelativeOffset => offset.x * transform.right + offset.y * transform.up;
    private Vector2 offset;

    private bool flip = false;
    private bool sin = false;

    public LimbPart (Rigidbody2D rigidbody, bool fliped, bool sin) {
        this.rigidbody = rigidbody;
        this.transform = rigidbody.transform;
        this.flip = fliped;
        this.sin = sin;

        var hinge = transform.GetComponent<HingeJoint2D> ();
        offset = hinge.anchor;
    }

    Vector2 avgDirection;
    Vector2 avgWorldPos;
    const float wratio = .95f;

    public void RotateToward (Vector2 worldPos, float force, float ratio) {
        //Calculate changes in worldPos, translate them to ratio bump.
        var dif = (worldPos - avgWorldPos).magnitude;
        if (dif > 1) {
            dif = 1;
        }
        dif = 1 - dif;
        avgWorldPos = worldPos * (1 - wratio) + avgWorldPos * wratio;
        ratio += (1 - ratio) * dif * .95f;
        ///////////////////

        //Get the direction we want to rotate toward.
        var direction = worldPos - (RelativeOffset + (Vector2) transform.position);
        // in case the Z rotation 0 value is actually in the -x axis, we want to flip it.
        if (flip)
            direction *= -1;

        //Save the avgDirection, this is different because its Local avg Direction
        // A.k.a if the hand is swinging from side to side, this value going to always change but hopefully avg, on the middle point.
        // this would result in less unnesecarry movements.
        // if Ratio is set to 1, the part would simply not rotate at all and just lock on the current avgDiretion.
        avgDirection = direction * (1 - ratio) + avgDirection * ratio;
        var angle = AngleFromDirection (avgDirection);

        var cangle = transform.eulerAngles.z;
        //When out actually Rotation at z-0, is Down, we want to enable sin, to make the Z-0 axis face the right side.
        if (sin)
            cangle -= 90;
        //here we getting the actually part rotation, and will get how much we need to add, to get to the target.
        angle = Mathf.DeltaAngle (cangle, angle);

        //here we are normalizing the angle jump so we wont add crazy amount of torque,
        //we also trying to make low values even lower (by making v * (1 + v)) while keeping the max value to 2.
        var x = angle > 0 ? 1 : -1;
        angle = Mathf.Abs (angle * .1f);
        if (angle > 2) {
            angle = 2;
        }
        angle *= .5f;
        angle *= (1 + angle);

        //here we want to reduce angular velocity (build up angular velocity), so we would have better control over the actual rotation.
        rigidbody.angularVelocity *= angle * .5f;
        //here we just add the torque.
        rigidbody.AddTorque (angle * force * x);
    }

    private float AngleFromDirection (Vector2 dir) {
        //Get the angle by using some Acos on x, flipping the rotation when y is lower then 0.
        dir = dir.normalized;
        var angle = Mathf.Acos (dir.x) * Mathf.Rad2Deg;
        return dir.y > 0 ? angle : 360 - angle;
    }

    public void OnDrawGizmos () {
        // draw the "ankles" 
        // we need to call this method from another OnDrawGizmos on a Monobehavior script.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere ((Vector2) transform.position + RelativeOffset, .1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere ((Vector2) transform.position - RelativeOffset, .1f);
    }
}