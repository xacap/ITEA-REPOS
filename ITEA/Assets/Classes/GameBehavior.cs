using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public GameObject player;


    void Start()
    {
        GameObject robot = GameObject.Instantiate(player) as GameObject;
        robot.transform.position = Vector3.zero;
    }

}
