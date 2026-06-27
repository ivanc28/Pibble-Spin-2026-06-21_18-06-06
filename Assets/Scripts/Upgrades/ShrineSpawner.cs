using UnityEngine;

public class ShrineSpawner : MonoBehaviour
{
    public int maxShrines;
    public Shrine shrinePrefab;
    public Transform bottomLeftShrineBound, topRightShrineBound;
    public float minSpawnDistanceFromOtherEntities;
    [Header("Shrine Cost")]
    public float baseShrineCost;
    //[Tooltip("How many seconds of game time must pass before increasing shrine cost")]
    //public float shrineCostIncreaseRate;
    [Tooltip("Coefficient of cost increase formula")]
    public float shrineCostIncreaseAmount;
    [Tooltip("Power of cost increase formula")]
    public float shrineCostIncreasePower;

    private Vector2 bottomLeftPos, topRightPos;
    public static ShrineSpawner Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        bottomLeftPos = bottomLeftShrineBound.position;
        topRightPos = topRightShrineBound.position;
    }
    private void Start()
    {
        for(int i = 0; i < maxShrines; i++)
        {
            SpawnShrine(true);
        }
    }

    public void SpawnShrine(bool useBaseCost = false)
    {
        Vector2 randomPos = GetRandomShrinePos();
        while(!EnoughDistanceFromEntities(randomPos, minSpawnDistanceFromOtherEntities))
        {
            randomPos = GetRandomShrinePos();
        }
        Shrine newShrine = Instantiate(shrinePrefab, randomPos, Quaternion.identity);
        newShrine.Initialize(GetShrineCost(Player.Instance.inventory.GetUpgradeCount()));
    }
    private Vector2 GetRandomShrinePos()
    {
        return new Vector2(Random.Range(bottomLeftPos.x, topRightPos.x), Random.Range(bottomLeftPos.y, topRightPos.y));
    }
    private bool EnoughDistanceFromEntities(Vector2 spawnPos, float minDistance)
    {
        foreach(Shrine shrine in FindObjectsByType<Shrine>(FindObjectsSortMode.None))
        {
            if(Vector2.Distance(shrine.transform.position, spawnPos) < minDistance)
            {
                return false;
            }
        }
        if(Vector2.Distance(Player.Instance.transform.position, spawnPos) < minDistance)
        {
            return false;
        }
        return true;
    }

    // some formula
    public int GetShrineCost(int upgradesClaimed)
    {
        return (int)(baseShrineCost + shrineCostIncreaseAmount * Mathf.Pow(upgradesClaimed, shrineCostIncreasePower));
    }
}
