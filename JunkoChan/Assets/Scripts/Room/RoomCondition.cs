using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Room
{
    public class RoomCondition : MonoBehaviour
    {
        List<GameObject> MonsterListInRoom = new List<GameObject>();
        public bool playerInThisRoom = false;
        public bool isClearRoom = false;

        GameObject NextGate;

        void Start()
        {
            NextGate = transform.GetChild(5).gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInThisRoom = true;
                PlayerTargeting.Instance.MonsterList = new List<GameObject>(MonsterListInRoom);
                Debug.Log("Открыта новая комната! Колличество врагов : " + PlayerTargeting.Instance.MonsterList.Count);

                StageMgr.Instance.CloseDoor.transform.position = NextGate.transform.position + new Vector3(0, 0, 0);
                StageMgr.Instance.OpenDoor.transform.position = NextGate.transform.position + new Vector3(0, 0, 0);
                StageMgr.Instance.OpenDoor.SetActive(false);
            }
            if (other.CompareTag("Monster"))
            {
                MonsterListInRoom.Add(other.transform.parent.gameObject);
            }
        }
        void LateUpdate()
        {
            if (playerInThisRoom)
            {
                Debug.Log("MonsterListInRoom.Count   " + MonsterListInRoom.Count);
                CheckMonsterList();
                if (MonsterListInRoom.Count <= 0 && !isClearRoom)
                {
                    isClearRoom = true;

                    StageMgr.Instance.CloseDoor.transform.position = new Vector3(-30, 0, 0);
                    StageMgr.Instance.OpenDoor.SetActive(true);
                }
            }
        }
       
        void CheckMonsterList()
        {
            if (MonsterListInRoom != null)
            {
                if (MonsterListInRoom.Count > 0 && !isClearRoom)
                {
                    for (int i = 0; i <= MonsterListInRoom.Count; i++)
                    {
                        if (MonsterListInRoom[i] == null)
                        {
                            MonsterListInRoom.Remove(MonsterListInRoom[i]);
                        }
                    }
                }
            }
            
            
        }

       

        /*
         * 
         * 
         * 
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInThisRoom = false;
                PlayerTargeting.Instance.MonsterList.Clear();
                Debug.Log("Игрок вышел!");
            }
            if (other.CompareTag("Monster"))
            {
                MonsterListInRoom.Remove(other.transform.parent.gameObject);
            }
        }

        
        */
    }
}

