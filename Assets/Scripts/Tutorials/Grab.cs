using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode key;
    public float range = 1f;

    private Transform parent;
    private FixedJoint2D joint;
    public Rigidbody2D rb;
    private float mass;
    private LayerMask mask;
    private Collider2D coll;
    void Start()
    {
        var circleColl = gameObject.AddComponent<CircleCollider2D>();
        circleColl.radius = .05f;
        coll = circleColl;
        rb = GetComponent<Rigidbody2D>();

        parent = transform;
        while (parent.parent) {
            parent = parent.parent;
        }
        mask = ~LayerMask.GetMask("Moveable");
        joint = gameObject.AddComponent<FixedJoint2D>();
        joint.enabled = false;
    }

    private void Update() {
        if (Input.GetKeyDown(key)) {
            if(joint.enabled) {
                if (joint.connectedBody) { 
                    joint.connectedBody.mass = mass;
                    joint.connectedBody = null;
                }
                joint.enabled = false;
            } else {
                var colliders = Physics2D.OverlapCircleAll(transform.position, range);
                foreach (var coll in colliders) {
                    if (coll.transform.IsChildOf(parent))
                        continue;
                    var rigidbody = coll.GetComponent<Rigidbody2D>();
                    if (rigidbody) {
                        var cd = coll.Distance(this.coll);
                        var pos = cd.pointA - cd.pointB;
                        rb.position += pos;

                        joint.connectedBody = rigidbody;
                        mass = rigidbody.mass;
                        rigidbody.mass = 1f;
                        joint.enabled = true;
                        break;
                    } else {
                        var cd = coll.Distance(this.coll);
                        var pos = cd.pointA - cd.pointB;
                        rb.position += pos;
                        joint.enabled = true;
                    }
                }


            }
        }
    }
}
