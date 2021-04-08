using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Libraries;

namespace Model
{
    public enum EPlayerState
    {
        stay,
        run,
        winner,
        dead
    }

    public class CPlayer : CAirModel
    {
        [SerializeField] private float moveSpeed = 2.0f;
        [SerializeField] private int score = 0;
        [SerializeField] private int armor = 0;

        public delegate void OnScoreChange(CPlayer player, int scoreChange);
        public delegate void OnArmorChange(CPlayer player, int armorChange);

        public event OnScoreChange onScoreChange;
        public event OnArmorChange onArmorChange;

        private void FixedUpdate()
        {
            Vector3 forwardRun = this.transform.position + this.transform.up * moveSpeed * Time.fixedDeltaTime;
            this.transform.position = forwardRun;
        }

        public void ChangeArmor(int armorChange)
        {
            armor += armorChange;
            onArmorChange.Invoke(this, armorChange);
        }

        public void AddScore(int scoreChange)
        {
            score += scoreChange;

            if (onScoreChange != null)
            {
                onScoreChange.Invoke(this, scoreChange);
            }
        }

        
    }
}


