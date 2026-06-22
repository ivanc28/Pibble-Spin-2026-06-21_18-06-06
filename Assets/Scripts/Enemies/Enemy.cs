using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = Player.Instance.transform.position - transform.position;
        rb.linearVelocity = moveDir.normalized * data.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.DamagePlayer(data.contactDamage);
        }
    }
}
