using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class CAirEnemy : MonoBehaviour
    {
        [SerializeField] private CEnemyWeapon mEnemyWeapon;

        public Transform slotTransform;

        Transform drone;
        private Animator animator;

        private void Awake()
        {
            mEnemyWeapon = Instantiate(mEnemyWeapon);
            mEnemyWeapon.transform.SetParent(this.transform, false);
           
            AddWeapons();

            drone = this.gameObject.transform.GetChild(1);
            animator = drone.GetComponent<Animator>();

        }
        void Update()
        {
            mEnemyWeapon.transform.position = slotTransform.position;

            mEnemyWeapon.Shoot();
        }

        public void AddWeapons()
        {
            slotTransform = this.gameObject.transform.GetChild(0);

        }

    }
}



