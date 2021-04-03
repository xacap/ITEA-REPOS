using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBehavior : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            Destroy(this.gameObject);
        }
    }
}
