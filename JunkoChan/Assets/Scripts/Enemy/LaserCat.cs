using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemy
{
    public class LaserCat : MonoBehaviour
    {
        public GameObject HitEffect;
        public GameObject Laser; 

        LineRenderer lr;

        new protected void Start()
        {
            lr = Laser.GetComponent<LineRenderer>();
            lr.enabled = false;
            HitEffect.SetActive(false);
        }

        void Update()
        {
            if (lr.enabled)
            {
                Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);

                if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("Wall"))
                {
                    HitEffect.SetActive(true);
                    HitEffect.transform.position = hit.point;
                    if (hit.transform.CompareTag("Player"))
                    {
                       //PlayerHpBar.Instance.currentHp -= Time.deltaTime * 250f;// LaserDMG;
                    }
                }
                else
                {
                    HitEffect.SetActive(false);
                }
            }
        }
    }
}

