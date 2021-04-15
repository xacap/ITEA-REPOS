using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class CMainWeapon : MonoBehaviour, IWeapon
    {
        public EWeponType main;
        
        private float mTime;

        public void Shoot()
        {
            if (mTime + 0.5 <= Time.time)
            {
                var bulletPrefab = Resources.Load<GameObject>("Bullets/Bullet");
                Instantiate(bulletPrefab);
                bulletPrefab.transform.position = this.transform.position;
                mTime = Time.time;
            }
        }

        public EWeponType GetWeaponType(EWeponType eWeponTipe)
        {
            return main;
        }
    }
}



