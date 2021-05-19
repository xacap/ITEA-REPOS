using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class SheepBolt : MonoBehaviour
    {
		[SerializeField] private float speedUp = -5f;
		//public int damage = 20;
		Rigidbody _rb;
		//[SerializeField] private GameObject explosionEffect;

		private void Awake()
		{
			_rb = GetComponent<Rigidbody>();
		}

		void Update()
		{
			Vector3 forwardUp = this.transform.position + this.transform.up * speedUp * Time.fixedDeltaTime;
			_rb.transform.position = forwardUp;
			
		}


		private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Wall"))
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }
}

