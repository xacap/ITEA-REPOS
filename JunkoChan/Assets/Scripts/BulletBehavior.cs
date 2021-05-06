using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onescreenDelay = 3f;

    void Update()
    {
        Destroy(this.gameObject, onescreenDelay);
    }
}
