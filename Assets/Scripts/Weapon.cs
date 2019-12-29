using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float DamageMultiplier = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var mag = collision.relativeVelocity.magnitude;
        if (mag > 25) {
            Debug.Log(collision.relativeVelocity);
            var health = collision.gameObject.GetComponentInParent<Health>();
            if (health) {
                health.GetDamage(DamageMultiplier * mag * .05f);
            }
        }
    }
}
