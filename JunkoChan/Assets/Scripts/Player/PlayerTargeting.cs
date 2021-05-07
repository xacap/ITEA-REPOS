using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerTargeting : MonoBehaviour
    {
        public static PlayerTargeting Instance // singlton     
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerTargeting>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("PlayerTargeting");
                        instance = instanceContainer.AddComponent<PlayerTargeting>();
                    }
                }
                return instance;
            }
        }
        private static PlayerTargeting instance;

        public bool getATarget = false;
        float currentDist = 0;      //Текущее расстояние
        float closetDist = 100f;    //Близкое расстояние
        float TargetDist = 100f;   //Целевое расстояние
        int closeDistIndex = 0;    //Ближайший индекс
        public int TargetIndex = -1;  //Индекс к цели
        int prevTargetIndex = 0;
        public LayerMask layerMask;

        public float atkSpd = 1f;

        public List<GameObject> MonsterList = new List<GameObject>(); //Monster List 

        public GameObject PlayerBolt;  //Снаряд
        public Transform AttackPoint;

        void Update()
        {
            SetTarget();
            AtkTarget();
        }

        void OnDrawGizmos()
        {
            if (getATarget)
            {
                for (int i = 0; i < MonsterList.Count; i++)
                {
                    if (MonsterList[i] == null) { return; }// Добавлять
                    RaycastHit hit; //	
                    bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position,//менять 
                                                out hit, 20f, layerMask);

                    if (isHit && hit.transform.CompareTag("Monster"))
                    {
                        Gizmos.color = Color.green;
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                    }
                    Gizmos.DrawRay(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position);//менять 
                }
            }
        }

        void Attack()
        {
            PlayerMovement.Instance.Anim.SetFloat("AttackSpeed", atkSpd);
            Instantiate(PlayerBolt, AttackPoint.position, transform.rotation);
        }

        void SetTarget()
        {
            if (MonsterList.Count != 0)
            {
                prevTargetIndex = TargetIndex;
                currentDist = 0f;
                closeDistIndex = 0;
                TargetIndex = -1;

                for (int i = 0; i < MonsterList.Count; i++)
                {
                    if (MonsterList[i] == null) { return; }   // Добавлять
                    currentDist = Vector3.Distance(transform.position, MonsterList[i].transform.GetChild(0).position);//менять 

                    RaycastHit hit;
                    bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position,//менять 
                                                out hit, 20f, layerMask);

                    if (isHit && hit.transform.CompareTag("Monster"))
                    {
                        if (TargetDist >= currentDist)
                        {
                            TargetIndex = i;

                            TargetDist = currentDist;

                            if (!JoystickMove.Instance.isPlayerMoving && prevTargetIndex != TargetIndex)  // Добавлять// Добавлять
                            {
                                TargetIndex = prevTargetIndex;
                            }
                        }
                    }

                    if (closetDist >= currentDist)
                    {
                        closeDistIndex = i;
                        closetDist = currentDist;
                    }
                }

                if (TargetIndex == -1)
                {
                    TargetIndex = closeDistIndex;
                }
                closetDist = 100f;
                TargetDist = 100f;
                getATarget = true;
            }

        }

        void AtkTarget()
        {
            if (TargetIndex == -1 || MonsterList.Count == 0)  // Добавлять 
            {
                PlayerMovement.Instance.Anim.SetBool("Attack", false);
                return;
            }
            if (getATarget && !JoystickMove.Instance.isPlayerMoving && MonsterList.Count != 0)
            {
                Debug.Log ( "lookat : " + MonsterList[TargetIndex].transform.GetChild ( 0 ) );  // менять
                transform.LookAt(MonsterList[TargetIndex].transform.GetChild(0));     // менять

                if (PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    PlayerMovement.Instance.Anim.SetBool("Idle", false);
                    PlayerMovement.Instance.Anim.SetBool("Walk", false);
                    //PlayerMovement.Instance.Anim.SetBool ( "Attack", true );
                }

            }
            else if (JoystickMove.Instance.isPlayerMoving)
            {
                if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    PlayerMovement.Instance.Anim.SetBool("Attack", false);
                    PlayerMovement.Instance.Anim.SetBool("Idle", false);
                    PlayerMovement.Instance.Anim.SetBool("Walk", true);
                }
            }
            else
            {
                PlayerMovement.Instance.Anim.SetBool("Attack", false);
                PlayerMovement.Instance.Anim.SetBool("Idle", true);
                PlayerMovement.Instance.Anim.SetBool("Walk", false);
            }
        }
    }
}

