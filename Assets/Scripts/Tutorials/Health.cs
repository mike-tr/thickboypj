using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private bool dead = false;

    StickmanController controller;

    private void Start() {
        controller = GetComponent<StickmanController>();
    }

    public void GetDamage(float damage) {
        health -= damage;
        if(health < 0) {
            IsDead();
        }
    }

    void IsDead() {
        dead = true;
        controller.Kill();
    }
}
