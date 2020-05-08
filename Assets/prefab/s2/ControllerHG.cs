using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHG : IController
{
    public Animator animator;
    public float hipForce = 500;
    public float addedForce = 1f;
    public float moveSpeed = 500;
    public float jumpForce = 500;

    int frameIndex = 0;
    int dir = 1;

    public int Jumps = 2;
    public int JumpCharge = 1;

    private float jtime = 0;

    private IStickman brain;

    Camera cam;

    public bool IsMe = true;
    private bool isDead;
    private Health health;
    private void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        if (IsMe)
        {
            brain = new PlayerStickman(this);
        }
        else
        {
            brain = new StickmanAi(this);
        }
    }

    float rattack = 0;
    float lattack = 0;

    public override void SetGrounded(bool value)
    {
        if (value)
        {
            if (jtime > Time.timeSinceLevelLoad)
            {
                return;
            }
            JumpCharge = Jumps;
        }
    }

    public override void Kill()
    {
        isDead = true;
        // legL.resting = true;
        // legR.resting = true;
    }

    bool walking = false;

    public override float GetMoveSpeed()
    {
        return moveSpeed;
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }
        else
        {
            if (health.IsDead())
            {
                Kill();
            }
        }
        RotateTo(hip, 0, hipForce);
        brain.Update();
    }

    public override bool IsAlive()
    {
        return !isDead;
    }

    public override Animator GetAnimator()
    {
        return animator;
    }
    public override void Jump()
    {
        if (JumpCharge > 0)
        {
            hip.velocity = Vector2.zero;
            hip.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            JumpCharge--;
            jtime = Time.timeSinceLevelLoad + .1f;
        }
    }

    void RotateTo(Rigidbody2D rigidbody, float angle, float force)
    {
        angle = Mathf.DeltaAngle(transform.eulerAngles.z, angle);

        var x = angle > 0 ? 1 : -1;
        angle = Mathf.Abs(angle * .1f);
        if (angle > 2)
        {
            angle = 2;
        }
        angle *= .5f;
        angle *= (1 + angle);

        rigidbody.angularVelocity *= .5f;
        rigidbody.AddTorque(angle * force * addedForce * x);
        addedForce = 1f;
    }
}
