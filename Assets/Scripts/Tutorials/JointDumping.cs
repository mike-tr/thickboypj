using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointDumping : MonoBehaviour {
    // Start is called before the first frame update
    Rigidbody2D rg;

    [Range (0, 1f)]
    public float dumping = .8f;
    void Start () {
        rg = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {
        rg.angularVelocity *= dumping;
    }
}