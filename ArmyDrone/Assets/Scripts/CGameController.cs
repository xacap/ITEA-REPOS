﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Libraries;
using Libraries.ActionSystem;
using Model;

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
    public CInput input;
    private GameObject playerAirplane;
    public CPlayerController controller;

    public EGameState currentGameState;
    private int itemsCollected = 0;
  
    [SerializeField] Canvas startCanvas;
    [SerializeField] Canvas inGameCanvas;
    [SerializeField] Canvas onPauseCanvas;
    [SerializeField] Canvas levelPassCanvas;
    [SerializeField] Canvas gameOverCanvas;

    private CEvents mNotificationManager = new CEvents();

    public CEvents GetNotificationManager()
    {
        return mNotificationManager;
    }

    public static CInput Input
    {
        get { return Instance.input; }
    }

    public static CGameController Instance { get; private set; }

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
        CInventoryList<string> inventoryList = new CInventoryList<string>();
    }

    public void Update()
    {
        input.Check();

    }

    public void StartGame()
    {
        SetGameState(EGameState.inGame);
    }

    public void LevelPassed()
    {
        Time.timeScale = 0f;
        SetGameState(EGameState.levelPass);
    }
   
    public void IsGameFinished()
    {
        Time.timeScale = 0f;
        SetGameState(EGameState.gameOver);
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

    public int Items
    {
        get { return itemsCollected; }
        set
        {
            itemsCollected = value;
        }
    }

   
}
