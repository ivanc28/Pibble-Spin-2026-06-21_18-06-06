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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = 0;
        enemy = enemyList[0];
        difficulty = 5;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (spawnTimer >= secondsPerSpawn)
        {
            for (int i = 0; i < enemiesSpawnedPerBatch; i++)
            {
                Vector2 spawnPoint = new Vector2(Random.Range(-30.0f,30.0f), Random.Range(-30.0f,30.0f));
                while (Vector2.Distance(spawnPoint, Player.Instance.transform.position) < 15)
                {
                    spawnPoint = new Vector2(Random.Range(-30.0f,30.0f), Random.Range(-30.0f,30.0f));
                }
                Instantiate(enemy, spawnPoint, Quaternion.identity);
                spawnTimer = 0;
            }
        }
        else
        {
            spawnTimer += Time.fixedDeltaTime;
        }

        if (waveTimer >= secondsPerWave)
        {
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

    private void SpawnCircleWave()
    {
        GameObject wave = waveSpawners[0];
        wave.transform.position = Player.Instance.transform.position;
        foreach (Transform child in wave.transform)
        {
            var spawnPoint = child.position;
            Instantiate(enemy, spawnPoint, Quaternion.identity);
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
                Instantiate(enemy, spawnPoint, Quaternion.identity);
            }
        }
    }
}
