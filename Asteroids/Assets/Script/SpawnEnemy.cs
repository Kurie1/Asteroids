using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SecondBetweenSpawns;
    public EnemyMovement enemyMovementScript;
    public float enemytopBound = 10;
    public float enemybound = 10;
    public GameController GameControllerScript;
    public GameObject SmallEnemyPrefab;

    private Queue<GameObject> enemyDelete;
    private float topBound;
    private float bound;
    private float nextSpawnTime;
    private bool isGameLose = false;
    private GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        SecondBetweenSpawns= Random.Range(3f,6f );
        GameControllerScript.OnGameLose += OnGameLose;
        GameControllerScript.OnGameRestart += OnGameRestart;
       

        enemyDelete = new Queue<GameObject>();

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        topBound = height / 2;
        bound = width / 2;
        
    }


    // Update is called once per frame
    void Update()
    {
      
           
        if (enemyDelete.Count != 0)
        {
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

        if (isGameLose)
        {
            if (enemyDelete.Count != 0)
            {
                GameObject firstBlock = enemyDelete.Peek();
                enemyDelete.Dequeue();
                Destroy(firstBlock);
            }
            return;
        }

        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + SecondBetweenSpawns;
            Vector3 RandomPosition = new Vector2(UnityEngine.Random.Range(-bound, bound), UnityEngine.Random.Range(-topBound, topBound));
            Enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            EnemyMovement enemyMovementScript = Enemy.GetComponent<EnemyMovement>();
            enemyDelete.Enqueue(Enemy);
            enemyMovementScript.Randomdirection(getRandomDerection(transform.position, RandomPosition));

            enemyMovementScript.isLargeOne = true;
            enemyMovementScript.OnLargeDie += SpawnSmallerEnemy;
        }
    }

    private void SpawnSmallerEnemy(Vector3 spawnPosition)
    {
        for (int i = 0; i <= 1; i++) {
            Vector3 RandomPosition = new Vector2(UnityEngine.Random.Range(-bound, bound), UnityEngine.Random.Range(-topBound, topBound));
            GameObject SmallEnemy = Instantiate(SmallEnemyPrefab, spawnPosition, Quaternion.identity);
            EnemyMovement enemyMovementScript = SmallEnemy.GetComponent<EnemyMovement>();
            enemyDelete.Enqueue(SmallEnemy);
            enemyMovementScript.Randomdirection(getRandomDerection(transform.position, RandomPosition));
        } 
    }


    private Vector3 getRandomDerection(Vector3 SpawnPosition,Vector3 RandomPosition)
    {
        Vector3 randomDerection = RandomPosition - SpawnPosition;
        randomDerection.Normalize();
        return randomDerection;
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
