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
            if (mTime + 0.3 <= Time.time)
            {
                enemyPrefab = Resources.Load<GameObject>("Enemys/EnemyDrone");
                Instantiate(enemyPrefab);

                mTime = Time.time;
            }

            var offsetPos = new Vector3(5,0,0);
            enemyPrefab.transform.position = this.transform.position + offsetPos;
        }
    }
}

