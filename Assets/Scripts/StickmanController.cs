using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    public Rigidbody2D hip;
    public Limb handR;
    public Limb handL;
    public Limb legR;
    public Limb legL;

    public float hipForce = 500;
    public float moveSpeed = 500;
    public float jumpForce = 500;

    int frameIndex = 0;
    int dir = 1;
    void Update()
    {
        RotateTo(hip, 0, hipForce);

        var direction = Input.GetAxis("Horizontal");
        if(Mathf.Abs(direction) > 0) {
            hip.AddForce(moveSpeed * direction * Vector2.right);
            if(frameIndex > 10) {
                frameIndex = 0;
                dir *= -1;
            }

            legL.SetPosition(Vector2.right * 10 * dir + Vector2.down * 7);
            legR.SetPosition(-Vector2.right * 10 * dir + Vector2.down * 7);
            frameIndex++;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            hip.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    void RotateTo(Rigidbody2D rigidbody, float angle, float force) {
        angle = Mathf.DeltaAngle(transform.eulerAngles.z, angle);

        var x = angle > 0 ? 1 : -1;
        angle = Mathf.Abs(angle * .1f);
        if (angle > 2) {
            angle = 2;
        }
        angle *= .5f;
        angle *= (1 + angle);

        rigidbody.AddTorque(angle * force * x);
    }
}
