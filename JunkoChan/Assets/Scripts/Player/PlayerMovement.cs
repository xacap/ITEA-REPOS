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
                Debug.Log(" Get Next Room ");
                StageMgr.Instance.NextStage();
            }

            if (other.transform.CompareTag("MeleeAtk"))
            {
                other.transform.parent.GetComponent<EnemyMummy>().meleeAtkArea.SetActive(false);
                PlayerHpBar.Instance.currentHp -= other.transform.parent.GetComponent<EnemyMummy>().damage * 2f;

                if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Dmg"))
                {
                    Anim.SetTrigger("Dmg");
                    //Instantiate(EffectSet.Instance.PlayerDmgEffect, PlayerTargeting.Instance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
                }

            }

            if (PlayerTargeting.Instance.MonsterList.Count <= 0 && other.transform.CompareTag("EXP"))
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }
}


