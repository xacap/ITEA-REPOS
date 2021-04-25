using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Libraries;


namespace Model
{
    public class CAirClass : MonoBehaviour
    {
        public enum EWeaponType
        {
            mainWeapon,
            plasmWeapon
        }

        [SerializeField] EWeaponType eWeaponType = EWeaponType.mainWeapon;
        CBaseBaheviorController moveController;
        
        public Vector3 targetPosition = Vector2.zero;
        public float smoothTime = 0.3f;
        private Vector3 velocity = Vector3.zero;

        List<IWeapon> weapons = new List<IWeapon>();

        [SerializeField] private CMainWeapon mMainWeapon;
        [SerializeField] private CPlasmWeapon mPlasmWeapon;

        public Transform slotTransform;
        public Transform slotPlasmTransform;

        Transform drone;
        private Animator animator;

        private void Awake()
        {
            mMainWeapon = Instantiate(mMainWeapon);
            mMainWeapon.transform.SetParent(this.transform, false);
            mPlasmWeapon = Instantiate(mPlasmWeapon);
            mPlasmWeapon.transform.SetParent(this.transform, false);
            AddWeapons();

            drone = this.gameObject.transform.GetChild(0);
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

            mMainWeapon.transform.position = slotTransform.position;
            mPlasmWeapon.transform.position = slotPlasmTransform.position;

            if (eWeaponType == EWeaponType.mainWeapon)
            {
                mMainWeapon.Shoot();
            }
            else if (eWeaponType == EWeaponType.plasmWeapon)
            {
                mPlasmWeapon.Shoot();
            }

            GetWeapon();
        }

        public void AddWeapons()
        {
            slotTransform = this.gameObject.transform.GetChild(1);
            slotPlasmTransform = this.gameObject.transform.GetChild(2);

            weapons.Add(mMainWeapon);
            weapons.Add(mPlasmWeapon);
        }

        public void GetWeapon()
        {
            if (Input.GetKey("1"))
            {
                eWeaponType = EWeaponType.mainWeapon;
            }
            else if (Input.GetKey("2"))
            {
                eWeaponType = EWeaponType.plasmWeapon;
            }
        }

        public void setBehaviorController(CBaseBaheviorController airObj)
        {
            moveController = airObj;
            moveController.setAirTarget(this);
        }
    }
}

