using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
	public enum EPlayerState
	{
		ePlayerActive,
		ePlayerWinner,
		ePlayerLost
	}

	public class CPlayer : MonoBehaviour
	{
		private EPlayerState mState = EPlayerState.ePlayerActive;
		public int health = 100;

		private Animator animator;

		private CGameController _gameController;


		private void Awake()
		{
			animator = this.GetComponent<Animator>();
			_gameController = GameObject.Find("GameManager").GetComponent<CGameController>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "FinishTrigger")
			{
				mState = EPlayerState.ePlayerWinner;
				_gameController.LevelPassed();
			}
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

			mState = EPlayerState.ePlayerLost;
			_gameController.IsGameFinished();

			//Destroy(gameObject);
		}
	}
}


