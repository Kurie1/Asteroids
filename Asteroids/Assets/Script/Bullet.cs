using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 50f;
    public Action Hit;

    private Vector2 bulletdirection;


    // Update is called once per frame
    void Update()
    {
       
        transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + bulletdirection, BulletSpeed * Time.deltaTime);


    }
    public void direction(Vector2 direction)
    {
        bulletdirection = direction;
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            
            Hit?.Invoke();
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            
        }
        
    }


}