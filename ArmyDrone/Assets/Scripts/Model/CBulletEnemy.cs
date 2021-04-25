using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class CBulletEnemy : MonoBehaviour
    {
		[SerializeField] private float speedUp = 5f;
		public int damage = 20;
		Rigidbody2D _rb;
		[SerializeField] private GameObject explosionEffect;
		public float onescreenDelay = 3f;

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			Vector3 forwardUp = this.transform.position + this.transform.up * speedUp * Time.fixedDeltaTime;
			_rb.transform.position = forwardUp;
			Destroy(this.gameObject, onescreenDelay);
		}


		void OnTriggerEnter2D(Collider2D collision)
		{
			CPlayer player = collision.GetComponent<CPlayer>();

			if (player != null && collision.gameObject.tag == "player")
			{
				var explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
				CAudioManager.Instance.PlaySFX(ESoundsFx.BulletBoom);

				Destroy(gameObject);
				Destroy(explosion, 1f);

				player.TakeDamage(damage);

			}

		}
	}
}

