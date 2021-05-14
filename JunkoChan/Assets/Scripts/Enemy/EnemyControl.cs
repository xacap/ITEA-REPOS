using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyControl : MonoBehaviour
    {
        public GameObject enemyCanvasGo;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Potato"))
            {
                enemyCanvasGo.GetComponent<EnemyHpBar>().Dmg();
            }
        }
    }
}


