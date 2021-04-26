using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

namespace Items
{
    public class CHeratBehavior : MonoBehaviour
    {
		
		public int health = 50;
		public float onescreenDelay = 5f;

		void Update()
		{
			Destroy(this.gameObject, onescreenDelay);
		}


		void OnTriggerEnter2D(Collider2D collision)
		{
			CPlayer player = collision.GetComponent<CPlayer>();

			if (player != null && collision.gameObject.tag == "player")
			{
				player.SetDamage(health);
				Destroy(this.gameObject);
			}
		}
	}
}

