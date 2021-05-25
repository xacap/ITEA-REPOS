﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData Instance     
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerData>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("PlayerData");
                        instance = instanceContainer.AddComponent<PlayerData>();
                    }
                }
                return instance;
            }
        }
        private static PlayerData instance;

        public float dmg = 250;
        public GameObject Player;
        public GameObject[] PlayerBolt;
        public GameObject ItemExp;

        public List<int> PlayerSkill = new List<int>();
    }
}

