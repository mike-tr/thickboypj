using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private bool dead = false;

    IController controller;

    private void Start()
    {
        controller = GetComponent<IController>();
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            SetDead();
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    public void SetDead()
    {
        dead = true;
        controller.Kill();
    }
}
