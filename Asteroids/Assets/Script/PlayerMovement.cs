using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed =10f;
    public float RotateSpeed = 10f;
    public Action Shoot;
    public Action Rotate;
    public Action Die;
    public GameController GameControllerScript;

    private Vector2 playerPosition;
    private float topBound;
    private float bound;
    private bool isGameLose = false;

    // Start is called before the first frame update
    void Start()
    {
        GameControllerScript.OnGameLose += OnGameLose;
        GameControllerScript.OnGameRestart += OnGameRestart;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        topBound = height / 2;
        bound = width / 2;
        playerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameLose)
        {
            transform.position = playerPosition;
            return;
        }
           

        float horizontalInput = Input.GetAxisRaw("Horizontal"); //-1 0  1
        float verticalInput = Input.GetAxisRaw("Vertical");// -1  0 1
        if (horizontalInput != 0)
        {
            transform.Rotate(new Vector3(0, 0, 1), horizontalInput * RotateSpeed * Time.deltaTime);
            
        }
        if (verticalInput != 0)
           
        {
            transform.position = transform.position + transform.up * verticalInput * MoveSpeed * Time.deltaTime;
            
        }
        if(transform.position.y > topBound)
        {
            transform.position = new Vector2(transform.position.x, -topBound);
        }
        if (transform.position.y < -topBound)
        {
            transform.position = new Vector2(transform.position.x, topBound);
        }
        if (transform.position.x > bound)
        {
            transform.position = new Vector2(-bound,transform.position.y);
        }
            
        if(transform.position.x < -bound) 
        {
            transform.position = new Vector2(bound, transform.position.y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            
            Shoot?.Invoke();  
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            Die?.Invoke();
        }
    }
    private void OnGameRestart()
    {
        isGameLose = false;
    }

    private void OnGameLose()
    {
        isGameLose = true;
    }
}
