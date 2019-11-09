using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSetter : MonoBehaviour
{
    // Start is called before the first frame update

    public SpiderRope[] rope;
    private int index = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            index = index > rope.Length - 1 ? 0 : index;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rope[index].setStart(worldPos);
            index++;
        }
    }
}
