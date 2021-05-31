using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Player
{
    public class SkillTest : MonoBehaviour
    {
        private List<int> PlayerSkillTest = new List<int>();
        
        void Awake()
        {
            PlayerSkillTest = PlayerData.Instance.PlayerSkill;
        }

        public void AddSkill(int numberSkill)
        {
            if (PlayerSkillTest != null && PlayerSkillTest.Count != 0)
            {
                if (PlayerSkillTest[numberSkill] == 0)
                {
                    PlayerSkillTest[numberSkill] = 1;
                }
                else 
                    PlayerSkillTest[numberSkill] = 0;
            }
        }


    }
}

