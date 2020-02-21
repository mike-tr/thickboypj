using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D[] colliders;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++) {
            for (int k = i+1; k < colliders.Length; k++) {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }
    }

    public void IgnoreCollider(Collider2D collider) {
        for (int i = 0; i < colliders.Length; i++) {
            Physics2D.IgnoreCollision(colliders[i], collider);
        }
    }

}
