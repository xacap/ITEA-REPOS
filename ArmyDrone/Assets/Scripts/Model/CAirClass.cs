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

        List<IWeapon> weapons = new List<IWeapon>();

        [SerializeField] private CMainWeapon mMainWeapon;

        public Transform slotTransform;
        Transform drone;
       private Animator animator;

        private void Awake()
        {
            mMainWeapon = Instantiate(mMainWeapon);
            mMainWeapon.transform.SetParent(this.transform, false);
            AddWeapons();
            drone = this.gameObject.transform.GetChild(1);
            animator = drone.GetComponent<Animator>();

        }
        void Update()
        {
            var stay = this.transform.position;
            var inputUp = new Vector3(0, 0.5f, 0);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition + inputUp, ref velocity, smoothTime);
           
            if (stay != targetPosition)
            {
                animator.SetInteger("MovingParam", 1);
            }
            else
            {
                animator.SetInteger("MovingParam", 0);
            }


            Debug.Log("stay " + stay);
            Debug.Log("targetPosition " + targetPosition);

            mMainWeapon.transform.position = slotTransform.position;
            mMainWeapon.Shoot();
        }

        public void setBehaviorController(CBaseBaheviorController airObj)
        {
            moveController = airObj;
            moveController.setAirTarget(this);
        }


        public void AddWeapons()
        {
            slotTransform = this.gameObject.transform.GetChild(0);
            weapons.Add(mMainWeapon);
        }
    }
}

