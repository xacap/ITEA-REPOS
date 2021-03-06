using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Enemy
{
    public class EnemyHpBar : MonoBehaviour
    {
        public Slider hpSlider;
        public Slider BackHpSlider;
        public bool backHpHit = false;
        public Transform enemy;
        public float maxHp = 1000f;
        public float currentHp = 1000f;

        
        void Update()
        {
            transform.position = enemy.position;
            hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f);

            if (backHpHit)
            {
                BackHpSlider.value = Mathf.Lerp(BackHpSlider.value, hpSlider.value, Time.deltaTime * 10f);
                if (BackHpSlider.value >= BackHpSlider.value - 0.01f)
                {
                    backHpHit = false;
                    BackHpSlider.value = hpSlider.value;
                }
            }
        }

        public void Dmg()
        {
            currentHp -= 250f;
            Invoke("BackHpFun", 0.5f);
        }

        void BackHpFun()
        {
            backHpHit = true;
        }
    }
}


