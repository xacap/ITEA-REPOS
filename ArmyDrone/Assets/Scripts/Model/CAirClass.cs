using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Libraries;


namespace Model
{
    public class CAirClass : MonoBehaviour
    {
        CBaseBaheviorController moveController;

        public Vector2 targetPosition = Vector2.zero;

        [SerializeField] private float moveSpeed = 2.0f;
        float lerp = 0, duration = 20f;



        private void FixedUpdate()
        {
            //if (targetPosition.x == Vector2(0f,0f))

            var moveTransform = this.transform.position + this.transform.up * moveSpeed * Time.fixedDeltaTime;
            this.transform.position = moveTransform;

            var startPos = this.transform.position;
                        
            lerp += Time.deltaTime / duration;
            this.transform.position = Vector2.Lerp(startPos, targetPosition, lerp);
        }
        public void setBehaviorController(CBaseBaheviorController airObj) 
        {
            moveController = airObj;
            moveController.setAirTarget(this);
        }
    }
}

