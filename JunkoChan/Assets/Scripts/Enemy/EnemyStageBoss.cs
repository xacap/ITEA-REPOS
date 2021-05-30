using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Room;
using Player;
using UI;

namespace Enemy
{
    public class EnemyStageBoss : EnemyMeleeFSM
    {
        public GameObject BossBolt;
        public Transform AttackPoint;

        WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
        WaitForSeconds Delay1000 = new WaitForSeconds(1f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, playerRealizeRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        new protected void Start()
        {
            base.Start();
            attackCoolTime = 2f;
            attackCoolTimeCacl = attackCoolTime;

            playerRealizeRange = 13f;
            attackRange = 20f;
            moveSpeed = 1f;
            nvAgent.stoppingDistance = 4f;
        }


        protected override void InitMonster()
        {
            maxHp = 100000f;
            currentHp = maxHp;
            damage = 300f;
        }
        protected override IEnumerator Attack()
        {
            yield return null;
            int RandomAction = Random.Range(0, 3);

            nvAgent.isStopped = true;
            transform.LookAt(Player.transform.position);
            switch (RandomAction)
            {
                case 0:
                    if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
                    {
                        Anim.SetTrigger("Attack01");
                    }
                    break;
                case 1:
                    if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02"))
                    {
                        Anim.SetTrigger("Attack02");
                    }
                    break;
                case 2:
                    if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
                    {
                        Anim.SetTrigger("GetHit");
                    }
                    nvAgent.stoppingDistance = 0f;
                    nvAgent.SetDestination(Player.transform.position);
                    //yield return Delay500;

                    nvAgent.isStopped = false;
                    nvAgent.speed = 200f;
                    yield return Delay1000;
                    break;
            }
            canAtk = false;

            yield return Delay500;

            nvAgent.speed = moveSpeed;
            nvAgent.stoppingDistance = attackRange;
            currentState = State.Idle;
        }

        public void Attack01()
        {
            GameObject PotatoL =
                      Instantiate(BossBolt, AttackPoint.position, transform.rotation);
            PotatoL.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -35f, 0));

            GameObject PotatoC =
            Instantiate(BossBolt, AttackPoint.position, transform.rotation);
            PotatoC.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 0));

            GameObject PotatoR =
            Instantiate(BossBolt, AttackPoint.position, transform.rotation);
            PotatoR.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 35f, 0));
        }

        public void Attack02()
        {
            GameObject PotatoL1 =
                     Instantiate(BossBolt, AttackPoint.position, transform.rotation);
            PotatoL1.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -25f, 0));

            GameObject PotatoL2 =
            Instantiate(BossBolt, AttackPoint.position, transform.rotation);
            PotatoL2.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -10f, 0));

            GameObject PotatoR1 =
            Instantiate(BossBolt, AttackPoint.position, transform.rotation);
            PotatoR1.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 25f, 0));

            GameObject PotatoR2 =
            Instantiate(BossBolt, AttackPoint.position, transform.rotation);
            PotatoR2.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 10f, 0));
        }

        protected override void AtkEffect()
        {
            Instantiate(EffectSet.Instance.DuckAtkEffect, transform.position, Quaternion.Euler(90, 0, 0));
        }

        void Update()
        {
            if (currentHp <= 0)
            {
                nvAgent.isStopped = true;

                _rb.gameObject.SetActive(false);
                PlayerTargeting.Instance.MonsterList.Remove(transform.parent.gameObject);
                PlayerTargeting.Instance.TargetIndex = -1;

                Vector3 CurrentPostion = new Vector3(transform.position.x, 3f, transform.position.z);
                for (int i = 0; i < (StageMgr.Instance.currentStage / 10 + 2 + Random.Range(0, 3)); i++)
                {
                    Debug.Log(i);
                    GameObject ExpClone =
                    Instantiate(PlayerData.Instance.ItemExp, CurrentPostion, transform.rotation);
                    ExpClone.transform.parent = gameObject.transform.parent.parent;
                }
                UiController.Instance.CheckBossRoom(false);

                Destroy(transform.parent.gameObject);
                return;
            }
            else
            {
                UiController.Instance.BossCurrentHp = currentHp;
                UiController.Instance.BossMaxHp = maxHp;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Potato"))
            {
                float potatodmg = other.gameObject.GetComponent<PlayerWeapon>().dmg;

                UiController.Instance.Dmg();

                Instantiate(EffectSet.Instance.DuckDmgEffect, other.transform.position, Quaternion.Euler(90, 0, 0));

                GameObject DmgTextClone =
                Instantiate(EffectSet.Instance.MonsterDmgText, transform.position, Quaternion.identity);//Quaternion.Euler ( 53, 0, 0 ) );

                float RaVal = Random.value;
                //            Debug.Log ( " Duck RaVal : " + RaVal );  //Test
                if (RaVal < 0.5)
                {
                    currentHp -= potatodmg;
                    DmgTextClone.GetComponent<DmgTxt>().DisplayDamage(potatodmg, false);
                }
                else
                {
                    currentHp -= potatodmg * 2f;
                    DmgTextClone.GetComponent<DmgTxt>().DisplayDamage(potatodmg * 2f, true);
                }

                Destroy(other.gameObject);
            }
        }
    }
}

