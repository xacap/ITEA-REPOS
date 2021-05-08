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

        void Update()
        {
            if (playerInThisRoom)
            {
                if (MonsterListInRoom.Count <= 0 && !isClearRoom)
                {
                    isClearRoom = true;
                    //Debug.Log(" Clear ");
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInThisRoom = true;
                PlayerTargeting.Instance.MonsterList = new List<GameObject>(MonsterListInRoom);
                //Debug.Log("Enter New Room! Mob Count : " + PlayerTargeting.Instance.MonsterList.Count);
                //Debug.Log ( "Player Enter New Room!" );
            }
            if (other.CompareTag("Monster"))
            {
                MonsterListInRoom.Add(other.transform.root.gameObject); 
                //Debug.Log(" Mob name : " + other.transform.root.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInThisRoom = false;
                PlayerTargeting.Instance.MonsterList.Clear();
                //Debug.Log("Player Exit!");
            }
            if (other.CompareTag("Monster"))
            {
                MonsterListInRoom.Remove(other.transform.parent.gameObject);
            }
        }
    }
}

