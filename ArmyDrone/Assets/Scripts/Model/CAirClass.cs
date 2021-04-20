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
        //private Animator animator;

        private void Awake()
        {
            mMainWeapon = Instantiate(mMainWeapon);
            mMainWeapon.transform.SetParent(this.transform, false);
            AddWeapons();
            drone = this.gameObject.transform.GetChild(1);
            //animator = drone.GetComponent<Animator>();
        }
        void Update()
        {
            var stay = this.transform.position;

            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
            mMainWeapon.transform.position = slotTransform.position;
            mMainWeapon.Shoot();

           /*
            if (Input.GetKey("w"))
            {
                animator.SetInteger("AnimationPar", 2);
            }
            else
            {
                animator.SetInteger("AnimationPar", 0);
            }
           */
                      
           
            Debug.Log(stay);
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

