using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float EnemySpeed = 50f;
    public float RotateSpeed ;
    public float MinRotateSpeed = 10f;
    public float MaxRotateSpeed = 50f;

    private Vector2 randomDerection;

    private void Start()
    {
        RotateSpeed = Random.Range( MinRotateSpeed, MaxRotateSpeed);
    }
    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + randomDerection, EnemySpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 1), RotateSpeed * Time.deltaTime);

    }
    public void Randomdirection(Vector3 direction)
    {
        randomDerection = direction;

    }
}
