using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerMovement PlayerMovementScript;
    public Action OnGameLose;
    public Action OnGameRestart;
    private bool isGameLose = false;
    void Start()
    {
        PlayerMovementScript.Die += Die;
    }

    private void Die()
    {
        OnGameLose?.Invoke();
        isGameLose = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)&& isGameLose==true)
        {
            OnGameRestart?.Invoke();
            isGameLose = false;
        }
    }
}
