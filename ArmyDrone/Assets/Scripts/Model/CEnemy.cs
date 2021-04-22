using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model
{
	public class CEnemy : MonoBehaviour
	{
		public int health = 100;

		private Animator animator;

		private void Awake()
		{
			animator = this.GetComponent<Animator>();
		}
		public void TakeDamage(int damage)
		{
			health -= damage;

			if (health <= 0)
			{
				Die();
			}
		}

		void Die()
		{
			CAudioManager.Instance.PlaySFX(ESoundsFx.EnemyDie);
			var mCollider = this.GetComponent<BoxCollider2D>();
			mCollider.enabled = false;
			animator.SetInteger("MovingParam", 2);
		}
	}
}

