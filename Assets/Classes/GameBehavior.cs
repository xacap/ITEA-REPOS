using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;


    public void Awake()
    {
        GameObject robot = GameObject.Instantiate(player) as GameObject;
        robot.transform.position = Vector3.zero;
    }
    

}
