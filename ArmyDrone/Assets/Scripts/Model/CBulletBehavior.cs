using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBulletBehavior : MonoBehaviour
{
	[SerializeField] private float speedUp = 3f;
	public int damage = 40;
	Rigidbody2D _rb;
	[SerializeField] private GameObject explosionEffect;
	[SerializeField] public AudioClip explosionSound;
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
		AudioSource audio = GetComponent<AudioSource>();

		if (collision != null && collision.gameObject.tag == "enemy")
		{
			var explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

			audio.PlayOneShot(explosionSound);

			//Destroy(gameObject);
			Destroy(explosion,1f);
		}
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "enemy")
		{
			Destroy(this.gameObject);
		}
	}

}
