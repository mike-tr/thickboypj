using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanAi : IStickman
{
    public StickmanAi(IController controller)
    {
        this.controller = controller;
        animator = controller.GetAnimator();
    }

    private IController controller;

    bool walking = false;
    private Animator animator;

    public IController target;

    public Transform transform
    {
        get { return controller.transform; }
    }
    public IController getController()
    {
        return controller;
    }

    public void Update()
    {
        if (target)
        {
            if (!target.IsAlive())
            {
                target = null;
                animator.SetBool("walk", false);
                animator.SetBool("attack", false);
                return;
            }
            var direction = target.transform.position - transform.position;
            if (Mathf.Abs(direction.magnitude) > 5)
            {
                controller.hip.AddForce(controller.GetMoveSpeed() * direction * Vector2.right);
                animator.SetBool("walk", true);
                walking = true;
                animator.SetBool("attack", false);
            }
            else if (walking)
            {
                walking = false;
                animator.SetBool("walk", false);
            }
            else
            {
                animator.SetBool("attack", true);
            }
        }

        foreach (var stickman in PlayerStickman.stickman)
        {
            if (stickman.IsAlive())
            {
                target = stickman.getController();
            }
        }
    }


}
