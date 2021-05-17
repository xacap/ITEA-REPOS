﻿using System.Collections;
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
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInThisRoom = true;
                PlayerTargeting.Instance.MonsterList = new List<GameObject>(MonsterListInRoom);
                Debug.Log("Открыта новая комната! Колличество врагов : " + PlayerTargeting.Instance.MonsterList.Count);

            }
            if (other.CompareTag("Monster"))
            {
                MonsterListInRoom.Add(other.gameObject);
                //MonsterListInRoom.Add(other.transform.root.gameObject);

            }
        }

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
    }
}

