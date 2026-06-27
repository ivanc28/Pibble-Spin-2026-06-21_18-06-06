using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyList;
    private GameObject enemy;
    [SerializeField] float secondsPerSpawn;
    private float spawnTimer;
    [SerializeField] int enemiesSpawnedPerBatch;
    [SerializeField] float secondsPerWave;
    private float waveTimer;
    [SerializeField] GameObject[] waveSpawners;
    private int difficulty;
    [SerializeField] int maxEnemyCount;
    private int currentEnemyCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = secondsPerSpawn;
        enemy = enemyList[0];
        difficulty = 1;
        currentEnemyCount = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (currentEnemyCount < maxEnemyCount)
        {
            if (spawnTimer >= secondsPerSpawn)
            {
                for (int i = 0; i < enemiesSpawnedPerBatch * difficulty; i++)
                {
                    Vector2 spawnPoint = new Vector2(Random.Range(-30.0f,30.0f), Random.Range(-30.0f,30.0f));
                    while (Vector2.Distance(spawnPoint, Player.Instance.transform.position) < 15)
                    {
                        spawnPoint = new Vector2(Random.Range(-30.0f,30.0f), Random.Range(-30.0f,30.0f));
                    }
                    Debug.Log(spawnPoint);
                    GameObject e = Instantiate(enemy, spawnPoint, Quaternion.identity, transform);
                    // Debug.Log(e.transform.position);
                    // e.transform.position = spawnPoint;
                }
                spawnTimer = 0;
            }
            else
            {
                spawnTimer += Time.fixedDeltaTime;
            }

            if (waveTimer >= secondsPerWave)
            {
                difficulty += 1;
                if (Random.Range(0,5) < 2)
                {
                    SpawnCircleWave();
                }
                else
                {
                    SpawnClusterWave();
                }
                waveTimer = 0;
            }
            else
            {
                waveTimer += Time.fixedDeltaTime;
            }
        }
    }

    private void SpawnCircleWave()
    {
        GameObject wave = waveSpawners[0];
        wave.transform.position = Player.Instance.transform.position;
        foreach (Transform child in wave.transform)
        {
            var spawnPoint = child.position;
            Instantiate(enemy, spawnPoint, Quaternion.identity, transform);
        }
    }
    private void SpawnClusterWave()
    {
        GameObject wave = waveSpawners[1];
        for (int i = 0; i < difficulty; i++)
        {
            wave.transform.position = new Vector2(Random.Range(-30.0f,30.0f), Random.Range(-30.0f,30.0f));
            while (Vector2.Distance(wave.transform.position, Player.Instance.transform.position) < 20)
            {
                wave.transform.position = new Vector2(Random.Range(-30.0f,30.0f), Random.Range(-30.0f,30.0f));
            }
            foreach (Transform child in wave.transform)
            {
                var spawnPoint = child.position;
                Instantiate(enemy, spawnPoint, Quaternion.identity, transform);
            }
        }
    }

    public void IncrementEnemyCount(int num)
    {
        currentEnemyCount += num;
    }
}
