using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Room;

namespace Enemy
{
    public class BossBolt : MonoBehaviour
    {
        Rigidbody _rb;

        public float damage = 100f;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.velocity = transform.forward * 10;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Wall"))
            {
                Destroy(gameObject, 0.1f);
            }
        }

    }
}

