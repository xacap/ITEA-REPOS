using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHpBar : MonoBehaviour
    {
        public static PlayerHpBar Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerHpBar>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("PlayerHpBar");
                        instance = instanceContainer.AddComponent<PlayerHpBar>();
                    }
                }
                return instance;
            }
        }
        private static PlayerHpBar instance;

        public Slider hpBar;
        public Transform player;
        public float maxHp;
        public float currentHp;

        public GameObject HpLineFolder;
        public Text playerHpText;

        float unitHp = 200f;

        void Start()
        {

        }

        void Update()
        {
            transform.position = player.position;
            hpBar.value = currentHp / maxHp;
            playerHpText.text = "" + currentHp;

        }

        public void GetHpBoost()
        {
            maxHp += 150;
            currentHp += 150;
            float scaleX = (1000f / unitHp) / (maxHp / unitHp);
            HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);

            foreach (Transform child in HpLineFolder.transform)
            {
                child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
            }

            HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
        }
    }
}

