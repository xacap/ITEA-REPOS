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
    public class CPlayerController : CBaseBaheviorController, IInputObserver
    {
        [SerializeField] private int score = 0;
        [SerializeField] private int armor = 0;

        public Vector2 newPosstion = Vector2.zero;
        private CAirClass targetObj = GameObject.Find("Player").GetComponent<CAirClass>();

        public delegate void OnScoreChange(CPlayerController player, int scoreChange);
        public delegate void OnArmorChange(CPlayerController player, int armorChange);

        public event OnScoreChange onScoreChange;
        public event OnArmorChange onArmorChange;

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

        public bool OnInputBegin(Vector2 position)
        {
            position = Camera.main.ScreenToWorldPoint(position);
            targetObj.targetPosition = position;

            return true;
        }
        public void OnInputMove(Vector2 position)
        {
            position = Camera.main.ScreenToWorldPoint(position);
            targetObj.targetPosition = position;
        }
        public void OnInputEnd(Vector2 position)
        {
            position = Camera.main.ScreenToWorldPoint(position);
            targetObj.targetPosition = position;
        }
    }
}

