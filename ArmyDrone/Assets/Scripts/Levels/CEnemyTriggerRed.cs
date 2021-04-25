using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CEnemyTriggerRed : MonoBehaviour
    {
        private GameObject enemyPrefabRed;

        void OnBecameVisible()
        {
            InvokeRepeating("GetEnemy", 0.1f, 1f);
            Destroy(this.gameObject, 0.5f);
        }

        void GetEnemy()
        {
            enemyPrefabRed = Resources.Load<GameObject>("Enemys/EnemyDroneRed");
            Instantiate(enemyPrefabRed);
        }
    }
}

