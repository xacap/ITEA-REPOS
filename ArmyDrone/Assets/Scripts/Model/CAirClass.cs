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
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
            mMainWeapon.transform.position = slotTransform.position;
            mMainWeapon.Shoot();

            if (targetPosition != Vector3.zero)
            {
               
                animator.SetInteger("AnimationPar", 1);
            }
            
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

