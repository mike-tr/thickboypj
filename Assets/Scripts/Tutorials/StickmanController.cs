using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    public Animator animator;

    public Rigidbody2D hip;
    public Limb handR;
    public Limb handL;
    public Limb legR;
    public Limb legL;

    public float hipForce = 500;
    public float addedForce = 1f;
    public float moveSpeed = 500;
    public float jumpForce = 500;

    int frameIndex = 0;
    int dir = 1;

    public int Jumps = 2;
    public int JumpCharge = 1;

    private float jtime = 0;

    Camera cam;

    public bool IsMe = true;
    private bool isDead;
    private void Start() {
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    float rattack = 0;
    float lattack = 0;

    public void SetGrounded(bool value) {
        if (value) {
            if(jtime > Time.timeSinceLevelLoad) {
                return;
            }
            JumpCharge = Jumps;
        }
    }

    public void Kill() {
        isDead = true;
        legL.resting = true;
        legR.resting = true;
    }

    bool walking = false;
    void Update()
    {
        if (isDead) {
            return;
        }
        RotateTo(hip, 0, hipForce);

        if (!IsMe)
            return;

        var direction = Input.GetAxis("Horizontal");
        if(Mathf.Abs(direction) > 0) {
            hip.AddForce(moveSpeed * direction * Vector2.right);
            //if (frameIndex > 10) {
            //    frameIndex = 0;
            //    dir *= -1;
            //}

            //legL.SetPosition(Vector2.right * 10 * dir + Vector2.down * 8);
            //legR.SetPosition(-Vector2.right * 10 * dir + Vector2.down * 8);
            //frameIndex++;
            animator.SetBool("walk", true);
            walking = true;
        }else if (walking) {
            walking = false;
            animator.SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if(JumpCharge > 0) {
                hip.velocity = Vector2.zero;
                hip.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                JumpCharge--;
                jtime = Time.timeSinceLevelLoad + .1f;
            }                 
        }

        var reverse = 1f;
        if (Input.GetKey(KeyCode.R)) {
            reverse = -1f;
        }

        if (Input.GetKey(KeyCode.E)) {
            var dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            handR.SetPosition(dir * reverse, 5f);
        }

        if (Input.GetKey(KeyCode.Q)) {
            var dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            handL.SetPosition(dir * reverse, 5f);
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

        rigidbody.angularVelocity *= .5f;
        rigidbody.AddTorque(angle * force * addedForce* x);
        addedForce = 1f;
    }
}
