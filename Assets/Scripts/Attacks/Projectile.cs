using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float damage;
    public Vector3 startPosition;
    public float range;
    public bool piercing;
    public float projectileSpeed;
    
    public virtual void Init(float modifiedDamage)
    {
        // myCollider = GetComponent<CircleCollider2D>();
        // myCollider.radius = 0.5f;
        // myCollider.enabled = true;
        // modifiedDamage = damage;
        rb = GetComponent<Rigidbody2D>();
        damage = modifiedDamage;
        startPosition = Player.Instance.transform.position;
        transform.position = startPosition;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > range)
        {
            EndOfLifespanBehavior();
        }
    }
    public virtual void ShootAt(Vector3 target)
    {
        Vector3 direction = (target - startPosition).normalized;
        rb.linearVelocity = direction * projectileSpeed;
        // Debug.Log("direction");
        // Debug.Log(direction);
    }
    public bool IsPiercing()
    {
        return piercing;
    }

    public virtual void EndOfLifespanBehavior()
    {
        Destroy(gameObject);
    }
}
