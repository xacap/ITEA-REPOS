using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Model;

namespace UI
{
    public class CWindowGameFinish : MonoBehaviour
    {
        public Text winTextFinish;

        void Start()
        {
            GameObject panelNode = gameObject.transform.Find("Panel").gameObject;
            GameObject obj = panelNode.transform.Find("ButtonPlay").gameObject;
            Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(() => Restart());
        }
        public void Restart()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1.0f;
        }
        public void Show(EPlayerState winnerBoxState)
        {
            switch (winnerBoxState)
            {
                case EPlayerState.ePlayerWinner:
                    winTextFinish.text = "Уровень пройден!";
                    break;
                case EPlayerState.ePlayerLost:
                    winTextFinish.text = "Миссия провалена!";
                    break;
            }
        }
    }
}

