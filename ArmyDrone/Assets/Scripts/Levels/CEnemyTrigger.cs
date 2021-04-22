using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CEnemyTrigger : MonoBehaviour
    {
        private float mTime;

        private GameObject enemyPrefab;
        

        void OnBecameVisible()
        {
            EnemyGen();
        }

        void EnemyGen()
        {
            enemyPrefab = Resources.Load<GameObject>("Enemys/EnemyDrone");
            Instantiate(enemyPrefab);

            var offsetPos = new Vector3(0,0,0);
            enemyPrefab.transform.position = transform.parent.position;
            enemyPrefab.transform.position = this.transform.position + offsetPos;
        }
    }
}

