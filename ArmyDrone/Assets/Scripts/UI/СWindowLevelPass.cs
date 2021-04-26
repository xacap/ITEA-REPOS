using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Model;

namespace UI
{
    public class СWindowLevelPass : MonoBehaviour
    {
        public Text winTextLP;

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
                    winTextLP.text = "Уровень пройден!";
                    break;
                case EPlayerState.ePlayerLost:
                    winTextLP.text = "Миссия провалена!";
                    break;
            }
        }
    }
}


