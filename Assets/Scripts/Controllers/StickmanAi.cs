using System.Collections.Generic;
using UnityEngine;
public class StickmanAi : IStickman {
    public static List<StickmanAi> stickman = new List<StickmanAi> ();
    public StickmanAi (IController controller) {
        stickman.Add (this);
        this.controller = controller;
        animator = controller.GetAnimator ();
    }

    bool walking = false;
    private Animator animator;

    public IController target;

    public Transform transform {
        get { return controller.transform; }
    }

    public override void Update () {
        if (target) {
            if (!target.IsAlive ()) {
                target = null;
                animator.SetBool ("walk", false);
                animator.SetBool ("attack", false);
                return;
            }
            var direction = target.transform.position - transform.position;
            controller.TryFlip (direction.x);
            if (Mathf.Abs (direction.magnitude) > 5) {
                controller.hip.AddForce (controller.GetMoveSpeed () * direction.normalized * Vector2.right);
                animator.SetBool ("walk", true);
                walking = true;
                animator.SetBool ("attack", false);
            } else if (walking) {
                walking = false;
                animator.SetBool ("walk", false);
            } else {
                animator.SetBool ("attack", true);
            }
        } else {
            foreach (var stickman in PlayerStickman.stickman) {
                if (stickman.IsAlive ()) {
                    target = stickman.getController ();
                    break;
                }
            }
            if (!target) {
                foreach (var stickman in stickman) {
                    if (stickman != this && stickman.IsAlive ()) {
                        target = stickman.getController ();
                        break;
                    }
                }
            }
            Debug.Log (target);
        }
    }
}