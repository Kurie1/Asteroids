using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SecondBetweenSpawns = 0.5f;
    public EnemyMovement enemyMovementScript;
    public float enemytopBound = 10;
    public float enemybound = 10;
    public Bullet BulletScript;
    public GameController GameControllerScript;

    private Queue<GameObject> enemyDelete;
    private float topBound;
    private float bound;
    private float nextSpawnTime;
    private bool isGameLose = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameControllerScript.OnGameLose += OnGameLose;
        GameControllerScript.OnGameRestart += OnGameRestart;

        enemyDelete = new Queue<GameObject>();

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        topBound = height / 2;
        bound = width / 2;
        BulletScript.Hit += Hit;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDelete.Count != 0)
        {
            if (isGameLose)
                return;

            GameObject firstBlock = enemyDelete.Peek();
            if (firstBlock != null)
            {
                if (firstBlock.transform.position.x > enemybound
                    || firstBlock.transform.position.x < -enemybound
                    || firstBlock.transform.position.y > enemytopBound
                    || firstBlock.transform.position.y < -enemytopBound)
                {
                    enemyDelete.Dequeue();
                    Destroy(firstBlock);

                }
            }
        }
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + SecondBetweenSpawns;
            Vector3 RandomPosition = new Vector2(UnityEngine.Random.Range(-bound, bound), UnityEngine.Random.Range(-topBound, topBound));
            GameObject Enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            EnemyMovement enemyMovementScript = Enemy.GetComponent<EnemyMovement>();
            enemyDelete.Enqueue(Enemy);
            enemyMovementScript.Randomdirection(getRandomDerection(transform.position, RandomPosition));

        }
    }
    private Vector3 getRandomDerection(Vector3 SpawnPosition,Vector3 RandomPosition)
    {
        Vector3 randomDerection = RandomPosition - SpawnPosition;
        randomDerection.Normalize();
        return randomDerection;
    }
    private void Hit()
    {
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
