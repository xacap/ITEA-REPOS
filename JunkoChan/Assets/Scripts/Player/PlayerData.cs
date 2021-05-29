using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData Instance     
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerData>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("PlayerData");
                        instance = instanceContainer.AddComponent<PlayerData>();
                    }
                }
                return instance;
            }
        }
        private static PlayerData instance;

        public float dmg = 250;
        public GameObject Player;
        public GameObject[] PlayerBolt;
        public GameObject ItemExp;
        public float PlayerCurrentExp = 1f;
        public float PlayerLvUpExp = 100f;
        public int PlayerLv = 1;
        public bool playerDead = false;

        public float maxHp = 1000f;
        public float currentHp = 1000f;

        public List<int> PlayerSkill = new List<int>();

        private void Update()
        {
            if (PlayerHpBar.Instance.currentHp <=0)
            {
                currentHp = 0;
                playerDead = true;
                PlayerMovement.Instance.Anim.SetTrigger("Dead");
                //UiController.Instance.EndGame();
                return;
            }
        }
        public void PlayerExpCalc (float exp)
        {
            PlayerCurrentExp += exp;
            if (PlayerCurrentExp >= PlayerLvUpExp)
            {
                PlayerLv++;
                PlayerCurrentExp -= PlayerLvUpExp;
                PlayerLvUpExp *= 1.3f;
                PlayerLevelUp();
            }

        }

        void PlayerLevelUp()
        {
            UiController.Instance.PlayerLvUp(true);
        }
    }
}

