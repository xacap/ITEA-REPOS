using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 20f;
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(" Name : " + other.transform.name);
            if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Monster"))
            {
                //Debug.Log(" Name : " + other.transform.name);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                Destroy(gameObject, 0.2f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log(" Name : " + collision.transform.name);
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Monster"))
            {
                //Debug.Log(" Name : " + collision.transform.name);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                Destroy(gameObject, 0.2f);
            }
        }
    }
}


