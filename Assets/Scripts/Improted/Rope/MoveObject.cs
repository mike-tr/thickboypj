using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Target;
    public Vector3 strength = Vector2.one;

    Camera cam;
    LayerMask mask;
    void Start()
    {
        cam = Camera.main;
        mask = LayerMask.GetMask("Movable");

        Physics2D.positionIterations = 300;
        Physics2D.velocityIterations = 300;
    }

    // Update is called once per frame
    Vector3 move;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            move = Input.mousePosition;
            
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(move), Vector3.forward, 10, mask);
            if (hit.collider) {
                Target = hit.collider.transform;
            }
        }
        else if (Input.GetKey(KeyCode.Mouse0) && Target) {
            var add = (Input.mousePosition - move);
            Target.position += new Vector3(add.x * strength.x, add.y * strength.y, add.z * strength.z) * Time.deltaTime;
            move = Input.mousePosition;
        }else if(Input.GetKeyUp(KeyCode.Mouse0)) {
            Target = null;
        }
    }
}
