using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CEnemyTrigger : MonoBehaviour
    {
        private GameObject enemyPrefab;
        
        void OnBecameVisible()
        {
            InvokeRepeating("GetEnemy", 0.3f, 0.3f);
            Destroy(this.gameObject, 1.1f);
        }

        void GetEnemy()
        {
            enemyPrefab = Resources.Load<GameObject>("Enemys/EnemyDrone");
            Instantiate(enemyPrefab);
        }
    }
}

