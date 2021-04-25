using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
	public class CPlayer : MonoBehaviour
	{
		public int health = 100;

		private Animator animator;

		private int herts;
		private void Awake()
		{
			animator = this.GetComponent<Animator>();
			//health = CGameController.Instance.herat;
		}
		public void TakeDamage(int damage)
		{
			health -= damage;
			CGameController.Instance.herat = health;

			if (health <= 0)
			{
				Die();
			}
		}
		public void SetDamage(int healths)
		{
			health += healths;
			CGameController.Instance.herat = health;
		}

		void Die()
		{
			CAudioManager.Instance.PlaySFX(ESoundsFx.EnemyDie);
			animator.SetInteger("MovingParam", 2);
			//Destroy(gameObject);
		}
	}
}


