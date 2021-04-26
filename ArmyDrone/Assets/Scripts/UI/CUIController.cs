using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

namespace UI
{
    public class CUIController : MonoBehaviour
    {
        public void ShowGameFinishWindow(EPlayerState playerState)
        {
            GameObject _go = GameObject.Find("GameOverCanvas");

            _go.GetComponent<CWindowGameFinish>().Show(playerState);

        }
        public void ShowLevelPassWindow(EPlayerState playerState)
        {
            GameObject _go = GameObject.Find("LevelPassCanvas");

            _go.GetComponent<СWindowLevelPass>().Show(playerState);
        }
    }
}


