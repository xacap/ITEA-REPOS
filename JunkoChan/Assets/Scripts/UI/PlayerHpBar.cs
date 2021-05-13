﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHpBar : MonoBehaviour
    {
        public Transform player;
        public Slider hpBar;
        public float maxHp;
        public float currentHp;

        void Start()
        {

        }

        void Update()
        {
            transform.position = player.position;
            hpBar.value = currentHp / maxHp;

        }
    }
}

