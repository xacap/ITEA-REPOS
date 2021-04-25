using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CVievGames : MonoBehaviour
{
    public Text scoreLabel;


    void Update()
    {
        scoreLabel.text = CGameController.Instance.Hearts.ToString("f0");
    }
}
