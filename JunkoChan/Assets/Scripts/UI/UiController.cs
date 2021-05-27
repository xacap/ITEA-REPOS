using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Player;
using Room;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        public static UiController Instance   
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UiController>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("UiController");
                        instance = instanceContainer.AddComponent<UiController>();
                    }
                }
                return instance;
            }
        }
        private static UiController instance;

        public GameObject JoyStickGO;
        public GameObject JoyStickPanelGO;
        public GameObject SlotMachineGO;
        public GameObject RouletteGO;
        public GameObject EndGameGO;

        public Text ClearRoomCnt;

        public Slider PlayerExpBar;

        public Text playerLvText;
        void Start()
        {
            PlayerExpBar.gameObject.SetActive(true);
        }

        

        public void PlayerLvUp(bool isSlotMachineOn)
        {
            if (isSlotMachineOn)
            {
                JoyStickGO.SetActive(false);
                JoyStickPanelGO.SetActive(false);
                SlotMachineGO.SetActive(true);
            }
            else
            {
                JoyStickGO.SetActive(true);
                JoyStickPanelGO.SetActive(true);
                SlotMachineGO.SetActive(false);
            }
        }

        public void EndGame()
        {
            JoyStickGO.gameObject.SetActive(false);
            JoyStickPanelGO.gameObject.SetActive(false);
            StartCoroutine(EndGamePopUp());
        }

        IEnumerator EndGamePopUp()
        {
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(1f);
            EndGameGO.SetActive(true);
            ClearRoomCnt.text = "Clear " + (StageMgr.Instance.currentStage - 1);
            yield return new WaitForSecondsRealtime(2f);
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}
