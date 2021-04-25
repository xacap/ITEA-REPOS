using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class CPlasmWeapon : MonoBehaviour, IWeapon
    {
        public EWeponType plasm;

        private float mTime;

        private GameObject bulletPlasmPrefab;

        public void Shoot()
        {
            if (mTime + 1 <= Time.time)
            {
                bulletPlasmPrefab = Resources.Load<GameObject>("Bullets/BulletPlasm");
                Instantiate(bulletPlasmPrefab);
                CAudioManager.Instance.PlaySFX(ESoundsFx.ShootLong);

                mTime = Time.time;
            }
            if (bulletPlasmPrefab != null)
            {
                bulletPlasmPrefab.transform.position = this.transform.position;
            }
        }

        public EWeponType GetWeaponType(EWeponType eWeponTipe)
        {
            return plasm;
        }
    }
}

