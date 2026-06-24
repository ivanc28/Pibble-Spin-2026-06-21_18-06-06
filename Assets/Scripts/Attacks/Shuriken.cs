using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public void Init()
    {
        // myCollider = GetComponent<CircleCollider2D>();
        // myCollider.radius = 0.5f;
        // myCollider.enabled = true;
        // modifiedDamage = damage;
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShootAt(Vector3 target)
    {
        transform.position = Player.Instance.transform.position;
        Vector3 direction = (target - transform.position).normalized;
        rb.linearVelocity = direction * 5;
        Debug.Log("direction");
        Debug.Log(direction);
    }
}
