using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class CEnemyDroneBehavior : MonoBehaviour
    {
        [SerializeField] private float speedUp = 1f;
        private Vector3 velocity = Vector3.zero;
        [SerializeField] Vector3 targetPosition = new Vector3(-5, -5, 0);

        void Update()
        {


            Vector3 forwardUp = this.transform.position + targetPosition * speedUp * Time.fixedDeltaTime;
            this.transform.position = forwardUp;
        }
    }
}

