using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // Start is called before the first frame update

    public float updateRate = 1f;
    void Start()
    {
        Time.fixedDeltaTime *= updateRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
