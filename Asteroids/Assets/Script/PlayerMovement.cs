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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //-1 0  1
        float verticalInput = Input.GetAxisRaw("Vertical");// -1  0 1
        if (horizontalInput != 0)
        {
            transform.Rotate(new Vector3(0, 0, 1), horizontalInput * RotateSpeed * Time.deltaTime);
            Rotate?.Invoke();
        }
        if (verticalInput != 0)
        {
            transform.position = transform.position + transform.up * verticalInput * MoveSpeed * Time.deltaTime;
            Rotate?.Invoke();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            
            Shoot?.Invoke();  
        }
    }
}
