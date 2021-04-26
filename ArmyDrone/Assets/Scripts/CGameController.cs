using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Libraries;
using Libraries.ActionSystem;
using Model;
using UI;

public enum EGameState
{
    start,
    inGame,
    onPause,
    levelPass,
    gameOver
}

public class CGameController : MonoBehaviour
{
    public static CGameController Instance;
    //public static CGameController Instance { get; private set; }


    public CInput input;
    private GameObject playerAirplane;
    public CPlayerController controller;

    public EGameState currentGameState;

    public Canvas startCanvas;
    public Canvas inGameCanvas;
    public Canvas onPauseCanvas;
    public Canvas levelPassCanvas;
    public Canvas gameOverCanvas;

    public int herat = 100;

    private CEvents mNotificationManager = new CEvents();
    private CUIController _UIController = new CUIController();


    public CEvents GetNotificationManager()
    {
        return mNotificationManager;
    }

    public static CInput Input
    {
        get { return Instance.input; }
    }


    public void Awake()
    {
        if (Instance == null) { Instance = this; }

        currentGameState = EGameState.start;
        mNotificationManager.Register(EEventType.eRestartGameEvent, IsGameFinished);

        input = new CInput();
        playerAirplane = GameObject.Find("Player");
        controller = new CPlayerController();
        playerAirplane.GetComponent<CAirClass>().setBehaviorController(controller);
        input.RegisterObserver(controller, 1);
    }

    void Start()
    {
        currentGameState = EGameState.start;
        CInventoryList<string> inventoryList = new CInventoryList<string>();
    }

    public void Update()
    {
        input.Check();

        if (herat >= 100)
        {
            herat = 100;
        }

    }

    public void StartGame()
    {
        SetGameState(EGameState.inGame);
    }

    public void LevelPassed()
    {
        Time.timeScale = 0f;
        SetGameState(EGameState.levelPass);
        _UIController.ShowLevelPassWindow(EPlayerState.ePlayerWinner);

    }

    public void IsGameFinished()
    {
        Time.timeScale = 0f;
        SetGameState(EGameState.gameOver);
        _UIController.ShowGameFinishWindow(EPlayerState.ePlayerLost);

    }

    void SetGameState(EGameState newGameState)
    {
        if (newGameState == EGameState.start)
        {
            startCanvas.enabled = true;
            inGameCanvas.enabled = false;
            onPauseCanvas.enabled = false;
            levelPassCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }
        else if (newGameState == EGameState.inGame)
        {
            startCanvas.enabled = false;
            inGameCanvas.enabled = true;
            onPauseCanvas.enabled = false;
            levelPassCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }
        else if (newGameState == EGameState.onPause)
        {
            startCanvas.enabled = false;
            inGameCanvas.enabled = false;
            onPauseCanvas.enabled = true;
            levelPassCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }
        else if (newGameState == EGameState.levelPass)
        {
            startCanvas.enabled = false;
            inGameCanvas.enabled = false;
            onPauseCanvas.enabled = false;
            levelPassCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }
        else if (newGameState == EGameState.gameOver)
        {
            startCanvas.enabled = false;
            inGameCanvas.enabled = false;
            onPauseCanvas.enabled = false;
            levelPassCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }

        currentGameState = newGameState;
    }

    public int Hearts
    {
        get { return herat; }
        set { herat = value; }
    }
}
