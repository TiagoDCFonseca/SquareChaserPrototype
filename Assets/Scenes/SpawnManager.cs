using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    private MainManager mainManager;

    // Variables Containing Spawn Objects Prefabs
    public GameObject coinPrefab;
    public GameObject obstaclePrefab;
    public GameObject shieldPrefab;
    public GameObject[] enemyPrefab;

    //Variables For GenerateSpanPos Method
    [SerializeField] private float spawnRangeX = 27;
    [SerializeField] private float spawnMaxY = 37;
    [SerializeField] private float spawnMinY = 0;

    //Variables for CoinGenerator
    private int coinsGenerated = 0;
    private int maxCoins = 500;

    //Variables for ObstacleGenerator
    private int obstaclesGenerated = 0;
    private int maxObstacles = 300;

    //Variables for Shield Generator
    private int shieldsGenerated = 0;
    private int maxShield = 200;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetSpawnState();
    }

    // Update is called once per frame
    void Update()
    {
       if(GameManager.Instance.IsGameOver() || MainManager.Instance.GameOver())
        {
            StopAllCoroutines();
        }
    }

    public void ResetSpawnState()
    {
        Debug.Log("Reset Spawn method called");
        coinsGenerated = 0;
        obstaclesGenerated = 0;
        shieldsGenerated = 0;
        // Restart spawning coroutines
        StartCoroutine(CoinGenerator());
        StartCoroutine(ObstacleGenerator());
        StartCoroutine(ShieldGenerator());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnFastEnemy());
    }

    IEnumerator CoinGenerator()
    {
        Debug.Log("Coin Generator method called");
        while (coinsGenerated < maxCoins && !MainManager.Instance.GameOver())
        {
            Instantiate(coinPrefab, GenerateSpawnPos(), transform.rotation);
            coinsGenerated++;
            yield return new WaitForSeconds(8);
        }
    }

    IEnumerator ObstacleGenerator()
    {
        Debug.Log("obstacle Generator method called");
        while (obstaclesGenerated < maxObstacles && !MainManager.Instance.GameOver())
        {
            Instantiate(obstaclePrefab, GenerateSpawnPos(), transform.rotation);
            obstaclesGenerated++;
            yield return new WaitForSeconds(7);
        }

    }
    IEnumerator ShieldGenerator()
    {
        Debug.Log("Shield Generator method called");
        while (shieldsGenerated < maxShield && !MainManager.Instance.GameOver())
        {
            Instantiate(shieldPrefab, GenerateSpawnPos(), transform.rotation);
            shieldsGenerated++;
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator SpawnEnemy()
    {
        Debug.Log("Spawn enemy Generator method called");
        while (true)
        {
            int newYpos = 48;
            Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), newYpos);
            Instantiate(enemyPrefab[0], spawnPos, enemyPrefab[0].transform.rotation);
            yield return new WaitForSeconds(20);
        }
       
    }
    IEnumerator SpawnFastEnemy()
    {
        Debug.Log("Spawn Fast method called");
        while (true)
        {
            int newYpos = 48;
            Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), newYpos);
            Instantiate(enemyPrefab[1], spawnPos, enemyPrefab[1].transform.rotation);
            yield return new WaitForSeconds(10);
        }
        
    }

    private Vector2 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosY = Random.Range(spawnMinY, spawnMaxY);
        Vector2 randomPos = new Vector2(spawnPosX, spawnPosY);
        return randomPos;
    }

}
