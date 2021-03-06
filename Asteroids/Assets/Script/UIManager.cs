using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameController GameControllerScript;
    public TMP_Text score;

    public Shoot ShootScript;

    private float scoreCounter = 0;
    

    void Start()
    {
        GameControllerScript.OnGameLose += OnGameLose;
        GameControllerScript.OnGameRestart += OnGameRestart;
        ShootScript.OnBulletHitObject += Hit;
        
        scoreCounter = 0;

    }


    private void Update()
    {

    }

    private void Hit()
    {
       
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
            score.text = scoreCounter.ToString();
            
        }
        else
        {
            canvasGroup.alpha = 0;
            scoreCounter = 0;
            
        }
    }
   
}
