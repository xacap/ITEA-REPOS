using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerTargeting : MonoBehaviour
    {
        public static PlayerTargeting Instance
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

        private float mTime;
        public float delayAttack = 0.5f;
        private GameObject bulletPrefab;
        public Transform AttackPoint;

        public GameObject PlayerBolt;

        public float atkSpd = 1f;

        public List<GameObject> MonsterList = new List<GameObject>();

        void FixedUpdate()
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
                    if (MonsterList[i] == null) 
                    { 
                        return; 
                    }

                    RaycastHit hit;	
                    bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position, out hit, 20f, layerMask);

                    if (isHit && hit.transform.CompareTag("Monster"))
                    {
                        Gizmos.color = Color.green;
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                    }
                    Gizmos.DrawRay(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position); 
                }
            }
        }

        private void Attack()
        {
            PlayerMovement.Instance.Anim.SetFloat("AttackSpeed", atkSpd);
            Instantiate(PlayerData.Instance.PlayerBolt[PlayerData.Instance.PlayerSkill[2]], AttackPoint.position, transform.rotation);

            if (PlayerData.Instance.PlayerSkill[1] > 0)
            {
                Invoke("MultiShot", 0.2f);
            }

            if (PlayerData.Instance.PlayerSkill[3] > 0)
            {
                GameObject PotatoL =
                Instantiate(PlayerData.Instance.PlayerBolt[PlayerData.Instance.PlayerSkill[3] - 1], AttackPoint.position, transform.rotation);
                PotatoL.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -45f, 0));

                GameObject PotatoR =
                Instantiate(PlayerData.Instance.PlayerBolt[PlayerData.Instance.PlayerSkill[3] - 1], AttackPoint.position, transform.rotation);
                PotatoR.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45f, 0));
            }
        }

        void MultiShot()
        {
            Instantiate(PlayerData.Instance.PlayerBolt[PlayerData.Instance.PlayerSkill[2]], AttackPoint.position, transform.rotation);

            if (PlayerData.Instance.PlayerSkill[3] > 0)
            {
                GameObject PotatoL =
                Instantiate(PlayerData.Instance.PlayerBolt[PlayerData.Instance.PlayerSkill[3] - 1], AttackPoint.position, transform.rotation);
                PotatoL.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -45f, 0));

                GameObject PotatoR =
                Instantiate(PlayerData.Instance.PlayerBolt[PlayerData.Instance.PlayerSkill[3] - 1], AttackPoint.position, transform.rotation);
                PotatoR.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45f, 0));
            }
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
                    if (MonsterList[i] == null) 
                    { 
                        return; 
                    }   

                    currentDist = Vector3.Distance(transform.position, MonsterList[i].transform.GetChild(0).position);
                    
                    RaycastHit hit;
                    bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position, out hit, 20f, layerMask);


                    if (isHit && hit.transform.CompareTag("Monster"))
                    {
                        if (TargetDist >= currentDist)
                        {
                            TargetIndex = i;
                            TargetDist = currentDist;

                            if (!JoystickMove.Instance.isPlayerMoving && prevTargetIndex != TargetIndex)
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
            if (TargetIndex == -1 || MonsterList.Count == 0) 
            {
                PlayerMovement.Instance.Anim.SetBool("Attack", false);
                return;
            }
            if (getATarget && !JoystickMove.Instance.isPlayerMoving && MonsterList.Count != 0)
            {
                var qTo = Quaternion.LookRotation(MonsterList[TargetIndex].transform.GetChild(0).transform.position - transform.position);
                qTo = Quaternion.Slerp(transform.rotation, qTo, 10 * Time.deltaTime);
                GetComponent<Rigidbody>().MoveRotation(qTo);
                

                if (PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    PlayerMovement.Instance.Anim.SetBool("Idle", false);
                    PlayerMovement.Instance.Anim.SetBool("Run", false);
                    PlayerMovement.Instance.Anim.SetBool ("Attack", true );
                    //Attack();
                }
            }
            else if (JoystickMove.Instance.isPlayerMoving)
            {
                if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    PlayerMovement.Instance.Anim.SetBool("Attack", false);
                    PlayerMovement.Instance.Anim.SetBool("Idle", false);
                    PlayerMovement.Instance.Anim.SetBool("Run", true);
                }
            }
            else
            {
                PlayerMovement.Instance.Anim.SetBool("Attack", false);
                PlayerMovement.Instance.Anim.SetBool("Idle", true);
                PlayerMovement.Instance.Anim.SetBool("Run", false);
            }
        }
    }
}

