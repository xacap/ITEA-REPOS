using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Libraries;


namespace Model
{
    public class CAirClass : MonoBehaviour
    {
        CBaseBaheviorController moveController;
        
        public Vector3 targetPosition = Vector2.zero;
        public float smoothTime = 0.3f;
        private Vector3 velocity = Vector3.zero;

        void Update()
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
        }

        public void setBehaviorController(CBaseBaheviorController airObj) 
        {
            moveController = airObj;
            moveController.setAirTarget(this);
        }
    }
}

