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
        [SerializeField] private CMainWeapon mWeapon;

        public Transform slotTransform;


        private void Awake()
        {
            mMainWeapon = Instantiate(mMainWeapon);
            AddWeapons();
        }
        void Update()
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
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

