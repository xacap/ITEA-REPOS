using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float maxHp = 1000f;
        public float currentHp = 1000f;

        public float damage = 100f;

        protected float playerRealizeRange = 10f;
        protected float attackRange = 5f;
        protected float attackCoolTime = 5f;
        protected float attackCoolTimeCacl = 5f;
        protected bool canAtk = true;

        protected float moveSpeed = 2f;

        protected GameObject Player;
        protected NavMeshAgent nvAgent;
        protected float distance;

        protected GameObject parentRoom;

        protected Animator Anim;
        protected Rigidbody _rb;

        public LayerMask layerMaskPlayer;

        protected void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            nvAgent = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();
            Anim = GetComponent<Animator>();

            StartCoroutine(CalcCoolTime());
        }

       
        protected bool CanAtkStateFun()
        {
            Vector3 targetDir = new Vector3(Player.transform.position.x - transform.position.x, 0f, Player.transform.position.z - transform.position.z);

            Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir, out RaycastHit hit, 30f, layerMaskPlayer);
            distance = Vector3.Distance(Player.transform.position, transform.position);

            if (hit.transform == null)
            {
                return false;
            }

            if (hit.transform.CompareTag("Player") && distance <= attackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual IEnumerator CalcCoolTime()
        {
            while (true)
            {
                yield return null;
                if (!canAtk)
                {
                    attackCoolTimeCacl -= Time.deltaTime;
                    if (attackCoolTimeCacl <= 0)
                    {
                        attackCoolTimeCacl = attackCoolTime;
                        canAtk = true;
                    }
                }
            }
        }
    }
}

