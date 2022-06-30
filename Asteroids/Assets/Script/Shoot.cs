using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float SecondBetweenShoot = 0.5f;
    public GameObject BulletPrefabs;
    public PlayerMovement PlayerMovementScript;
    public GameObject shootPosition;
    public Bullet BulletScript;

    private Queue<GameObject> BulletDelete;
    private float nextShootTime;
    private float topBound;
    private float bound;


    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementScript.Shoot += ShootBullet;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
         topBound = height / 2;
         bound = width / 2;
        BulletDelete = new Queue<GameObject>();
    }
     void Update()
    {
        if (BulletDelete.Count != 0)
        {
            GameObject firstBlock = BulletDelete.Peek();
            if (firstBlock.transform.position.x > bound || firstBlock.transform.position.x < -bound || firstBlock.transform.position.y > topBound || firstBlock.transform.position.y < -topBound)
            {
                BulletDelete.Dequeue();
                Destroy(firstBlock);

            }
        }
    }

    private void ShootBullet()
    {
        if (Time.time > nextShootTime)
        {
            nextShootTime = Time.time + SecondBetweenShoot;

            Vector2 directionBullet = shootPosition.transform.position - transform.position;
            GameObject bulletDelete = Instantiate(BulletPrefabs, shootPosition.transform.position, Quaternion.identity);

            Bullet BulletScript =bulletDelete.GetComponent<Bullet>();
            BulletScript.direction(directionBullet);

            BulletDelete.Enqueue(bulletDelete);

        }
    }
}
