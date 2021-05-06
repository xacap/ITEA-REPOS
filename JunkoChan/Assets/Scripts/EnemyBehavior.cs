using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;

    public Transform patrolRoute;
    public List<Transform> location;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }
            private set
            {
            _lives = value;
            if (_lives <= 0)
                {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
                }
            }
    }



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform;

        InitializePatrolRoute();

        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void InitializePatrolRoute()
    {
        foreach (Transform childe in patrolRoute)
        {
            location.Add(childe);
        }

    }

    void MoveToNextPatrolLocation()
    {
        if (location.Count == 0)
            return;

        agent.destination = location[locationIndex].position;

        locationIndex = (locationIndex + 1) % location.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Enemy detected!");
        }
    }

    void OnTriggerExit(Collider other)
    { 
        if(other.name == "Player")
        {
            Debug.Log("Player upizdil");
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
        }
    }
    

   
}
