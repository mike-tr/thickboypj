using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour {
    public Hinge2DIkSolver solver { get; private set; }
    // Start is called before the first frame update
    bool set = false;

    public float force = 0;
    public Vector3 position;
    public bool overridep = false;
    public void Init (Hinge2DIkSolver solver) {
        if (set)
            return;
        set = true;
        this.solver = solver;
        this.solver.CallBackOnSolve += OnSolve;
    }

    public void overrideAnim (Vector3 newPos, float newForce) {
        position = newPos;
        force = newForce;
        overridep = true;
    }

    private void LateUpdate () {
        if (overridep) {
            transform.position = position;
            solver.force = force;
            solver.active = true;
            overridep = false;
        }
    }

    void OnSolve () {
        if (overridep) {
            Debug.Log (position);
            transform.position = position;
        }
    }
}
