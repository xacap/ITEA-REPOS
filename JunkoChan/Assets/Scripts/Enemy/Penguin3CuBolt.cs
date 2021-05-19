using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Penguin3CuBolt : MonoBehaviour
    {
        Rigidbody _rb;
        Vector3 NewDir;
        int bounceCnt = 3;
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            NewDir = transform.up;
            _rb.velocity = NewDir * -10f;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(" Collision Name : " + collision.transform.name);
            if (collision.transform.CompareTag("Wall"))
            {
                bounceCnt--;
                if (bounceCnt > 0)
                {
                    Debug.Log("hit wall");
                    NewDir = Vector3.Reflect(NewDir, collision.contacts[0].normal);
                    _rb.velocity = NewDir * -10f;
                }
                else
                {
                    Destroy(gameObject, 0.1f);
                }
            }
        }
    }
}

