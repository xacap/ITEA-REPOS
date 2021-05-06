using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    private string _state;

    public string State
    {
        get { return _state;  }
        set { _state = value; }
    }

    public string lableText = "Collect all 4 item and win your freedom!";
    public int maxItems = 4;

    public bool showWinScreen = false;

    public bool showLossScreen = false;


    private int _itemsCollected = 0;

    public int Items
    {
        get { return _itemsCollected; }

        set
        {
            _itemsCollected = value;
            
            if (_itemsCollected >= maxItems)
            {
                lableText = "You've found all the items!";
                showWinScreen = true;

                Time.timeScale = 0f;
            }
            else
            {
                lableText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }

        }
    }
    private int _playerLives = 3;

    public int Lives
    {
        get { return _playerLives; }
        set
        {
            _playerLives = value;
            
            if(_playerLives <= 0)
            {
                lableText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                lableText = "Ouch... that's got hurt.";
            }
        }
    }

    void Start()
    {
        

        InventoryList<string> inventoryList = new
        InventoryList<string>();
    }

    

   /*
    
    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerLives);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), lableText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "YOU WIN!"))
            {
                Utilities.RestartLevel(0);
            }
           
        }

        if(showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "You lose..." ))
                {
                Utilities.RestartLevel();
            }
        }
    }
   */

}
