using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerState
{
    stay,
    run,
    winner,
    dead
}

public class CPlayerBehavior : MonoBehaviour
{
    //[SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private int score = 0;
    [SerializeField] private int armor = 0;

    public delegate void OnScoreChange(CPlayerBehavior player, int scoreChange);
    public delegate void OnArmorChange(CPlayerBehavior player, int armorChange);
    
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



}
