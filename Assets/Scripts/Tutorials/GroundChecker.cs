using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    int grounded = 0;
    IController controller;
    // Start is called before the first frame update
    void Start () {
        controller = GetComponentInParent<IController> ();
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (grounded < 1) {
            controller.SetGrounded (true);
        }
        grounded++;
    }

    private void OnCollisionExit2D (Collision2D collision) {
        grounded--;
        if (grounded < 1) {
            controller.SetGrounded (false);
            grounded = 0;
        }
    }

}