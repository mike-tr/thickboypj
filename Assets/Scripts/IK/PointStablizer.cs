using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStablizer : MonoBehaviour {
    float offset = 0;
    Vector3 current;
    public float speed = 5;
    // Start is called before the first frame update
    void Start () {
        offset = transform.localEulerAngles.z;
        current = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update () {

    }

    private void LateUpdate () {
        transform.eulerAngles = current;
        current.z = Mathf.LerpAngle (current.z, offset - transform.localEulerAngles.z, Time.deltaTime * speed);
        transform.eulerAngles = current;
    }
}