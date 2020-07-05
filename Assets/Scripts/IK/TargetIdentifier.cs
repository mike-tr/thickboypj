using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIdentifier : MonoBehaviour {
    // Start is called before the first frame update
    public Dictionary<string, IKController> targets = new Dictionary<string, IKController> ();
    void Start () {
        foreach (var child in transform.GetComponentsInChildren<IKController> ()) {
            targets.Add (child.name, child);
        }
    }
}
