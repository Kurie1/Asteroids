using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameController GameControllerScript;
    public TMP_Text scoreText;
    
    public Bullet BulletScript;

    private float scoreCounter = 0;
    private bool isLoseGame = false;

    void Start()
    {
        GameControllerScript.OnGameLose += OnGameLose;
        GameControllerScript.OnGameRestart += OnGameRestart;
        BulletScript.Hit += Score;
        
        scoreCounter = 0;

    }


    private void Update()
    {

    }

    private void Score()
    {
        if (!isLoseGame)
            scoreCounter++;
    }

    private void OnGameRestart()
    {
        GameLose(false);
    }

    private void OnGameLose()
    {
        GameLose(true);
    }

    private void GameLose(bool isGameLose)
    {
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        if (isGameLose)
        {
            canvasGroup.alpha = 1;
            scoreText.text = scoreCounter.ToString();
            isLoseGame = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            scoreCounter = 0;
            isLoseGame = false;
        }
    }
}
