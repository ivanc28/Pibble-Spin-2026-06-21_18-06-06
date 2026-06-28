using UnityEngine;

public class Currency : MonoBehaviour
{
    public int currencyAmt;
    public float moveSpeed;
    public Rigidbody2D rb;
    public float minSpawnSpeed;
    public float maxSpawnSpeed;
    public float collectDelay;
    public AudioClip[] collectClips;
    private float collectDelayTimer;
    private void Update()
    {
        if(collectDelayTimer < collectDelay)
        {
            collectDelayTimer += Time.deltaTime;
            return;
        }
        if (Player.Instance.ItemInRange(transform))
        {
            Vector2 dir = Player.Instance.transform.position - transform.position;
            rb.linearVelocity = dir.normalized * moveSpeed;
        }
    }
    private void Collect()
    {
        Player.Instance.inventory.IncreaseCurrency(currencyAmt);
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(collectClips, 0.4f, true);
        }
    }
    public void SpawnAtRandomSpeed()
    {
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward);
        Vector2 randomDir = rotation * Vector2.up;
        rb.linearVelocity = randomDir.normalized * Random.Range(minSpawnSpeed, maxSpawnSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
