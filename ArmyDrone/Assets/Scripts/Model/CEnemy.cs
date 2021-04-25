using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model
{
	public class CEnemy : MonoBehaviour
	{
		public int health = 100;

		private Animator animator;
		private GameObject heartPrefab;
		private Transform transformParrent;
		private GameObject baseLeyer;
		private Transform baseLayerTransform;

		private void Awake()
		{
			animator = this.GetComponent<Animator>();
			heartPrefab = Resources.Load<GameObject>("Items/Heart");
			transformParrent = this.gameObject.transform.parent;
			baseLeyer = GameObject.Find("BaseLeyer");
			baseLayerTransform = baseLeyer.transform;
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
			Instantiate(heartPrefab, transformParrent.position, transformParrent.rotation, baseLayerTransform);
			CAudioManager.Instance.PlaySFX(ESoundsFx.EnemyDie);
			var mCollider = this.GetComponent<BoxCollider2D>();
			mCollider.enabled = false;
			animator.SetInteger("MovingParam", 2);
		}
	}
}

