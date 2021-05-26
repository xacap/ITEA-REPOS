using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        private Rigidbody _rb;
        private Vector3 NewDir;
        public int bounceCnt = 2;
        public int wallBounceCnt = 2;
        public int dmg = 250;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            NewDir = transform.up;
            _rb.velocity = transform.forward * 20f;
        }

        Vector3 ResultDir (int index)
        {
            int closetIndex = -1;
            float closetDis = 500f;
            float currentDis = 0;

            for (int i = 0; i < PlayerTargeting.Instance.MonsterList.Count; i++)
            {
                if (i == index) continue;
                currentDis = Vector3.Distance(PlayerTargeting.Instance.MonsterList[i].transform.position, transform.position);

                if (currentDis > 5) continue;

                if (closetDis > currentDis)
                {
                    closetDis = currentDis;
                    closetIndex = i;
                }
            }

            if (closetIndex == -1)
            {
                Debug.Log("Что-то не так!");
                Destroy(gameObject, 0.2f);
                return Vector3.zero;
            }
            return (PlayerTargeting.Instance.MonsterList[closetIndex].transform.position - transform.position).normalized;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Monster"))
            {
                if (PlayerData.Instance.PlayerSkill[0] != 0 && PlayerTargeting.Instance.MonsterList.Count >= 2)
                {
                    int myIndex = PlayerTargeting.Instance.MonsterList.IndexOf(other.gameObject.transform.parent.gameObject);

                    if(bounceCnt > 0)
                    {
                        bounceCnt--;
                        PlayerData.Instance.dmg *= 0.7f;
                        NewDir = ResultDir(myIndex) * 20f;
                        _rb.velocity = NewDir;
                        return;
                    }
                }
                _rb.velocity = Vector3.zero;
                Destroy(gameObject, 0.1f);
            }


            if (other.transform.CompareTag("Wall"))
            {
                if (PlayerData.Instance.PlayerSkill[4] == 0)
                {
                    _rb.velocity = Vector3.zero;
                    Destroy(gameObject, 0.1f);
                }
                    
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Wall"))
            {
                if (PlayerData.Instance.PlayerSkill[4] != 0)
                {
                    if (wallBounceCnt > 0)
                    {
                        wallBounceCnt--;
                        PlayerData.Instance.dmg *= 0.5f;
                        NewDir = Vector3.Reflect(NewDir, collision.contacts[0].normal);
                        _rb.velocity = NewDir * 20f;
                        return;
                    }
                }
                _rb.velocity = Vector3.zero;
                Destroy(gameObject);
            }
        }
    }
}


