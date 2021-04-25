﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model

{
    public class CEnemyWeapon : MonoBehaviour, IWeapon
    {
        public EWeponType main;

        private float mTime;

        private GameObject bulletPrefab;

        public void Shoot()
        {
            if (mTime + 0.46 <= Time.time)
            {
                bulletPrefab = Resources.Load<GameObject>("Bullets/BulletRed");
                Instantiate(bulletPrefab);
                CAudioManager.Instance.PlaySFX(ESoundsFx.ShootShort);

                mTime = Time.time;
            }

            if (bulletPrefab != null)
            {
                bulletPrefab.transform.position = this.transform.position;
            }
        }

        public EWeponType GetWeaponType(EWeponType eWeponTipe)
        {
            return main;
        }
    }
}

