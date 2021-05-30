﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : MonoBehaviour
{
    public static EffectSet Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EffectSet>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerData");
                    instance = instanceContainer.AddComponent<EffectSet>();
                }
            }
            return instance;
        }
    }
    private static EffectSet instance;

    [Header("Monster")]
    public GameObject DuckAtkEffect;
    public GameObject DuckDmgEffect;
    public GameObject MonsterDmgText; 

    [Header("Player")]
    public GameObject PlayerAtkEffect;
    public GameObject PlayerDmgEffect;
}
