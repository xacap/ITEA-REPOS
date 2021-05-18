﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyDuck : MonoBehaviour
{

    public GameObject player;

    public NavMeshAgent nvAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nvAgent.SetDestination(player.transform.position);
    }
}
