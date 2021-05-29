using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Room
{
    public class StageMgr : MonoBehaviour
    {
        public static StageMgr Instance // singlton     
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<StageMgr>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("StageMgr");
                        instance = instanceContainer.AddComponent<StageMgr>();
                    }
                }
                return instance;
            }
        }
        private static StageMgr instance;

        public GameObject Player;

        public GameObject CloseDoor;
        public GameObject OpenDoor;

        [System.Serializable]
        public class StartPositionArray
        {
            public List<Transform> StartPosition = new List<Transform>();
        }

        public StartPositionArray[] startPositionArrays;    // 0 1 2
                                                            //startPositionArrays[0] 1~5 Stage
                                                            //startPositionArrays[1] 6~10 Stage
                                                            //Делаем 20 комнат и входим в начальную локацию каждой комнаты.

        public List<Transform> StartPositionAngel = new List<Transform>(); // 3 комнаты ангела
        // 3 комнаты ангела 
        public List<Transform> StartPositionBoss = new List<Transform>(); // 3 босса

        public Transform StartPositionLastBoss; // В последнюю минуту


        public int currentStage = 0;  //Текущеий Stage
        int LastStage = 20; // Последний Stage

        // Start is called before the first frame update
        void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        public void NextStage()
        {
            currentStage++;

            if (currentStage > LastStage)
            {
                UiController.Instance.EndGame();
                return;
            }

            if (currentStage % 5 != 0)  //Normal Stage
            {
                int arrayIndex = currentStage / 10;
                int randomIndex = Random.Range(0, startPositionArrays[arrayIndex].StartPosition.Count);
                Player.transform.position = startPositionArrays[arrayIndex].StartPosition[randomIndex].position;
                startPositionArrays[arrayIndex].StartPosition.RemoveAt(randomIndex);
            }
            else    //BossRoom or Angel
            {
                if (currentStage % 10 == 5)   // Angel
                {
                    int randomIndex = Random.Range(0, StartPositionAngel.Count);
                    Player.transform.position = StartPositionAngel[randomIndex].position;
                }
                else    //Boss
                {
                    if (currentStage == LastStage)  //LastBoss
                    {
                        Player.transform.position = StartPositionLastBoss.position;
                    }
                    else    //Mid Boss
                    {
                        int randomIndex = Random.Range(0, StartPositionBoss.Count);
                        Player.transform.position = StartPositionBoss[randomIndex].position;
                        StartPositionBoss.RemoveAt(currentStage / 10);
                    }
                    UiController.Instance.CheckBossRoom(true); 
                }
            }
            CameraBehavior.Instance.CarmeraNextRoom();
        }//NextStage
    }
}

