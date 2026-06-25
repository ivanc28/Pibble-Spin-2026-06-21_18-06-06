using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float secondsPerSpawn;
    private float spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (spawnTimer >= secondsPerSpawn)
        {
            Vector2 spawnPoint = new Vector2(Random.Range(-30.0f,30.0f), Random.Range(-30.0f,30.0f));
            Instantiate(enemy, spawnPoint, Quaternion.identity);
            spawnTimer = 0;
        }
        else
        {
            spawnTimer += Time.fixedDeltaTime;
        }
    }
}
