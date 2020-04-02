using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickman : IStickman
{
    public static List<PlayerStickman> stickman = new List<PlayerStickman>();


    public PlayerStickman(StickmanController controller) {
        stickman.Add(this);
        this.controller = controller;
        animator = controller.animator;
    }

    public bool IsAlive() {
        return controller.IsAlive();
    }

    private StickmanController controller;

    bool walking = false;
    private Animator animator;
    public void Update()
    {
        var direction = Input.GetAxis("Horizontal");
        if (Mathf.Abs(direction) > 0) {
            controller.hip.AddForce(controller.moveSpeed * direction * Vector2.right);
            animator.SetBool("walk", true);
            walking = true;
        } else if (walking) {
            walking = false;
            animator.SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            controller.Jump();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            animator.SetBool("attack", true);
            //var dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //handR.SetPosition(dir * reverse, 5f);
        } else if (Input.GetKeyUp(KeyCode.E)) {
            animator.SetBool("attack", false);
        }

        //if (Input.GetKey(KeyCode.Q)) {
        //    var dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //    handL.SetPosition(dir * reverse, 5f);
        //}
    }

    public StickmanController getController() {
        return controller;
    }
}
