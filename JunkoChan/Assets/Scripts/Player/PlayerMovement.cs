using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Room;
using Enemy;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerMovement>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("PlayerMovement");
                        instance = instanceContainer.AddComponent<PlayerMovement>();
                    }
                }
                return instance;
            }
        }
        private static PlayerMovement instance;


        private Rigidbody _rb;
        public float moveSpeed = 5f;
        public Animator Anim;

        public void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Anim = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            if (JoystickMove.Instance.JoyVec.x != 0 || JoystickMove.Instance.JoyVec.y != 0)
            {
                _rb.velocity = new Vector3(JoystickMove.Instance.JoyVec.x, 0, JoystickMove.Instance.JoyVec.y) * moveSpeed;
                _rb.rotation = Quaternion.LookRotation(new Vector3(JoystickMove.Instance.JoyVec.x, 0, JoystickMove.Instance.JoyVec.y));
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NextRoom"))
            {
                StageMgr.Instance.NextStage();
            }

            if (other.transform.CompareTag("MeleeAtk"))
            {
                other.transform.parent.GetComponent<EnemyDuck>().meleeAtkArea.SetActive(false);
                PlayerHpBar.Instance.currentHp -= other.transform.parent.GetComponent<EnemyDuck>().damage * 2f;

                if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Dmg"))
                {
                    Anim.SetTrigger("Dmg");
                    Instantiate(EffectSet.Instance.PlayerDmgEffect, PlayerTargeting.Instance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
                }

            }

            if (other.transform.CompareTag("BossMeleeAttack"))
            {
                PlayerHpBar.Instance.currentHp -= other.transform.parent.GetComponent<EnemyStageBoss>().damage * 2f;
                PlayerData.Instance.currentHp -= other.transform.parent.GetComponent<EnemyStageBoss>().damage * 2f;

                if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Dmg"))
                {
                    Anim.SetTrigger("Dmg");
                    Instantiate(EffectSet.Instance.PlayerDmgEffect, PlayerTargeting.Instance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
                }
            }

            if (PlayerTargeting.Instance.MonsterList.Count <= 0 && other.transform.CompareTag("EXP"))
            {
                PlayerData.Instance.PlayerExpCalc(100);
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("RangeAtk"))
            {
                Destroy(collision.gameObject, 0.1f);

                PlayerHpBar.Instance.currentHp -= collision.transform.GetComponent<BossBolt>().damage;
                PlayerData.Instance.currentHp -= collision.transform.GetComponent<BossBolt>().damage;

                if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Dmg"))
                {
                    Anim.SetTrigger("Dmg");
                    Instantiate(EffectSet.Instance.PlayerDmgEffect, PlayerTargeting.Instance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
                }
            }
        }
    }
}


