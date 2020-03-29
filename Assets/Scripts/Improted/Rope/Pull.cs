using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    public float force = 70;
    public Rigidbody2D rg;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            Vector2 dir = rg.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = dir.normalized;

            //rg.transform.position -= (Vector3)dir * force * Time.deltaTime;
            //rg.MovePosition(rg.position + -dir * force * Time.deltaTime);
            rg.AddForce(-dir * force * Time.deltaTime * 1000);
        }
    }
}


