using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float DamageMultiplier = 2f;
    private Equipable part;
    private void Start () {
        part = GetComponent<Equipable> ();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D (Collision2D collision) {
        if (!part.parent)
            return;
        var mag = collision.relativeVelocity.magnitude;
        if (mag > 25) {
            var health = collision.gameObject.GetComponentInParent<Health> ();
            if (health) {
                health.GetDamage (DamageMultiplier * mag * .05f);
            }
        }
    }
}